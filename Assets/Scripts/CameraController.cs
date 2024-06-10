using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10.0f;  // Velocidad de movimiento de la c�mara
    public float verticalSpeed = 5.0f;  // Velocidad de movimiento vertical
    public float minX = 16.0f;   // L�mite m�nimo en el eje X
    public float maxX = 65.0f;   // L�mite m�ximo en el eje X
    public float minZ = -30.0f;  // L�mite m�nimo en el eje Z
    public float maxZ = 30.0f;   // L�mite m�ximo en el eje Z
    public float minY = 5.0f;    // L�mite m�nimo en el eje Y
    public float maxY = 30.0f;   // L�mite m�ximo en el eje Y

    private Transform fondoTransform;  // Transform del objeto con la etiqueta "Fondo"
    private Renderer fondoRenderer;    // Renderer del objeto con la etiqueta "Fondo"

    void Start()
    {
        // Buscar el objeto con la etiqueta "Fondo"
        //Para que la camara siga siempre la "accion"
        GameObject fondo = GameObject.FindGameObjectWithTag("Camino");
        if (fondo != null)
        {
            fondoTransform = fondo.transform;
            fondoRenderer = fondo.GetComponent<Renderer>();
            if (fondoRenderer == null)
            {
                Debug.LogError("El objeto 'Fondo' no tiene un componente Renderer.");
            }
        }
        else
        {
            Debug.LogError("No se encontr� ning�n objeto con la etiqueta 'Fondo'");
        }
    }

    void Update()
    {
        if (fondoTransform == null || fondoRenderer == null) return;

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

        // Calcular la posici�n en el objeto "Fondo" que debe mirar la c�mara
        Bounds fondoBounds = fondoRenderer.bounds;
        float fondoWidth = fondoBounds.size.x;

        // Calcular la posici�n a lo largo del ancho del "Fondo"
        float t = (newPosition.x - minX) / (maxX - minX);  // Normalizar la posici�n X de la c�mara
        Vector3 lookAtPosition = new Vector3(fondoBounds.min.x + t * fondoWidth, fondoTransform.position.y, fondoTransform.position.z);

        // Ajustar la rotaci�n de la c�mara para mirar hacia la posici�n calculada en el objeto "Fondo"
        Vector3 directionToLookAt = lookAtPosition - transform.position;
        Quaternion rotationToLookAt = Quaternion.LookRotation(directionToLookAt);
        Vector3 eulerRotationToLookAt = rotationToLookAt.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotationToLookAt.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
