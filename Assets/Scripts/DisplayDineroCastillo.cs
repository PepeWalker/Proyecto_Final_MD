using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class DisplayDineroCastillo : MonoBehaviour
{

    public Castillo castilloScriptJugador, CastillosSctiptEnemigo;  
    public TextMeshProUGUI textoOroJugador, textoOroFondoJugador, textoOroEnemigo, textoOroFondoEnemigo;
    public TextMeshProUGUI textoNivelJugador, textoNivelEnemigo;


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        textoOroJugador.text = castilloScriptJugador.oro.ToString();
        textoOroFondoJugador.text = textoOroJugador.text;
        textoNivelJugador.text = castilloScriptJugador.nivel.ToString();


        textoOroEnemigo.text = CastillosSctiptEnemigo.oro.ToString();
        textoOroFondoEnemigo.text = textoOroEnemigo.text;
        textoNivelEnemigo.text = CastillosSctiptEnemigo.nivel.ToString();
    
    }
}
