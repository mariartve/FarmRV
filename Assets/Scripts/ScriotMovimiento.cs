using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureRealisticPropsPack : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 5.0f;
    private float verticalRotation = 0.0f;
    public float maxVerticalAngle = 60.0f;
    public float moveSpeed = 5.0f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        // Obtener el componente Rigidbody
        rb = GetComponent<Rigidbody>();
        // Asegurarse de que el Rigidbody tenga las restricciones adecuadas para la rotación
        rb.freezeRotation = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up * mouseX * rotationSpeed);

            float mouseY = Input.GetAxis("Mouse Y");
            verticalRotation -= mouseY * rotationSpeed;
            verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);
            transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        // Al colisionar, detener el movimiento del Rigidbody
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
