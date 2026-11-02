using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5;
    public float distanciaSuelo = 1.5f;
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
        Debug.Log(estaEnSuelo);

        if (estaEnSuelo)
        {
            Vector2 movimiento = Vector2.zero;

            if (Keyboard.current.wKey.isPressed)
                movimiento.y = 1;
            if (Keyboard.current.sKey.isPressed)
                movimiento.y = -1;
            if (Keyboard.current.aKey.isPressed)
                movimiento.x = -1;
            if (Keyboard.current.dKey.isPressed)
                movimiento.x = 1;

            //El salto se realiza solo si el jugador esta en el suelo
            if (estaEnSuelo)
                puedeSaltar = true;
            if (Keyboard.current.spaceKey.wasPressedThisFrame && puedeSaltar)
                rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
                puedeSaltar = false;

            Vector3 direccion = transform.forward * movimiento.y + transform.right * movimiento.x;

            rb.MovePosition(rb.position + direccion * velocidad * Time.deltaTime);
        }
    }
}