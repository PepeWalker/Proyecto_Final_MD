using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Castillo : Unidades
{
    public int nivel, limiteUnidad;
    public float oro;

    public Transform spawnPoint;

    public MotorBatalla m; //temporal

    
    public List<GameObject> lUnidadesDisponibles, lUnidadesActivas;

    // Start is called before the first frame update
    void Start()
    {


        spawnPoint = transform.GetChild(0);
    }

    //Hacer el comportamiento de la IA con otro castillo IA


    // Update is called once per frame
    void Update()
    {

        //Para generar unidad, en corrutina para que haya cooldown de X tiempo.
        //Generar Unidad 1
        if (Input.GetKeyDown("1") && lUnidadesActivas.Count < limiteUnidad)
        {
            if (oro >= m.lUnidades[0].coste)
            {
                decreaseGold(m.lUnidades[0].coste);
                lUnidadesActivas.Add( Instantiate(m.lpUnidades[0], spawnPoint));
                
            }
            else
                Debug.Log("No hay suficiente oro para la unidad 1");


        }
        //Generar Unidad 2
        if (Input.GetKeyDown("2") && lUnidadesActivas.Count < limiteUnidad)
        {
            if (oro >= m.lUnidades[1].coste)
            {
                decreaseGold(m.lUnidades[1].coste);
                lUnidadesActivas.Add(Instantiate(m.lpUnidades[1], spawnPoint));

            }
            else
                Debug.Log("No hay suficiente oro para la unidad 2");


        }
        //Generar Unidad 3
        if (Input.GetKeyDown("3") && lUnidadesActivas.Count < limiteUnidad)
        {
            if (oro >= m.lUnidades[2].coste)
            {
                decreaseGold(m.lUnidades[2].coste);
                lUnidadesActivas.Add(Instantiate(m.lpUnidades[2], spawnPoint));

            }
            else
                Debug.Log("No hay suficiente oro para la unidad 3");


        }



    }
    public void GenerarUnidad(int i,Castillo u)
    {
        if (lUnidadesActivas.Count < limiteUnidad)
        {
            if(oro >= u.coste)
            {
                decreaseGold(u.coste);
                lUnidadesActivas.Add(Instantiate(m.lpUnidades[i], spawnPoint));
            }
        }
    }

    //}
    //else if (Input.GetKeyDown("1"))
    //    Debug.Log("Unidades tipo 1 Maximas");


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


    




}
