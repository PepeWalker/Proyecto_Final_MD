using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class DisplayDineroCastillo : MonoBehaviour
{
    public static DisplayDineroCastillo instance;   
    public TextMeshProUGUI textoOroJugador, textoOroFondoJugador, textoOroEnemigo, textoOroFondoEnemigo;
    public TextMeshProUGUI textoNivelJugador, textoNivelEnemigo;


    private void Awake()
    {
        if (DisplayDineroCastillo.instance == null)
        {
            instance = this;
        }
        else { 
        Destroy(this.gameObject);
        }
    }

    //Actualziar textos ambos castillos
    public void UpdateTexts (Castillo cJ, Castillo cE)
    {
        textoOroJugador.text = cJ.oro.ToString();
        textoOroFondoJugador.text = textoOroJugador.text;
        textoNivelJugador.text = cJ.nivel.ToString();


        textoOroEnemigo.text = cE.oro.ToString();
        textoOroFondoEnemigo.text = textoOroEnemigo.text;
        textoNivelEnemigo.text = cE.nivel.ToString();
    }

    //Actualizar textos solo un texto
    public void UpdateTextsC(Castillo c)
    {
        if (c.esJugador)
        {
            textoOroJugador.text = c.oro.ToString();
            textoOroFondoJugador.text = textoOroJugador.text;
            textoNivelJugador.text = c.nivel.ToString();
        }
        else
        {

            textoOroEnemigo.text = c.oro.ToString();
            textoOroFondoEnemigo.text = textoOroEnemigo.text;
            textoNivelEnemigo.text = c.nivel.ToString();
        }


       

    }

}
