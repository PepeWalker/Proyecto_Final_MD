using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class MotorBatalla : MonoBehaviour
{

   

    public int nivelCastilloMax; //predefinir cual ser� el nivel maximo por partida

    public Castillo castilloIA, castilloJugador;

    public float nivelOro=5;

    public float delayAmount = 5.0f;
    protected float timer;


   // Start is called before the first frame update
   void Start()
    {

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

    public void AumentoOro()
    {
        timer += Time.deltaTime;
        if (timer > delayAmount)
        {
            timer = 0f;
            castilloJugador.increaseGold(nivelOro * castilloJugador.nivel);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Tiempo para aumento de oro
        AumentoOro();

        //subir nivel
        if (Input.GetKeyDown("q"))
        {
            LevelUpCastillo(castilloJugador);
        }
        
    }


}
