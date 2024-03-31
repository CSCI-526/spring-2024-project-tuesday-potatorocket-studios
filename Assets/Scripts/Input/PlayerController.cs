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
    public bool invincible;
    private int invincibilityTime = 1;
    private Image healthBar;
    public float maxHealth = 100;
    public float currentHealth;
    private float healthLerpSpeed = 3;
    private string typeOneTrapTag = "spike";
    private string typeTwoTrapTag = "EnemyBullet";
    //private string typeThreeTrapTag = "laser";
    private float typeOneTrapDamage = 10;
    private float typeTwoTrapDamage = 15;
    private float typeThreeTrapDamage = 20;
    private GameObject gameManager;
    private int invincibleFlag = 0;


    public bool isShielded;

    public TextMeshProUGUI coinText;
    [SerializeField] public TextMeshProUGUI tutorialText;


    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;
    private int numCoins;


    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Image>();
        invincible = false;
        currentHealth = maxHealth;
        coinText = GameObject.Find("CoinUI").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager");
        InvokeRepeating("SavePlayerLocation", 0, 1);
        numCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
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

        coinText.text = "Coins: " + GlobalValues.coins.ToString();

    }

    private void SavePlayerLocation()
    {
        if (GameObject.FindWithTag("LevelProgress") == null && gameObject != null)
        {
            Analytics analyticsScript = gameManager.GetComponent<Analytics>();
            if (analyticsScript.playerLocationAnalytics != null)
            {
                analyticsScript.playerLocationAnalytics.x = transform.position.x;
                analyticsScript.playerLocationAnalytics.y = transform.position.y;
                analyticsScript.PublishPlayerLocationAnalytics();
            }
        }
    }

    void FixedUpdate()
    {
        if (GlobalValues.coins - GlobalValues.coinsFromLastLevel == numCoins)
        {
            GlobalValues.speedOfTime = 2f;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.fillAmount = 0.0f;
            Destroy(gameObject);
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, Math.Max(currentHealth, 0) / maxHealth, healthLerpSpeed * Time.deltaTime);
            healthBar.color = Color.Lerp(Color.red, Color.green, Math.Max(currentHealth, 0) / maxHealth);
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
    
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            if (theCollision.contacts[0].normal.y == 1) {
                isGrounded = true;
            }
        }

        if (!invincible && (isShielded == false))
        {
            if (theCollision.collider.tag == typeOneTrapTag)
            {
                gameManager.GetComponent<Analytics>().updateTrapData("spike");
                currentHealth -= typeOneTrapDamage;
                invincible = true;
            }
            else if (theCollision.collider.tag == typeTwoTrapTag)
            {
                gameManager.GetComponent<Analytics>().updateTrapData("enemybullet");
                currentHealth -= typeTwoTrapDamage;
                invincible = true;
            }
            else if (theCollision.collider.tag == "Enemy")
            {
                gameManager.GetComponent<Analytics>().updateTrapData("enemymonster");
                Destroy(theCollision.gameObject);
                currentHealth -= 25;
                invincible = true;
            }

            if (invincible)
            {
                StartCoroutine(MakeVulnerable());
            }
        }
    }

    //called from coin script to load the menu after the tutorial
    public void StartLoadMenuSceneCoroutine(float waitTime)
    {
        StartCoroutine(LoadMenuSceneCoroutine(waitTime));
    }

    IEnumerator LoadMenuSceneCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("MenuScene");

    }

    public void TakeDamage(float damage)
    {
        if (!invincible)
        {
            gameManager.GetComponent<Analytics>().updateTrapData("laser");
            currentHealth -= typeThreeTrapDamage;
            invincible = true;

            if (invincible)
            {
                StartCoroutine(MakeVulnerable());
            }
        }
    }

    private IEnumerator MakeVulnerable()
    {
        yield return new WaitForSeconds(invincibilityTime);
        if (GameObject.FindWithTag("LevelProgress") == null)
        {
            invincible = false;
        }
    }
}







