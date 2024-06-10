using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    // Start is called before the first frame update


    public Transform target;
    public GameObject targetObj;
    void Start()
    {
        transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        

        transform.LookAt(target);
    }
}
