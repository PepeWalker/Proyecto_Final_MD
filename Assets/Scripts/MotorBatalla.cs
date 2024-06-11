using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class MotorBatalla : MonoBehaviour
{

    public List<GameObject> lpUnidades; //lista player?

    public List<Unidades> lUnidades; //Lista unidades?

    public int nivelCastilloMax;

    public Castillo castilloIA, castilloJugador;

    public float nivelOro=5;

    public float delayAmount = 5.0f;
    protected float timer;


   // Start is called before the first frame update
   void Start()
    {
       // castilloIA = transform.GetChild(2).GetComponent<Castillo>();
        //castilloJugador = transform.GetChild(1).GetComponent<Castillo>();

        castilloJugador.lUnidadesDisponibles = lpUnidades;

        foreach (GameObject g in lpUnidades)
        {
            Unidades u = Instantiate(g, new Vector3(0, 1500, 0),Quaternion.identity).GetComponent<Unidades>();
            lUnidades.Add(u);
        }
        castilloJugador.m = this;

        timer = 0f;

       
    }

    public void LevelUpCastillo(Castillo c)
    {
        if(c.nivel < nivelCastilloMax)
        {
            c.levelUp();
            c.nivel++;
        }
        else
        {
            Debug.Log("Nivel maximo alcanzado.");
        }
    }
    // Update is called once per frame
    void Update()
    {

        //Tiempo para aumento de oro
        timer += Time.deltaTime;
        if (timer > delayAmount) {
            timer = 0f;
            castilloJugador.increaseGold(nivelOro * castilloJugador.nivel);
        }

        //subir nivel
        if (Input.GetKeyDown("q"))
        {
            LevelUpCastillo(castilloJugador);
        }
        


        ////Generar unidad 1
        //if (Input.GetKeyDown("1") && contador1 < 10)
        //{
        //    if (castilloScript.getGold() >= unidad_1Script.getCoste())
        //    {
        //        castilloScript.decreaseGold(unidad_1Script.getCoste());
        //        Instantiate(Unidades_1[contador1], spawnUnidades.transform);
        //        contador1++;
        //    }
        //    else
        //        Debug.Log("No hay suficiente oro para la unidad 1");

        //}
        //else if (Input.GetKeyDown("1"))
        //    Debug.Log("Unidades tipo 1 Maximas");

        /* //aun no hechos unidad 2 y 3
        if (Input.GetKeyDown("2") && contador1 < 10)
        {
            Instantiate(Unidades_2[contador2], spawnUnidades.transform);
            contador2++;

        }
          if (Input.GetKeyDown("3") && contador1 < 5)
        {
            Instantiate(Unidades_3[contador3], spawnUnidades.transform);
            contador3++;

        }
        */




    }




}
