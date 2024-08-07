using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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




        for (int i = 0;i < tiposDeUnidades.Count; i++)
        {
            //para debug
            if (tiposDeUnidades[i] == null || tiposDeUnidades[i].prefab == null)
            {
                //Debug.LogError($"el tipo de unidad en el índice {i} es null.");
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
                    Debug.LogError($"No se instanció la unidad del tipo {tiposDeUnidades[i].name}");
                    continue;
                }


                u.gameObject.SetActive(false);
                pool.Enqueue(u);
            }
            unidadesPool[tiposDeUnidades[i].prefab.GetComponent<Unidades>().GetType()] = pool;

        }



    }


    // Update is called once per frame
     void Update()
    {
       
    }

    //funcion para genear unidad en funcion del i introducido.
    //Será por boton en canvas
    public void GenerarUnidad(Button b)
    {
        int i = 0;
        switch (b.transform.name)
        {
            case "UnidadEnemigo 0": 
                i = 0;
                break;
            case "UnidadEnemigo 1":
                i = 1;
                break;
            case "UnidadEnemigo 2":
                i = 2;
                break;
            case "Unidad 0":
                i = 0;
                break;
            case "Unidad 1":
                i = 1;
                break;
            case "Unidad 2":
                i = 2;
                break;
        }



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

            b.GetComponent<ButtonCooldown>().OnButtonPressCooldown();
            b.interactable = false;



        }
        else
        {
            //Si has alcancedo limite de unidades
            Debug.Log(lUnidadesActivas.Count >= limiteUnidad ? "Máximo de unidades alcanzado." : "Oro insuficiente.");

        }

  


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

        float danioDespuesDefensa = Mathf.Max(0, danio - defensa);
        vida -= danioDespuesDefensa;

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
