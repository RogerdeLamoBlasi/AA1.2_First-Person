using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoCamara : MonoBehaviour
{
    public Transform camara;
    public float sensibilidadRaton = 100f;

    private float rotacionVertical = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Obtener el movimiento del ratón
        Vector2 movimientoRaton = Mouse.current.delta.ReadValue();

        // Calcular la rotación horizontal y vertical
        float ratonX = movimientoRaton.x * sensibilidadRaton * Time.deltaTime;
        float ratonY = movimientoRaton.y * sensibilidadRaton * Time.deltaTime;
        
        transform.Rotate(Vector3.up * ratonX);
        rotacionVertical -= ratonY;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -90f, 90f);

        camara.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);
    }
}
