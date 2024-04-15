using System.Collections;
using TMPro;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    private float RotatePower;
    private float StopPower;
    private Rigidbody2D rbody;
    private int spinning;

    private float wheelTimer = 0f;
    private float wheelInterval = 0.1f; // Interval for rotation in seconds so player can't spam

    private float smallDelay;
    private float randomPower;
    private int bossBullet;

    private KeyCode spinKey = KeyCode.E; // Key that spins the wheel
    public GameObject Player;
    public GameObject bulletSpawner;
    public GameObject sword;
    public GameObject gameManager;

    public TextMeshProUGUI wheelText;

    private TextMeshProUGUI tutorialTextObject;

    private float originalJumpForce;

    private Analytics analyticsScript;

    public GameObject Shield;
    private static string[,] WHEEL_ITEMS = new string[7, 8] {{"NOWIND","NOWIND","NOWIND", "NOWIND","NOWIND","NOWIND", "NOWIND","NOWIND"}, // Tutorial
                                                             {"NOWIND","SHIELD","DEFENSE", "HEAL","COINS","SLOWMO", "ADDBULLET","JUMP"},
                                                             {"NOWIND", "SLOWMO","DEFENSE", "HEAL","COINS","REMOVEBULLET", "ADDBULLET","JUMP"},
                                                             {"NOWIND", "SHIELD","DEFENSE", "HEAL","COINS","REMOVEBULLET", "ADDBULLET","SLOWMO"},
                                                             {"SLOWMO", "JUMP","DEFENSE", "HEAL","COINS","REMOVEBULLET", "ADDBULLET","SWORD"},
                                                             {"SLOWMO", "SHIELD","DEFENSE", "HEAL","COINS","REMOVEBULLET", "ADDBULLET","JUMP"},
                                                             {"SLOWMO", "SHIELD","DEFENSE", "HEAL","COINS","REMOVEBULLET", "ADDBULLET","SWORD"}};
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        wheelTimer = 0f;
        spinning = 0;
        RotatePower = 400;
        StopPower = 150;
        Player = GameObject.FindWithTag("Player");
        gameManager = GameObject.FindWithTag("GameController");
        wheelText = GameObject.Find("WheelOutcome").GetComponent<TextMeshProUGUI>();
        bossBullet = 0;
        tutorialTextObject = GameObject.Find("TutorialText").GetComponent<TextMeshProUGUI>();
        StartCoroutine(MakeTextDisappear());
        analyticsScript = gameManager.GetComponent<Analytics>();
    }

    private void Update()
    {
        wheelTimer += Time.deltaTime;

        //If player has no coins, they can't spin the wheel
        if (Player != null && Input.GetKeyDown(spinKey) && GlobalValues.coins == 0)
        {
            wheelText.text = "Not enough coins to spin!";
            StartCoroutine(MakeTextDisappear());
        }

        if (gameManager.GetComponent<TimerScript>().sliderTimer > 1 && Player != null && Input.GetKeyDown(spinKey) && GlobalValues.coins > 0 && wheelTimer - wheelInterval > 1 && spinning == 0)
        { //Spin the wheel
            spinning = 1;
            randomPower = Random.Range(-200f, 200f); //Add random power so that you don't land on the same section every spin
            rbody.AddTorque(RotatePower + randomPower);
            GlobalValues.coins -= 1;
            gameManager.GetComponent<Analytics>().PublishWheelAnalytics();
            //Player.GetComponent<PlayerController>().coinText.text = "Coins: " + Player.GetComponent<PlayerController>().coinCount;
        }

        if (rbody.angularVelocity > 0)
        { //Slow the wheel
            rbody.angularVelocity -= StopPower * Time.deltaTime;
        }

        if (rbody.angularVelocity <= 0)
        { //If we get negative speed, we stop it at 0
            rbody.angularVelocity = 0;
        }

        if (rbody.totalTorque == 0 && rbody.angularVelocity == 0 && spinning == 1)
        { //Wheel is stopped
            smallDelay += 1 * Time.deltaTime; //To prevent update speed issues. Otherwise it thinks the exact moment we sping the wheel as stopped
            if (smallDelay >= 0.1f)
            {
                //print("Stopped");
                wheelTimer = 0f;
                spinning = 0;
                GetColor();
                smallDelay = 0;
            }
        }
    }

    //public PlayerController playerController;
    private void GetColor()
    {
        string wheelItem = WHEEL_ITEMS[GlobalValues.level, (int)transform.eulerAngles.z / 45];

        if (wheelItem.Equals("NOWIND"))
        {
            GameObject wind = GameObject.Find("Wind");
            if (wind != null)
            {
                wheelText.text = "Yay! Temporarily removed wind";
                analyticsScript.buffsAnalytics.removeWind += 1;
                wind.SetActive(false);
                if (GlobalValues.level != 0)
                {
                    StartCoroutine(activateWind(10, wind));
                }
            }

            if (GlobalValues.level == 0 && tutorialTextObject != null)
            {
                tutorialTextObject.text = "Nice! Now go for that coin!";
            }
        }
        else if (wheelItem.Equals("SLOWMO")) // Slow motion
        {
            GlobalValues.slowMoFactor = 2;
            Camera.main.SetReplacementShader(Shader.Find("Unlit/Grayscale"), "RenderType");
            PlayerController playerController = Player.GetComponent<PlayerController>();
            Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
            analyticsScript.buffsAnalytics.slowMo += 1;
            wheelText.text = "Slow motion!";
            Time.timeScale /= GlobalValues.slowMoFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            playerController.moveSpeed *= 2f;
            playerController.jumpForce *= 2f;
            rb.gravityScale *= 4f;
            StartCoroutine(MakeTextDisappear());
            StartCoroutine(DisableSlowMo(3, playerController, rb));
        }
        else if (wheelItem.Equals("ADDBULLET"))
        {
            bossBullet += 1;
            Instantiate(bulletSpawner, new Vector3(Camera.main.transform.position.x + 10, Camera.main.transform.position.y - 1, 0), Quaternion.identity);
            if (bossBullet == 1)
            {
                wheelText.text = "Boo, boss bullet trap added.";
            }
            else
            {
                wheelText.text = "Boss bullet trap has been buffed!.";
            }
        }
        else if (wheelItem.Equals("SHIELD"))
        {
            analyticsScript.buffsAnalytics.shield += 1;
            activateShield();
            wheelText.text = "Got a shield!";
        }
        else if (wheelItem.Equals("COINS"))
        {
            GlobalValues.coins += 2;
            wheelText.text = "Got 2 coins!";
            analyticsScript.buffsAnalytics.coins += 1;
        }
        else if (wheelItem.Equals("REMOVEBULLET"))
        {
            BulletSpawner[] spin = FindObjectsOfType<BulletSpawner>();
            if (spin != null)
            {
                analyticsScript.buffsAnalytics.removeBullet += 1;
                foreach (BulletSpawner s in spin)
                {
                    s.cooldown = 0;
                    StartCoroutine(disableCooldown(5, s));
                }
                wheelText.text = "Bullets temporarily deactivated!";
            }
        }
        else if (wheelItem.Equals("SWORD"))
        {
            analyticsScript.buffsAnalytics.sword += 1;
            if (GameObject.FindWithTag("Sword") == null)
            {
                Instantiate(sword, Player.transform.position, Quaternion.identity);
            }
            wheelText.text = "Use the sword to kill the enemy!";
        }
        else if (wheelItem.Equals("JUMP"))
        {
            analyticsScript.buffsAnalytics.jump += 1;
            originalJumpForce = Player.GetComponent<PlayerController>().jumpForce;
            Player.GetComponent<PlayerController>().jumpForce *= 1.5f;
            StartCoroutine(jumpCooldown(7));
            wheelText.text = "Increased jump temporarily!";
        }
        else if (wheelItem.Equals("DEFENSE"))
        {
            analyticsScript.buffsAnalytics.defense += 1;
            Player.GetComponent<PlayerController>().defense += 3;
            wheelText.text = "Increased defense!";
        }
        else if (wheelItem.Equals("HEAL"))
        {
            analyticsScript.buffsAnalytics.heal += 1;
            Player.GetComponent<PlayerController>().maxHealth += 25;
            Player.GetComponent<PlayerController>().currentHealth += 25;
            wheelText.text = "Increased Health!";
        }
        StartCoroutine(MakeTextDisappear());
    }

    IEnumerator MakeTextDisappear()
    {
        yield return new WaitForSeconds(3);
        wheelText.text = "";
    }

    IEnumerator disableCooldown(int time, BulletSpawner spin)
    {
        yield return new WaitForSeconds(time);
        spin.cooldown = 1;
    }

    IEnumerator activateWind(int time, GameObject wind)
    {
        yield return new WaitForSeconds(time);
        wind.SetActive(true);
    }

    IEnumerator jumpCooldown(int time)
    {

        yield return new WaitForSeconds(time);
        Player.GetComponent<PlayerController>().jumpForce = originalJumpForce;
    }

    public void activateShield()
    {
        Shield.SetActive(true);
        Shield.GetComponent<Shield>().timer = 0;
        Player.GetComponent<PlayerController>().isShielded = true;
    }

    IEnumerator DisableSlowMo(int time, PlayerController playerController, Rigidbody2D rb)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale *= GlobalValues.slowMoFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        playerController.moveSpeed /= 2f;
        playerController.jumpForce /= 2f;
        rb.gravityScale /= 4f;
        GlobalValues.slowMoFactor = 1;
        Camera.main.ResetReplacementShader();
    }
}