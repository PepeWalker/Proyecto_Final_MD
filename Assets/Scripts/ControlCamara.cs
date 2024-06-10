using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamara : MonoBehaviour
{
    public float speed = 30.0f;  // Velocidad de movimiento de la c�mara
    public float verticalSpeed = 15.0f;  // Velocidad de movimiento vertical

    public float minX = 15.0f;   // L�mite m�nimo en el eje X
    public float maxX = 65.0f;   // L�mite m�ximo en el eje X
    public float minZ = -30.0f;  // L�mite m�nimo en el eje Z
    public float maxZ = 30.0f;   // L�mite m�ximo en el eje Z
    public float minY = 5.0f;    // L�mite m�nimo en el eje Y
    public float maxY = 30.0f;   // L�mite m�ximo en el eje Y

    private Transform fondoTransform;  // Transform del objeto con la etiqueta "Fondo"

    // Start is called before the first frame update
    void Start()
    {

        // Buscar el objeto con la etiqueta "Fondo"
        GameObject fondo = GameObject.FindGameObjectWithTag("Camino");
        if (fondo != null)
        {
            fondoTransform = fondo.transform;
        }
        else
        {
            Debug.LogError("No se encontr� ning�n objeto con la etiqueta 'Fondo'");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (fondoTransform == null) return;

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

        // Ajustar la rotaci�n de la c�mara para mirar hacia el objeto "Fondo"
        Vector3 directionToFondo = fondoTransform.position - transform.position;
        Quaternion rotationToFondo = Quaternion.LookRotation(directionToFondo);
        Vector3 eulerRotationToFondo = rotationToFondo.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotationToFondo.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

    }
}
