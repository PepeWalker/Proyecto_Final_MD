using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

public class DisplayDineroCastillo : MonoBehaviour
{
    public GameObject castilloOBJ;
    public Castillo castilloScript;  
    public TextMeshProUGUI textoOro;
    public TextMeshProUGUI textoNivel;




    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        textoOro.text = castilloScript.oro.ToString();
        textoNivel.text = castilloScript.nivel.ToString();
    }
}
