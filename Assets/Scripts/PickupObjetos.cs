using UnityEngine;
using UnityEngine.InputSystem;

public class PickupObjetos : MonoBehaviour
{
    public Transform camara;
    public LayerMask capaObjetos;
    public float distanciaMaxima = 2;
    public float distanciaObjeto = 2;
    public float distanciaMinima = 1;

    private GameObject PickupObjeto;
    private Rigidbody fisicasObjeto;
    private bool gravedadOriginal;
    private bool KinematicOriginal;



    void Update()
    {
        if (PickupObjeto != null)
        {
            // Soltar el objeto al dejar de pulsar el clic derecho
            if (!Mouse.current.rightButton.isPressed)
            {
                SoltarObjeto();
            }
            else
            {
                // Mantener el objeto delante de la camara
                PickupObjeto.transform.position = camara.position + camara.forward * distanciaObjeto;
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

    void RecogerObjeto(RaycastHit impacto)
    {
        fisicasObjeto = impacto.rigidbody;
        PickupObjeto = fisicasObjeto.gameObject;

        gravedadOriginal = fisicasObjeto.useGravity;
        KinematicOriginal = fisicasObjeto.isKinematic;

        fisicasObjeto.useGravity = false;
        fisicasObjeto.isKinematic = true;
    }

    void SoltarObjeto()
    {
        fisicasObjeto.useGravity = gravedadOriginal;
        fisicasObjeto.isKinematic = KinematicOriginal;

        PickupObjeto = null;
        fisicasObjeto = null;
    }
}
