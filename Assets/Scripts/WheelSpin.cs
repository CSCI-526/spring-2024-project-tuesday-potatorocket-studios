using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private KeyCode spinKey = KeyCode.F; // Key that spins the wheel
    public GameObject Player;
    public GameObject bulletSpawner;
    public GameObject sword;
    public GameObject gameManager;

    public TextMeshProUGUI wheelText;

    public GameObject Shield;
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
    }

    private void Update()
    {
        wheelTimer += Time.deltaTime;

        //If player has no coins, they can't spin the wheel
        if (Player != null && Input.GetKeyDown(spinKey) && Player.GetComponent<PlayerController>().coinCount == 0)
        {
            wheelText.text = "Not enough coins to spin!";
            StartCoroutine(ChangeText());
        }



        if (gameManager.GetComponent<TimerScript>().sliderTimer > 1 && Player != null && Input.GetKeyDown(spinKey) && Player.GetComponent<PlayerController>().coinCount > 0 && wheelTimer - wheelInterval > 1 && spinning == 0)
        { //Spin the wheel
            spinning = 1;
            randomPower = Random.Range(-200f, 200f); //Add random power so that you don't land on the same section every spin
            rbody.AddTorque(RotatePower + randomPower);
            Player.GetComponent<PlayerController>().coinCount -= 1;
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

        if (rbody.angularVelocity == 0 && spinning == 1)
        { //Wheel is stopped

            smallDelay += 1 * Time.deltaTime; //To prevent update speed issues. Otherwise it thinks the exact moment we sping the wheel as stopped
            if (smallDelay >= 0.1f)
            {
                //print("Stopped");
                GetColor();
                wheelTimer = 0f;
                spinning = 0;
                smallDelay = 0;
            }

        }


    }

    //public PlayerController playerController;
    private void GetColor()
    {
        float mySector = transform.eulerAngles.z;
        // float mySector = 150; //For testing purposes

        //Remove the wind for tutorial level
        if(SceneManager.GetActiveScene().name == "Tutorial"){
            mySector = 1;
        }


        if (mySector > 0 && mySector <= 45)
        {

            //playerController.moveSpeed = 50f;
            print("Green5");

            GameObject wind = GameObject.Find("Wind");
            if (wind != null)
            {
                wheelText.text = "Yay! Temporarily removed wind";
                wind.SetActive(false);
                StartCoroutine(activateWind(10, wind));
            }

        }
        else if (mySector > 45 && mySector <= 90)
        {

            //playerController.moveSpeed = 50f;
            print("Green6");

            GameObject wind = GameObject.Find("Wind (1)");
            if (wind != null)
            {
                wheelText.text = "Yessss! Removed wind";
                wind.SetActive(false);
                StartCoroutine(activateWind(10, wind));
            }
        }
        else if (mySector > 90 && mySector <= 135)
        {
            //playerController.moveSpeed = 50f;
            print("Red1");
            if (Player != null)
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

        }
        else if (mySector > 135 && mySector <= 180)
        {

            print("Green1");

            if (Player != null)
            {
                activateShield();
                wheelText.text = "Got a shield!";
            }
        }
        else if (mySector > 180 && mySector <= 225)
        {
            print("Green2");
            if (GlobalValues.level == 2 && GameObject.FindWithTag("Sword") == null)
            {
                Instantiate(sword, Player.transform.position, Quaternion.identity);
            }
            else if (Player != null)
            {
                Player.GetComponent<PlayerController>().coinCount += 10;
                wheelText.text = "Got 10 coins!";
            }
        }
        else if (mySector > 225 && mySector <= 270)
        {
            print("Green3");

            BulletSpawner[] spin = FindObjectsOfType<BulletSpawner>();
            if (spin != null)
            {
                foreach (BulletSpawner s in spin)
                {
                    s.cooldown = 0;
                    StartCoroutine(disableCooldown(5, s));
                }

                wheelText.text = "Bullets temporarily deactivated!";
            }


        }
        else if (mySector > 270 && mySector <= 315)
        {
            print("Red2");
            
            bossBullet += 1;
            //Player.GetComponent<PlayerController>().moveSpeed *= 0.9f;
            Instantiate(bulletSpawner, new Vector3(10, -1, 0), Quaternion.identity);
            if (bossBullet == 1)
            {
                wheelText.text = "Boo, boss bullet trap added.";
            }
            else
            {
                wheelText.text = "Boss bullet trap has been buffed!.";
                    
            }

        }
        else if (mySector > 315 && mySector <= 360)
        {
            print("Green4");
            
            if (GlobalValues.level == 2 && GameObject.FindWithTag("Sword") == null)
            {
                Instantiate(sword, Player.transform.position, Quaternion.identity);
            }
            else if (Player != null)
            {
                Player.GetComponent<PlayerController>().coinCount += 10;
                wheelText.text = "Got 10 coins!";
            }
        }
        StartCoroutine(ChangeText());
    }

    IEnumerator ChangeText()
    {
        yield return new WaitForSeconds(3);
        wheelText.text = "Press F to Spin";
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

    public void activateShield()
    {
        Shield.SetActive(true);
        Shield.GetComponent<Shield>().timer = 0;
        Player.GetComponent<PlayerController>().isShielded = true;
    }
}