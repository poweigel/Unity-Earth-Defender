using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody objectRb;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        objectRb.AddForce(Vector3.back * speed);

        // If object is too far to the left, destroy the object (good for bullets).
        if (transform.position.z > 50.0f)
        {
            Destroy(gameObject);
        }
    }

    // If object collides with Earth, destroy the object.
    // TODO: Reduce Earth hp based on object hp.
    private void OnCollisionEnter(Collision collision)  // For Rigidbody collisions.
    {
        if (collision.gameObject.CompareTag("Earth"))
        {
            Destroy(gameObject);
        }
    }
}
