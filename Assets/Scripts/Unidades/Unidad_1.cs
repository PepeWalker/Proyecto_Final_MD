using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unidad_1 : Unidades
{

    public MotorBatalla motorScript;




    // Start is called before the first frame update
    void Start()
    {
        vida = 100;
        ataque = 10;
        defensa = 1;
        velocidad = 3;
        alcance = 1;
        velocidadAtaque = 2;

        coste = 40f;


        origenSpawn = new Vector3(0f, 0.1f, 0.6f);


        motorScript = gameObject.GetComponentInParent<MotorBatalla>();

        //Al inicializar, mirar si es de jugador, y poner TAGs Player o Enemigo en consecuencia.
        esJugador = true; //temporal
    }

    

    // Update is called once per frame
    void Update()
    {
       

    }



}
