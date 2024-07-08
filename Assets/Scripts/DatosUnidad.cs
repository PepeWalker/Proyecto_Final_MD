using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NuevaUnidad", menuName = "Unidades/DatosUnidad")]
public class DatosUnidad : ScriptableObject
{
    public GameObject prefab;
    public float coste;
    public float vidaMax;
    public float ataque;
    public float defensa;
    public float velocidad;
    public float alcance;
    public float velocidadAtaque;
    public Vector3 escala;
}
