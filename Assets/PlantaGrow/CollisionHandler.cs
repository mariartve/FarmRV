using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject[] capsules;  // Array to hold references to all capsules
    public float moveSpeed = 0.01f;   // Speed at which the capsules will move

    private bool shouldMoveCapsules = false;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is the sphere
        if (collision.gameObject.CompareTag("Sphere"))
        {
            shouldMoveCapsules = true;
        }
    }

    void Update()
    {
        if (shouldMoveCapsules)
        {
            // Move all capsules upwards
            foreach (GameObject capsule in capsules)
            {
                capsule.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
            }
        }
    }
}
