using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool invincible;
    private int invincibilityTime = 1;
    private Image healthBar;
    private float maxHealth = 100;
    private float currentHealth;
    private float healthLerpSpeed = 3;
    private string typeOneTrapTag = "spike";
    private string typeTwoTrapTag = "EnemyBullet";
    private string typeThreeTrapTag = "laser";
    private float typeOneTrapDamage = 10;
    private float typeTwoTrapDamage = 15;
    private float typeThreeTrapDamage = 20;

    public int coinCount;

    public TextMeshProUGUI coinText;


    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;


    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
        invincible = false;
        currentHealth = maxHealth;
        coinCount = 0;
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

        coinText.text = "Coins: " + coinCount;

    }

    void FixedUpdate()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, currentHealth / maxHealth, healthLerpSpeed * Time.deltaTime);
            healthBar.color = Color.Lerp(Color.red, Color.green, currentHealth / maxHealth);
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

       //Vector2 move = value.Get<Vector2>();
         //rb.velocity = move * moveSpeed;

     }*/


    //checks if player is on the ground
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }

        if (!invincible)
        {
            if (theCollision.collider.tag == typeOneTrapTag)
            {
                currentHealth -= typeOneTrapDamage;
                invincible = true;
            }
            else if (theCollision.collider.tag == typeTwoTrapTag)
            {
                currentHealth -= typeTwoTrapDamage;
                invincible = true;
            }

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }

            if (invincible)
            {
                StartCoroutine(MakeVulnerable());
            }
        }

        if (theCollision.gameObject.tag == "Coin")
        {
            //do null check in case coin is fading out when player touches it
            if (theCollision.gameObject != null)
            {
                Destroy(theCollision.gameObject);

            }
            coinCount++;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!invincible)
        {
            currentHealth -= typeThreeTrapDamage;
            invincible = true;
            
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }

            if (invincible)
            {
                StartCoroutine(MakeVulnerable());
            }
        }
    }

    private IEnumerator MakeVulnerable()
    {
        yield return new WaitForSeconds(invincibilityTime);
        invincible = false;
    }
}







