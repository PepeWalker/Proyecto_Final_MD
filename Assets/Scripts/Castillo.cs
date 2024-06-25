using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Castillo : Unidades
{
    public int nivel, limiteUnidad;
    public float oro;

    public Transform spawnPoint;



    public List<GameObject> lUnidadesDisponibles, lUnidadesActivas;
    public List<Unidades> lUnidadesInstaciadas, lUnidadesMuertas;


    public List<DatosUnidad> tiposDeUnidades;
    private Dictionary<System.Type, Queue <Unidades>>unidadesPool;

   

    // Start is called before the first frame update
    void Start()
    {
        unidadesPool = new Dictionary<System.Type, Queue<Unidades>>();

        //para debug
        if (tiposDeUnidades.Count != lUnidadesDisponibles.Count)
        {
            Debug.LogError("El número de tipos de unidades no coincide con las unidades disponibles.");
            return;
        }



        for (int i = 0;i < tiposDeUnidades.Count; i++)
        {
            //para debug
            if (tiposDeUnidades[i] == null || tiposDeUnidades[i].prefab == null)
            {
                Debug.LogError($"El tipo de unidad o prefab en el índice {i} es null.");
                continue;
            }


            Queue<Unidades> pool = new Queue<Unidades>();
            //genero 5 de cada por ahora?
            for (int j = 0;j<5;j++) 
            {
                Unidades u = Instantiate(tiposDeUnidades[i].prefab, new Vector3(0, 1500, 0), Quaternion.identity).GetComponent<Unidades>();
                
                //para debug
                if (u == null)
                {
                    Debug.LogError($"No se pudo instanciar la unidad del tipo {tiposDeUnidades[i].name}");
                    continue;
                }


                u.gameObject.SetActive(false);
                pool.Enqueue(u);
            }
            unidadesPool[tiposDeUnidades[i].prefab.GetComponent<Unidades>().GetType()] = pool;

        }


        //Viejo codigo
        /*
        //llena la lista de unidades con unidades clonadas de la lista de prefab/GO unidades
        foreach (GameObject g in lUnidadesDisponibles)
        {
            Unidades u = Instantiate(g, new Vector3(0, 1500, 0), Quaternion.identity).GetComponent<Unidades>();
            lUnidadesInstaciadas.Add(u);
        }

        //Cojo motorbatlla por si acaso?
        //No se si necesario
        m = GetComponentInParent<MotorBatalla>();
        */

        //coger entre los GO hijos cual tiene la tag Spawnpoint para establecerlo
        //por si añado mas hijo en el futuro 
        // LO METO A MANO DESDE LA INTERFAZ
        /*foreach (Transform child in transform)
        {
            if (child.CompareTag("SpawnPoint"))
            {
                spawnPoint = child;
                break; // sale del bucle al encontrar el objecto con la etiqueta "SpawnPoint"
            }
        }
        */

    }

    //Hacer el comportamiento de la IA con otro castillo IA


    // Update is called once per frame
     void Update()
    {
       
    }


    void OnEnable()
    {
        GameEvents.current.onUnidadMuerta += UnidadMuerta;
    }

    void OnDisable()
    {
        GameEvents.current.onUnidadMuerta -= UnidadMuerta;
    }


    //funcion para genear unidad en funcion del i introducido.
    //Será por boton en canvas
    public void GenerarUnidad(int i)
    {
        if (lUnidadesActivas.Count < limiteUnidad && oro >= tiposDeUnidades[i].coste)
        {
            //rotacion basada en si es jugador o enemigo
            Quaternion rotacion = esJugador ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, 90, 0);

            //posicion de spawnpoint
            Vector3 posicion = spawnPoint.position;

            Unidades unidad = Instantiate(tiposDeUnidades[i].prefab, posicion, rotacion).GetComponent<Unidades>();
            unidad.datosUnidad = tiposDeUnidades[i];
            unidad.ResetearUnidad();
            lUnidadesActivas.Add(unidad.gameObject);
            decreaseGold(tiposDeUnidades[i].coste);

            Debug.Log($"Unidad generada en posición: {posicion}, rotación: {rotacion.eulerAngles}");


        }
        else
        {
            //Si has alcancedo limite de unidades
            Debug.Log(lUnidadesActivas.Count >= limiteUnidad ? "Máximo de unidades alcanzado." : "Oro insuficiente.");
        }
    }

    public void UnidadMuerta(Unidades unidad)
    {
        lUnidadesActivas.Remove(unidad.gameObject);
        unidad.gameObject.SetActive(false);

        System.Type tipoUnidad = unidad.GetType();
        if (unidadesPool.ContainsKey(tipoUnidad))
        {
            unidadesPool[tipoUnidad].Enqueue(unidad);
        }
    }

    public float getOro()
    {
        return oro;
    }
    public void increaseGold (float x) {oro += x;}

    public void decreaseGold (float x) {oro -= x;}



    // Funcion subir nivel
    // nivel 1 base
    // nivel 2 armadura sube, sube oro obtenido, numero de unidades maximo aumente
    // nivel 3 igual pero mas?
    public void levelUp()
    {
        //mas adelante aniadir diferencia para nivel 2 y 3
        if (oro>= 100)
        {
            nivel++;
            limiteUnidad += 5;
            oro -= 100;
            Debug.Log("Subido nivel de castillo");
        }
        else
            Debug.Log("No suficiente oro.");
    }


    public override void RecibirAtaque(float danio)
    {
        base.RecibirAtaque(danio);
        if (isDeath())
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        Debug.Log(esJugador ? "¡Has perdido!" : "¡Has ganado!");

        //  añadir mas cosas para finalizar el juego
        //  mostrar una pantalla de fin de juego, detener la generación de unidades, etc.. no se que mas podria poner.
    }




}
