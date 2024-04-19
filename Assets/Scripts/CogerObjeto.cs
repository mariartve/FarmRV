using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerObjeto : MonoBehaviour
{
     public KeyCode teclaAgarrar = KeyCode.E; 

    private GameObject objetoAgarrado;
    private Rigidbody rbObjetoAgarrado;
    private bool estaAgarrando = false;

    void Update()
    {
        // Si presionas la tecla para agarrar
        if (Input.GetKeyDown(teclaAgarrar))
        {
            if (estaAgarrando)
            {
                SoltarObjeto();
            }
            else
            {
                AgarrarObjeto();
            }
        }
    }

    void AgarrarObjeto()
    {
        // Raycast para detectar objetos
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            objetoAgarrado = hit.transform.gameObject;
            rbObjetoAgarrado = objetoAgarrado.GetComponent<Rigidbody>();

            if (rbObjetoAgarrado != null)
            {
                rbObjetoAgarrado.isKinematic = true; // Hacer que el objeto no responda a la física mientras lo agarramos
                objetoAgarrado.transform.SetParent(transform); // Hacer que el objeto sea hijo del jugador para seguirlo
                objetoAgarrado.transform.localPosition = Vector3.zero; // Colocar el objeto en la posición local del jugador
                estaAgarrando = true;
            }
        }
    }

    void SoltarObjeto()
    {
        if (objetoAgarrado != null && rbObjetoAgarrado != null)
        {
            rbObjetoAgarrado.isKinematic = false; // Permitir que el objeto responda nuevamente a la física
            objetoAgarrado.transform.SetParent(null); // Desvincular el objeto del jugador
            objetoAgarrado = null;
            rbObjetoAgarrado = null;
            estaAgarrando = false;
        }
    }





    void Update()
    {
        
    }
}
using UnityEngine;


   