using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JetpackJugador : MonoBehaviour
{
    public Image barraJetpack;
    public float fuerzaJetpack = 15f;
    public float distanciaSuelo = 1f;

    private Rigidbody rb;
    private float combustible = 1f;
    private float tiempoRecarga = 0f;
    private bool estaEnSuelo;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 origenRay = transform.position + Vector3.up * 0.1f;
        estaEnSuelo = Physics.Raycast(origenRay, Vector3.down, distanciaSuelo);

        if (estaEnSuelo)
        {
            if (combustible <= 0f)
                tiempoRecarga += Time.deltaTime;

            if (combustible > 0f || tiempoRecarga >= 0.5f)
                combustible += Time.deltaTime / 0.5f;
        }
        else
        {
            tiempoRecarga = 0f;
        }

        combustible = Mathf.Clamp01(combustible);

        if (barraJetpack != null)
            barraJetpack.fillAmount = combustible;
    }

    void FixedUpdate()
    {
        if (!estaEnSuelo && Keyboard.current.spaceKey.isPressed && combustible > 0f)
        {
            rb.AddForce(Vector3.up * fuerzaJetpack, ForceMode.Acceleration);

            combustible -= Time.fixedDeltaTime;

            if (combustible <= 0f)
            {
                combustible = 0f;
                tiempoRecarga = 0f;
            }
        }
    }
}
