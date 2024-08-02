using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unidades : MonoBehaviour
{
    public DatosUnidad datosUnidad;

    public ParticleSystem particulasDanio;


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
    public UnidadEstado estadoActual;


    public bool andando, corRunning;
    public bool atacando;
    public bool recibiendoAtaque;
    public bool muerto;

    // para no recibir varios ataque seguidos mientras esté recibiendo ya un ataque
    public bool puedeRecibirDanio = true;


    public Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        estadoActual = UnidadEstado.Idle;

        float RandomBonus() => Random.Range(0f, 2f);

        vida = datosUnidad.vidaMax;
        ataque = datosUnidad.ataque ;
        defensa = datosUnidad.defensa;
        velocidad = datosUnidad.velocidad + RandomBonus();
        alcance = datosUnidad.alcance + RandomBonus();
        velocidadAtaque = datosUnidad.velocidadAtaque + RandomBonus();


        // mostrar en consola los valores para debug
        Debug.Log($"Unidad {gameObject.name} inicializada con: " +
                  $"Vida: {vida}, Ataque: {ataque}, Defensa: {defensa}, " +
                  $"Velocidad: {velocidad}, Alcance: {alcance}, " +
                  $"Velocidad de Ataque: {velocidadAtaque}");


    }

    // Update is called once per frame
    void Update()
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
                        recibiendoAtaque = false;
                    }
                    break;
                case UnidadEstado.Muriendo:
                    EstadoMuriendo();
                    break;
            }
        

        

    }

    public virtual void CheckDeath()
    {

        if (vida <= 0)
        {
            vida = 0;
            muerto = true;
            CambiarEstado(UnidadEstado.Muriendo);
        }
        else if (recibiendoAtaque)
        {
            CambiarEstado(UnidadEstado.RecibiendoAtaque);
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

        
        CambiarEstado(UnidadEstado.Idle);

        anim.SetBool("Muerto", false);
    }

    public bool isDeath()
    {
        //Si la vida llega a cero o menos, eliminamos la unidad o reseteamos
        if (vida <= 0)
        {
            muerto = true;
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
            yield return new WaitForSeconds(2.5f);
        }


        // Notificar al Castillo
        Castillo castillo = FindObjectOfType<Castillo>();
        if (castillo != null)
        {   
            //Comunicar con el castillo
            // quien la pone inactiva y gestiona la lista de unidades disponibles etc
            castillo.UnidadMuerta(this);
        }

        GameEvents.Instance.UnidadMuerta(this);
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
        corRunning = true;
                
        
        
            atacando = true;

            // obtengo el sc del target 
            Unidades targetUnidad = target.GetComponent<Unidades>();
            if (targetUnidad != null && targetUnidad.PuedeRecibirDanio())
            {
                
                //animacion de ataque
                anim.SetTrigger("Atacando");
                //esperar que la animacion termine
                yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length + 0.2f);

                //aplico el daño
                targetUnidad.RecibirAtaque(ataque);
                Debug.Log($"Atacando a {target.name}. Daño: {ataque}");

                //espera el tiempo indicado por velocidad ataque
                Debug.Log($"{this.name} esperando para siguiente ataque.");
                yield return new WaitForSeconds(velocidadAtaque);
            }
            else
            {
                //si el target no puede recibir danio, espera un poco para repetir otro ataque
                //Esto lo pongo porque atacaban muy rapido y como son clones necesito algo de control
                yield return new WaitForSeconds(0.5f);
            }
            
        
        corRunning = false;
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

    public virtual void RecibirAtaque(float danio)
    {
        if (!puedeRecibirDanio) return;

        Debug.Log($"{gameObject.name} recibiendo daño: {danio}");


        puedeRecibirDanio = false;
        recibiendoAtaque = true;

        float danioDespuesDefensa = Mathf.Max(0, danio - defensa);
        vida -= danioDespuesDefensa;



        Debug.Log($"Vida restante de {gameObject.name}: {vida}");

        // comprobar si la unidad ha muerto
        CheckDeath();


    }


    //Funcion que mueve la unidad, a izquierda si IA a derecha si Player
    public void moveUnit()
    {
        Debug.Log($"{gameObject.name} moviendose.");
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
       if(target != null)
        {
            getCloseTarget(esJugador);
        }
        if (targetAtDistance())
        {
            andando = false;
            CambiarEstado(UnidadEstado.Atacando);
            Debug.Log($"{gameObject.name} Cambiado a estado de Ataque");
        }
        else
        {
            andando = true;
            moveUnit();
            anim.SetBool("Andando", true);

            Debug.Log("Andando...");
        }
    }

    private void EstadoAtacando()
    {

        if (!targetAtDistance())
        {
            atacando = false;
            CambiarEstado(UnidadEstado.Andando);
        }
        else if (!atacando )
        {
            if(!corRunning) 
            { 
                StartCoroutine(AtacarIE()); 
            }
            
        }
        if (recibiendoAtaque)
        {
            atacando = false;
            CambiarEstado(UnidadEstado.RecibiendoAtaque);
        }

    }
    private IEnumerator EstadoRecibiendoAtaque()
    {
        
        puedeRecibirDanio = false;

        anim.SetTrigger("RecibiendoAtaque");
        particulasDanio.Play();

        Debug.Log($"{gameObject.name} esta recibiendo un Ataque!");
        // Esperar a que la animación termine
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length+0.3f);

        
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
        if (muerto)
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
                if (corRunning)
                { StopCoroutine(AtacarIE());
                    corRunning = false; 
                    atacando = false;
                }
                  
                break;
            case UnidadEstado.RecibiendoAtaque:
                // 
                break;
        }

        // entrar al nuevo estado
        estadoActual = nuevoEstado;
        /*switch (nuevoEstado)
        {
            case UnidadEstado.Atacando:
                //anim.SetTrigger("Atacando");
                break;
            case UnidadEstado.RecibiendoAtaque:
                //anim.SetTrigger("RecibiendoAtaque");
                //activo la animacion en el IEnumerator, asique seria redundante
                break;
            case UnidadEstado.Muriendo:
                //anim.SetBool("Muerto",true);
                break;
        }*/
    }
}
