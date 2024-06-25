using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;


    private void Awake()
    {
        // Implementación del patrón Singleton
        if (current == null)
        {
            current = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (current != this)
        {
            Destroy(gameObject);
        }
    }



    public event Action<Unidades> onUnidadMuerta;

    
    public void UnidadMuerta(Unidades unidad)
    {
        onUnidadMuerta?.Invoke(unidad);
    }
}
