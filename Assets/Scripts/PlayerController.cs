using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float speed = 10.0f;
    private float verticalBound = 9.0f;
    public float jumpSpeed = 500.0f;
    public bool isOnGround = true;
    Rigidbody playerRb;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        JumpPlayer();
        ConstrainPlayerPosition();
    }

    // Moves the player based on arrow key input
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float vertivalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.forward * speed * vertivalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
    }
    // Jump the player based on "Space" key input
    void JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
            isOnGround = false;
        }
    }
    // Prevent the player from leaving the top or bottom of the screen
    void ConstrainPlayerPosition()
    {
        if (transform.position.z < -verticalBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -verticalBound);
        }
        if (transform.position.z > verticalBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, verticalBound);
        }
    }
    // Prevent the player spam the jump button
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
}
