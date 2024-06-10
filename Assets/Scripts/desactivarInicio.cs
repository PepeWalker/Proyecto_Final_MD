using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class desactivarInicio : MonoBehaviour
{
    public GameObject GO ;

    // Start is called before the first frame update
    void Start()
    {
        GO = this.gameObject;
        StartCoroutine(ActivoAFalse());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ActivoAFalse()
    {
        yield return new WaitForSeconds(1);

        GO.SetActive(false);
        Debug.Log("Objeto desactivado");
    }


}
