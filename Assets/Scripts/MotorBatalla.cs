using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class MotorBatalla : MonoBehaviour
{

   

    public int nivelCastilloMax=10; //predefinir cual será el nivel maximo por partida

    public Castillo castilloEnemigo, castilloJugador;

    public float nivelOro=5;

    public float delayAmount = 5.0f; //delay del oro que se genera
    protected float timer;

    //Canvas GameOvers
    public GameObject canvasGameOver;
    public GameObject canvasJuego;

    [SerializeField] private bool _juegoTerminado = false;

   // Start is called before the first frame update
   void Start()
    {

        timer = 0f;
        GameEvents.Instance = new GameObject("GameEvents").AddComponent<GameEvents>();
    }

    public void LevelUpCastillo(Castillo c)
    {
        if(c.nivel < nivelCastilloMax)
        {
            c.levelUp();
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
            castilloEnemigo.increaseGold(nivelOro * castilloEnemigo.nivel);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!_juegoTerminado)
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

    public void OnCastilloDestruido()
    {

        _juegoTerminado = true;

    }


}
