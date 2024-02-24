using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private bool isGrounded;

    private int coinCount = 0;

    private TextMeshProUGUI coinText;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;


    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;

        coinText = GameObject.Find("CoinUI").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            Vector3 movement = new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);

        }
    }


    //jump with spacebar or W
    private void OnJump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

    }



    /* Refactored for continuous movement in update function instead 
    private void OnWASD(InputValue value)
     {

        Vector2 move = value.Get<Vector2>();

        rb.velocity = move * moveSpeed;

     }*/






    //checks if player is on the ground
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        //checks if player is on the ground
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

            isGrounded = true;

        }
        //checks if player picked up a coin
        if (theCollision.gameObject.tag == "Coin")
        {
            //do null check in case coin is fading out when player touches it
            if (theCollision.gameObject != null)
            {
                Destroy(theCollision.gameObject);

            }
            coinCount++;
            coinText.text = "Coins: " + coinCount;
        }
    }



}
