using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unidades : MonoBehaviour
{

    public float vida, vidaMax, ataque, defensa, velocidad, alcance, velocidadAtaque, distanciaAlObjetivo;

    public float coste;
    public bool esJugador;

    public GameObject target;

    public Vector3 origenSpawn;

    //Para raycast no se si es necesario.
    public Vector3 direction = Vector3.forward;
    public float rayCastLenght;

    //Variables de estado de la unidad
    public enum UnidadEstado
    {
        Idle,
        Andando,
        Atacando,
        RecibiendoAtaque,
        Muriendo
    }
    private UnidadEstado estadoActual;


    public bool andando;
    public bool atacando;
    public bool recibiendoAtaque;
    public bool muerto;

    // para no recibir varios ataque seguidos mientras esté recibiendo ya un ataque
    private bool puedeRecibirDanio = true;


    public Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        estadoActual = UnidadEstado.Idle;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // comprobar si la unidad ha muerto
        CheckDeath();  

        if (!muerto)
        {
            // actualizamos y/o buscamos el objetivo mas cercano
            getCloseTarget(esJugador);

            

            switch (estadoActual)
            {
                case UnidadEstado.Idle:
                    EstadoIdle();
                    break;
                case UnidadEstado.Andando:
                    EstadoAndando();
                    break;
                case UnidadEstado.Atacando:
                    EstadoAtacando();
                    break;
                case UnidadEstado.RecibiendoAtaque:
                    if (recibiendoAtaque)
                    {
                        StartCoroutine(EstadoRecibiendoAtaque());
                    }
                    break;
                case UnidadEstado.Muriendo:
                    EstadoMuriendo();
                    break;
            }
        }

        /* antigua logica comportamiento
         * 
            //1 comprobar si esta muerto
            //Caso positivo llamar funcion para animacion de muerte y reiniciar unidad.
            CheckDeath();
        
        //2 Buscar enemigo mas cercano y comprobar si esta a alcance para atacar
        getCloseTarget(this.esJugador);


        //3 Si esta objetivo al alcance, atacando=true y andando=false

        //comprueba si esta a alacance
        if(atacando)
        {
            Debug.Log("sigue atacando.");
        }
        else if (targetAtDistance() && !atacando)
        {

            atacando = true;
            andando = false;
            anim.SetBool("Andando", false);

            //Funcion atacando
            StartCoroutine(AtacarIE());
            Debug.Log("Terminado de Atacar.");
        }
        //4 Si no alcance mover
        else
        { 
            moveUnit();
            anim.SetBool("Andando", true);
            andando = true;
            
        }
        */



    }

    public virtual void CheckDeath()
    {
        if (isDeath() && estadoActual != UnidadEstado.Muriendo)
        {
            CambiarEstado(UnidadEstado.Muriendo);
        }
    }

    //Animacion de muerte, y reiniciar unidad, toda la vida, posicion de spawn etc.
    public void ResetearUnidad()
    {
        vida = vidaMax;
        andando = false;
        atacando = false;
        recibiendoAtaque = false;
        muerto = false;
        target = null;
        transform.position = origenSpawn;
        gameObject.SetActive(true);
        puedeRecibirDanio = true;

        //NO se si hace falta hacer algo con el animator 
        CambiarEstado(UnidadEstado.Idle);

        anim.SetBool("Muerto", false);
    }

    public bool isDeath()
    {
        //Si la vida llega a cero o menos, eliminamos la unidad o reseteamos
        if (vida <= 0)
        {
            return true;
        }
        return false;
    }

    private IEnumerator Morir()
    {
        // Activar animación de muerte 
        if (anim != null)
        {
            anim.SetTrigger("Muerto");
            // Esperar a que termine la animación
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+1.5f);
        }

        gameObject.SetActive(false);

        // Notificar al Castillo
        Castillo castillo = FindObjectOfType<Castillo>();
        if (castillo != null)
        {   
            //Comunicar con el castillo
            //castillo.UnidadMuerta(this);
        }
    }


    //Calculara el enemigo mas cercano 
    public void getCloseTarget(bool esjugadorAUX) {

        //En funcion de que objetivo busca la unidad, se asigna una tag y otra
        string tagObjetivo = esjugadorAUX ? "Enemigo" : "Player";

        GameObject[] objetivos = GameObject.FindGameObjectsWithTag(tagObjetivo);

        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject objetivo in objetivos)
        {
            float dSqrToTarget = (objetivo.transform.position - currentPosition).sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestEnemy = objetivo;
            }
        }

        target = closestEnemy;

    }

    //Comprobar si el objetivo mas cercano ya seleccionado esta a alcance
    //para pasar a atacar.
    public bool targetAtDistance()
    {
        // primero comprobar si tiene target o si está muerto
        if (target == null || target.GetComponent<Unidades>().isDeath())
        {
            getCloseTarget(esJugador);
            if (target == null) return false;
        }

        distanciaAlObjetivo = Vector3.Distance(transform.position, target.transform.position);
        return distanciaAlObjetivo <= alcance;
    }





    //Funcion atacar

    internal IEnumerator AtacarIE() 
    {

        atacando = true;
        while (estadoActual == UnidadEstado.Atacando && target != null)
        {

            // obtengo el sc del target 
            Unidades targetUnidad = target.GetComponent<Unidades>();
            if (targetUnidad != null)
            {
                // para esperar hasta que la unidad pueda recibirdanio
                yield return new WaitUntil(() => targetUnidad.PuedeRecibirDanio());


                //animacion de ataque
                anim.SetTrigger("Atacando");
                yield return new WaitForSeconds(0.2f);

                //aplico el daño
                targetUnidad.RecibirAtaque(ataque);
                Debug.Log($"Atacando a {target.name}. Daño: {ataque}");
            }
            //espera el tiempo indicado por velocidad ataque
            yield return new WaitForSeconds(velocidadAtaque);
        }
        atacando = false;

    }

    internal float getVida()
    {
        return vida;
    }

    public bool PuedeRecibirDanio()
    {
        return puedeRecibirDanio;
    }

    public void RecibirAtaque(float danio)
    {
        if (!puedeRecibirDanio) return;

        Debug.Log($"{gameObject.name} recibiendo daño: {danio}");

        puedeRecibirDanio = false;
        recibiendoAtaque = true;

        float danioDespuesDefensa = Mathf.Max(0, danio - defensa);
        vida -= danioDespuesDefensa;

        Debug.Log($"Vida restante de {gameObject.name}: {vida}");

        if (vida <= 0)
        {
            vida = 0;
            CambiarEstado(UnidadEstado.Muriendo);
        }
        else
        {
            CambiarEstado(UnidadEstado.RecibiendoAtaque);
        }

    }


    //Funcion que mueve la unidad, a izquierda si IA a derecha si Player
    public void moveUnit()
    {

        if (esJugador)
            transform.position += new Vector3(0, 0, velocidad * Time.deltaTime);
        else
            transform.position -= new Vector3(0,0, velocidad * Time.deltaTime);
    }


    // -------------------------------------------
    // Funciones para comprobar el estado de las unidades
    // para hacerlo escalable
    //


    private void EstadoIdle()
    {
        getCloseTarget(esJugador);
        if (target != null)
        {
            if (targetAtDistance())
            {
                CambiarEstado(UnidadEstado.Atacando);
            }
            else
            {
                CambiarEstado(UnidadEstado.Andando);
            }
        }
    }

    private void EstadoAndando()
    {
       
        if (targetAtDistance())
        {
            CambiarEstado(UnidadEstado.Atacando);
        }
        else
        {
            moveUnit();
            anim.SetBool("Andando", true);
        }
    }

    private void EstadoAtacando()
    {

        if (!targetAtDistance())
        {
            CambiarEstado(UnidadEstado.Andando);
        }
        else if (!atacando)
        {
            StartCoroutine(AtacarIE());
        }
        if (recibiendoAtaque)
        {
            CambiarEstado(UnidadEstado.RecibiendoAtaque);
        }

    }
    private IEnumerator EstadoRecibiendoAtaque()
    {
        anim.SetTrigger("RecibiendoAtaque");

        Debug.Log("Recibiendo un Ataque!");
        // Esperar a que la animación termine
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+0.1f);

        recibiendoAtaque = false;
        puedeRecibirDanio = true;

        // Volver al estado anterior
        if (target != null && targetAtDistance())
        {
            CambiarEstado(UnidadEstado.Atacando);
        }
        else
        {
            CambiarEstado(UnidadEstado.Andando);
        }
    }
        private void EstadoMuriendo()
    {
        if (!muerto)
        {
            StartCoroutine(Morir());
        }
    }

    // ----------------------
    // Funcion cambiar estado

    private void CambiarEstado(UnidadEstado nuevoEstado)
    {
        // salir del estado actual
        switch (estadoActual)
        {
            case UnidadEstado.Andando:
                anim.SetBool("Andando", false);
                break;
            case UnidadEstado.Atacando:
                StopCoroutine(AtacarIE());
                atacando = false;
                break;
            case UnidadEstado.RecibiendoAtaque:
                // 
                break;
        }

        // entrar al nuevo estado
        estadoActual = nuevoEstado;
        switch (nuevoEstado)
        {
            case UnidadEstado.Atacando:
                anim.SetTrigger("Atacando");
                break;
            case UnidadEstado.RecibiendoAtaque:
                //anim.SetTrigger("RecibiendoAtaque");
                //activo la animacion en el IEnumerator, asique seria redundante
                break;
            case UnidadEstado.Muriendo:
                anim.SetBool("Muerto",true);
                break;
        }
    }
}
