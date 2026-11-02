using UnityEngine;
using UnityEngine.InputSystem;

public class PickupObjetos : MonoBehaviour
{
    public Transform camara;
    public LayerMask capaObjetos;
    public float distanciaMaxima = 2;
    public float distanciaObjeto = 2;
    public float distanciaMinima = 1;

    // Pickup avanzado
    public float distanciaMaximaEnMano = 4; //No se que nombre ponerle, es la distancia maxima que puedes alejar el objeto con el raton
    public float velocidadRueda = 0.5f;
    public float velocidadSeguimiento = 10;
    public float fuerzaLanzamiento = 5;

    private GameObject PickupObjeto;
    private Rigidbody fisicasObjeto;
    private bool gravedadOriginal;
    private bool KinematicOriginal;

    void Update()
    {
        if (PickupObjeto != null)
        {
            // Soltar el objeto
            if (!Mouse.current.rightButton.isPressed)
            {
                SoltarObjeto();
            }
            else
            {
                // Acercar y alejar con la rueda
                float rueda = Mouse.current.scroll.ReadValue().y / 120f;

                distanciaObjeto -= rueda * velocidadRueda;

                distanciaObjeto = Mathf.Clamp(distanciaObjeto, distanciaMinima, distanciaMaximaEnMano);
            }

            return;
        }

        // Detectar objetos delante de la camara
        RaycastHit impacto;

        if (Physics.Raycast(camara.position, camara.forward, out impacto, distanciaMaxima, capaObjetos))
        {
            Debug.DrawRay(camara.position, camara.forward * distanciaMaxima, Color.green);

            if (Mouse.current.rightButton.isPressed && impacto.rigidbody != null)
            {
                RecogerObjeto(impacto);
            }
        }
    }

    // Mover el objeto utilizando fisicas
    void FixedUpdate()
    {
        if (PickupObjeto != null)
        {
            Vector3 posicionObjetivo = camara.position + camara.forward * distanciaObjeto;

            fisicasObjeto.linearVelocity = (posicionObjetivo - fisicasObjeto.position) * velocidadSeguimiento;
        }
    }

    void RecogerObjeto(RaycastHit impacto)
    {
        fisicasObjeto = impacto.rigidbody;
        PickupObjeto = fisicasObjeto.gameObject;

        gravedadOriginal = fisicasObjeto.useGravity;
        KinematicOriginal = fisicasObjeto.isKinematic;

        fisicasObjeto.useGravity = false;
        fisicasObjeto.isKinematic = false;
    }

    void SoltarObjeto()
    {
        // Restaurar las fisicas originales
        fisicasObjeto.useGravity = gravedadOriginal;
        fisicasObjeto.isKinematic = KinematicOriginal;

        // Lanzar el objeto
        if (!KinematicOriginal)
        {
            fisicasObjeto.AddForce(camara.forward * fuerzaLanzamiento, ForceMode.Impulse);
        }

        PickupObjeto = null;
        fisicasObjeto = null;
    }
}
