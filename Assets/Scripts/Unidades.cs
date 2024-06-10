using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unidades : MonoBehaviour
{

    public float vida, ataque, defensa, velocidad, alcance, velocidadAtaque, distanciaAlObjetivo;

    public float coste;
    public bool esJugador;

    public GameObject target;

    public Vector3 origenSpawn;

    //Para raycast no se si es necesario.
    public Vector3 direction = Vector3.forward;
    public float rayCastLenght;

    //Variables de estado de la unidad
    public bool andando;
    public bool atacando;
    public bool recibiendoAtaque;
    public bool muerto;


    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //1 comprobar si esta muerto
        //Caso positivo llamar funcion para animacion de muerte y reiniciar unidad.
        muerto = isDeath();

        if (muerto)
        {
            //Crea metodo de muerte
            //Deberia activar efecto de muerte desactivarse y seguir con Reset
            //Muerte();
            Debug.Log("Unidad Muerta!");
            this.gameObject.SetActive(false); //forma temporal!
            //Llamamos al reset.
            ResetUnit();
        }
        
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

            //Funcion atacando
            StartCoroutine(AtacarIE());
            Debug.Log("Terminado de Atacar.");
        }
        //4 Si no alcance mover
        else
        { moveUnit();andando = true; }
    


    }

    //Animacion de muerte, y reiniciar unidad, toda la vida, posicion de spawn etc.
    public void ResetUnit()
    {
        
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

    //Calculara el enemigo mas cercano 
    public void getCloseTarget(bool esjugadorAUX) {

        GameObject[] objetivos;

        //Diferenciar el objetivo de la IA o no
        
        if (esjugadorAUX) 
        { objetivos = GameObject.FindGameObjectsWithTag("Enemigo"); }
        else
            objetivos = GameObject.FindGameObjectsWithTag("Player");

        // Inicializar variables para el enemigo más cercano
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;


        // Recorrer todos los enemigos y encontrar el más cercano
        foreach (GameObject objetivo in objetivos)
        {
            Vector3 directionToTarget = objetivo.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestEnemy = objetivo;
            }
        }

        // Devolver el enemigo más cercano
        target = closestEnemy;


    }

    //Comprobar si el objetivo mas cercano ya seleccionado esta a alcance
    //para pasar a atacar.
    public bool targetAtDistance()
    {
        EmpezarTargetAtDistance:
        if (!isDeath()) { 
            this.distanciaAlObjetivo = Vector3.Distance(transform.position, this.target.transform.position);

            if (this.distanciaAlObjetivo < this.alcance)
            {
                //Debug.Log("Objetivo en alcance.");
                return true;
            }

            else
            {
                //Debug.Log("Objetivo NO esta en alcance.");
                return false;
            }
        }
        else
        {
            getCloseTarget(esJugador);
            goto EmpezarTargetAtDistance;
        }
    }



    //Usar un RayCast para buscar el objetivo mas cerncano?
    //Para el comportamiento
    /*
    public void rayCastTarget()
    {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit Hit;
        if (Physics.Raycast(ray, out Hit, rayCastLenght))
        {
            if (Hit.collider.CompareTag(enemyTag))
            {

            }
        }

    }
    */

    //Funcion atacar
    
    private IEnumerator AtacarIE() 
    {
        //Hacer o corrutina con cada ataque, o una variable tiempo que se vaya reseteando y comprobando con cada ataque.
        //Llamar animacion ataque efectos etc.
        //bucle hasta que el objetivo esté muerto o la unidad muera.

        //Si objetivo no tiene vida, salir y buscar siguiente objetivo mas cercano.
        //Si tiene vida, buscarle su fincion derecibir daño y pasarle nuestro daño y que lo reste a su vida.
        //depsues de atacar esperar el tiempo que indique su velocidad de ataque.

        Debug.Log("Empezando a ATACAR");
        Unidades unidadTarget = target.GetComponent<Unidades>();

        while (!muerto && !unidadTarget.isDeath())
        {
                    Debug.Log("Atacando!");
                    unidadTarget.RecibirAtaque(ataque);

                    Debug.Log("Esperando para volver a atacar!");
                    yield return new WaitForSeconds(velocidadAtaque);
                
        }

        atacando = false;
        Debug.Log("Dejando de Atacar!");

    }

    private float getVida() 
    {
        return vida;    
    }

    public void RecibirAtaque(float danio)
    {
         Debug.Log("recibiendo Danio!");
        recibiendoAtaque = true;
        //simple calculo del danio, baser defencia a restar directamente el danio
        vida -= danio - defensa;
        if (vida < 0)
        { 
            vida = 0;
        }

        recibiendoAtaque = false;
            
    }


    //Funcion que mueve la unidad, a izquierda si IA a derecha si Player
    public void moveUnit()
    {

        if (esJugador)
            transform.position += new Vector3(0, 0, velocidad * Time.deltaTime);
        else
            transform.position -= new Vector3(0,0, velocidad * Time.deltaTime);
    }

    
    


}
