using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private bool isGrounded;

    [SerializeField] private float moveSpeed;


    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    //jump with spacebar or W
    private void OnJump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        }

    }

    private void OnWASD(InputValue value)
    {

        Vector2 move = value.Get<Vector2>();

        rb.velocity = move * moveSpeed;

    }


    //checks if player is on the ground
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.name == "Ground")
        {

            isGrounded = true;

        }
    }






}
