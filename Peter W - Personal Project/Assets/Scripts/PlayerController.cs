using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float horizontalInput;
    public float verticalInput;
    private float zBoundLeft = 20.0f;  // Horizontal axis maximum (leftmost).
    private float xBound = 6.0f;    // Vertical axis range (top and bottom).
    public GameObject bulletPrefab;
    private Rigidbody playerRb;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        MovePlayer();
        ConstrainPlayerPosition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + transform.forward * 1, bulletPrefab.transform.rotation);
        }
    }

    // Moves the player based on arrow key input.
    void MovePlayer()
    {
        // Move player when "Horizontal" and "Vertical" keys are pressed.
        horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(Vector3.back * speed * horizontalInput);
        //transform.Translate(Vector3.back * horizontalInput * Time.deltaTime * speed);
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.right * speed * verticalInput);
        //transform.Translate(Vector3.right * verticalInput * Time.deltaTime * speed);
    }

    // Prevent the player from leaving the bounds of the screen.
    void ConstrainPlayerPosition()
    {
        // Prevent player from moving off the screen.
        if (transform.position.z > zBoundLeft)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundLeft);
        }

        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
    }

    // If player collides with enemy, destroy the enemy and the player.
    // TODO: Only destroy enemy if enemy's health is lower than that of the player.
    private void OnCollisionEnter(Collision collision)  // For Rigidbody collisions.
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Debug.Log("Player has collided with enemy.");
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    // If player collides with powerup, destroy the powerup.
    private void OnTriggerEnter(Collider other) // For trigger-based collisions.
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
        }
    }
}
