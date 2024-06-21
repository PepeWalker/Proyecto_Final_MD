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
    public List<Unidades> lUnidadesInstaciadas;

    // Start is called before the first frame update
     void Start()
    {

        
        //llena la lista de unidades con unidades clonadas de la lista de prefab/GO unidades
        foreach (GameObject g in lUnidadesDisponibles)
        {
            Unidades u = Instantiate(g, new Vector3(0, 1500, 0), Quaternion.identity).GetComponent<Unidades>();
            lUnidadesInstaciadas.Add(u);
        }

        //Cojo motorbatlla por si acaso?
        //No se si necesario
        m = GetComponentInParent<MotorBatalla>();


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

    //funcion para genear unidad en funcion del i introducido.
    //Será por boton en canvas
    public void GenerarUnidad(int i)
    {
        if (lUnidadesActivas.Count < limiteUnidad)
        {
            if(oro >= lUnidadesInstaciadas[i].coste)
            {
                decreaseGold(lUnidadesInstaciadas[i].coste);
                lUnidadesActivas.Add(Instantiate(lUnidadesDisponibles[i], spawnPoint));
            }
            else
            {
                Debug.Log("No hay suficiente oro para generar esta unidad.");
            }
        }
        else
        {
            Debug.Log("Maximo unidades alcanzado, espera que alguna se muera...");
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
