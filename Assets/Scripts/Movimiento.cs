using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;
    public float x, y;

    private GameObject objetoRecogido;
    public Transform lugarRecogida;  // Lugar donde se mantendr� el objeto recogido

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * velocidadRotacion, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidad);

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);

        // Check if the player presses the pick up/drop key (e.g., E key)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objetoRecogido != null)
            {
                // Soltar el objeto
                objetoRecogido.transform.parent = null;
                objetoRecogido = null;
            }
            else
            {
                // Intentar recoger un objeto
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2.0f))
                {
                    if (hit.collider.CompareTag("ObjetoRecogible"))
                    {
                        objetoRecogido = hit.collider.gameObject;
                        objetoRecogido.transform.position = lugarRecogida.position;
                        objetoRecogido.transform.parent = lugarRecogida;
                    }
                }
            }
        }
    }
}
