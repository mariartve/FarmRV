using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoTake : MonoBehaviour
{
    public float grabDistance = 2.0f;  // Distancia a la que se puede agarrar un objeto
    public KeyCode grabKey = KeyCode.G;  // Tecla para agarrar el objeto
    public KeyCode releaseKey = KeyCode.R;  // Tecla para soltar el objeto

    private GameObject grabbedObject;

    // Update is called once per frame
    void Update()
    {
        // Detectar entrada de la tecla G para agarrar objetos
        if (Input.GetKeyDown(grabKey))
        {
            if (grabbedObject == null)
            {
                // Intentar agarrar un objeto
                TryGrabObject();
            }
        }

        // Detectar entrada de la tecla R para soltar objetos
        if (Input.GetKeyDown(releaseKey))
        {
            if (grabbedObject != null)
            {
                // Soltar el objeto
                ReleaseObject();
            }
        }
    }

    private void TryGrabObject()
    {
        // Raycast hacia adelante para intentar encontrar un objeto para agarrar
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance))
        {
            if (hit.collider.gameObject.CompareTag("Apple"))
            {
                grabbedObject = hit.collider.gameObject;
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;  // Desactivar física
                    rb.useGravity = false;  // Desactivar gravedad
                }
                grabbedObject.transform.SetParent(transform);
            }
        }
    }

    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;  // Activar física
                rb.useGravity = true;  // Activar gravedad
            }
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = collision.gameObject.AddComponent<Rigidbody>();
            }
            rb.useGravity = true;
        }
    }
}
