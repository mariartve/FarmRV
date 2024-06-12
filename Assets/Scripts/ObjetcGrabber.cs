
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetcGrabber : MonoBehaviour
{
    public float grabDistance = 2.0f;  // Distancia máxima a la que se puede agarrar un objeto
    public KeyCode grabKey = KeyCode.Mouse0;  // Tecla para agarrar el objeto

    private GameObject grabbedObject; // Objeto actualmente agarrado

    void Update()
    {
        // Detectar entrada de la tecla para agarrar objetos
        if (Input.GetKeyDown(grabKey))
        {
            // Intentar agarrar un objeto
            TryGrabObject();
        }
    }

    private void TryGrabObject()
    {
        // Raycast hacia adelante desde la posición de la mano para intentar encontrar un objeto para agarrar
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabDistance))
        {
            if (hit.collider.gameObject.CompareTag("Apple") || hit.collider.gameObject.CompareTag("Seed"))
            {
                // Si el objeto es una semilla o una manzana, agárralo
                grabbedObject = hit.collider.gameObject;
                Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;  // Desactivar física para que el objeto no se caiga
                    rb.useGravity = false;  // Desactivar gravedad para que el objeto no sea atraído hacia abajo
                }
                grabbedObject.transform.SetParent(transform); // Hacer que el objeto sea hijo de la mano para seguir su movimiento
            }
        }
    }
}
