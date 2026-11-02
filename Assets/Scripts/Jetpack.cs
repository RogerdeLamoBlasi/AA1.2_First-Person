using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class JetpackJugador : MonoBehaviour
{
    public Image barraJetpack;
    public float fuerzaJetpack = 15;
    public float distanciaSuelo = 1;

    private Rigidbody rb;
    private float combustible = 1;
    private float tiempoRecarga = 0;
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
            if (combustible <= 0)
                tiempoRecarga += Time.deltaTime;

            if (combustible > 0 || tiempoRecarga >= 0.5f)
                combustible += Time.deltaTime / 0.5f;
        }
        else
        {
            tiempoRecarga = 0;
        }

        combustible = Mathf.Clamp01(combustible);

        if (barraJetpack != null)
            barraJetpack.fillAmount = combustible;
    }

    void FixedUpdate()
    {
        if (!estaEnSuelo && Keyboard.current.spaceKey.isPressed && combustible > 0)
        {
            rb.AddForce(Vector3.up * fuerzaJetpack, ForceMode.Acceleration);

            combustible -= Time.fixedDeltaTime;

            if (combustible <= 0)
            {
                combustible = 0;
                tiempoRecarga = 0;
            }
        }
    }
}
