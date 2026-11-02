using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5;
    public float distanciaSuelo = 1.1f;

    private bool estaEnSuelo;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 origenRayo = transform.position + Vector3.up * 0.1f;

        estaEnSuelo = Physics.Raycast(origenRayo, Vector3.down, distanciaSuelo);

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

            Vector3 direccion = transform.forward * movimiento.y + transform.right * movimiento.x;

            rb.MovePosition(rb.position + direccion * velocidad * Time.deltaTime);
        }
    }
}