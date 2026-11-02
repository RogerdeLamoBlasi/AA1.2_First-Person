using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5;
    public float distanciaSuelo = 1;
    public float fuerzaSalto = 5;
    public LayerMask capaSuelo;


    private bool puedeSaltar = true;
    private bool estaEnSuelo;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Vector3 origenRay = transform.position + Vector3.up * 0.1f;

        estaEnSuelo = Physics.Raycast(origenRay, Vector3.down, distanciaSuelo, capaSuelo);

        Vector2 movimiento = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            movimiento.y = 1;
        if (Keyboard.current.sKey.isPressed)
            movimiento.y = -1;
        if (Keyboard.current.aKey.isPressed)
            movimiento.x = -1;
        if (Keyboard.current.dKey.isPressed)
            movimiento.x = 1;

        float multiplicador = 1f;


        // velocidad segun la direccion
        if (movimiento.y < 0)
            multiplicador = 0.5f;
        else if (movimiento.y == 0 && movimiento.x != 0)
            multiplicador = 0.75f;

        // Recuperar la posibilidad de saltar al tocar el suelo
        if (estaEnSuelo)
            puedeSaltar = true;

        if (Keyboard.current.spaceKey.wasPressedThisFrame && puedeSaltar)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            puedeSaltar = false;
        }

        // Sprint solo en el suelo y hacia adelante
        if (estaEnSuelo && Keyboard.current.leftShiftKey.isPressed && movimiento.y > 0)
            multiplicador *= 2f;

        // Reducir velocidad en el aire
        if (!estaEnSuelo)
            multiplicador *= 0.5f;

        Vector3 direccion = transform.forward * movimiento.y + transform.right * movimiento.x;

        // Evitar mayor velocidad en diagonal
        direccion = direccion.normalized;

        rb.MovePosition(rb.position + direccion * velocidad * multiplicador * Time.deltaTime);
    }

}
