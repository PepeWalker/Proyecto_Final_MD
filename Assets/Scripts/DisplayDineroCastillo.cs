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


    // Start is called before the first frame update
    void Start()
    {
        castilloScript = castilloOBJ.GetComponent<Castillo>();

        GameObject textoOroObj = GameObject.Find("TextoOro");

        textoOro = textoOroObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textoOro.text = "Oro: " + castilloScript.oro.ToString();
    }
}
