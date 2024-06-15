using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Los limites hay poder cambiar segun escenario


    public float speed = 10.0f;  // Velocidad de movimiento de la c�mara
    public float verticalSpeed = 5.0f;  // Velocidad de movimiento vertical
    public float minX = 16.0f;   // L�mite m�nimo en el eje X
    public float maxX = 65.0f;   // L�mite m�ximo en el eje X
    public float minZ = -30.0f;  // L�mite m�nimo en el eje Z
    public float maxZ = 30.0f;   // L�mite m�ximo en el eje Z
    public float minY = 5.0f;    // L�mite m�nimo en el eje Y
    public float maxY = 30.0f;   // L�mite m�ximo en el eje Y

    [SerializeField] 
    private Transform lookAtObject;  // Transform del objeto con la etiqueta "Fondo"
    private Renderer fondoRenderer;    // Renderer del objeto con la etiqueta "Fondo"

    void Start()
    {
        if (lookAtObject == null)
            lookAtObject = GameObject.Find("lookAtObject").transform;
                
    }

    void Update()
    {
        

        // Obtener la entrada del usuario
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveY = 0;

        // Ajustar la altura con la tecla Espacio y Control
        if (Input.GetKey(KeyCode.Space))
        {
            moveY = verticalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            moveY = -verticalSpeed * Time.deltaTime;
        }

        // Calcular el desplazamiento en base a la entrada del usuario y la direcci�n de la c�mara
        Vector3 moveDirection = (transform.right * moveHorizontal + transform.forward * moveVertical);
        moveDirection.y += moveY;
        moveDirection *= speed * Time.deltaTime;

        // Obtener la nueva posici�n de la c�mara
        Vector3 newPosition = transform.position + moveDirection;

        // Restringir la posici�n de la c�mara dentro de los l�mites especificados
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Asignar la nueva posici�n a la c�mara
        transform.position = newPosition;

        transform.LookAt(lookAtObject);
    }
}
