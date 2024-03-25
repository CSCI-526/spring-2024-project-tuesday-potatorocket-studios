using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TimerScript : MonoBehaviour
{

    private Slider slider;
    public float sliderTimer;
    private bool timerIsRunning = false;
    private GameObject levelProgress;
    public TextMeshProUGUI gameOverWin;
    private GameObject player;
    private PlayerController playerScript;
    private GameObject gameController;
    private Analytics analyticsScript;
    private GameObject wheelModal;
    // Start is called before the first frame update
    void Start()
    {
        levelProgress = GameObject.FindWithTag("LevelProgress");
        levelProgress.SetActive(false);
        slider = GameObject.Find("SliderTimer").GetComponent<Slider>();
        slider.maxValue = sliderTimer;
        slider.value = sliderTimer;
        wheelModal = GameObject.Find("WheelModal");
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        String sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Tutorial")
        {
            GlobalValues.level = 0;
            //playerScript.invincible = true;
        }
        else
        {
            GlobalValues.level = sceneName[sceneName.Length - 1] - '0';
            if (GlobalValues.level == 1)
            {
                wheelModal.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                wheelModal.SetActive(false);
                StartTimer();
            }
        }
        gameController = GameObject.FindWithTag("GameController");
        analyticsScript = gameController.GetComponent<Analytics>();
    }

    public void StartTimer()
    {
        timerIsRunning = true;
        StartCoroutine(UpdateTimer());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && wheelModal != null)
        {
            wheelModal.SetActive(!wheelModal.activeSelf);
            timerIsRunning = !timerIsRunning;
            StartCoroutine(UpdateTimer());
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    //ticks down the timer
    IEnumerator UpdateTimer()
    {
        while (timerIsRunning)
        {
            sliderTimer -= Time.deltaTime * GlobalValues.speedOfTime;
            yield return new WaitForSeconds(.001f);

            if (player == null)
            {
                timerIsRunning = false;
            }

            if (sliderTimer <= 0)
            {
                Traps trapsData = analyticsScript.TrapsData;
                timerIsRunning = false;
                player.GetComponent<PlayerController>().invincible = true;
                levelProgress.SetActive(true);
                analyticsScript.PublishData();
                float leftTimerValue = 0;
                int coinCount = GlobalValues.coins;
                gameOverWin.text = $"Time Left: {leftTimerValue}s\nCoin Count: {coinCount}\nSpikes damage: {trapsData.spike}\nLasers damage: {trapsData.laser}\nBullets damage: {trapsData.bullet}";
            }
            slider.value = sliderTimer;
        }

    }

    public void ProceedToNextLevel()
    {
        GlobalValues.level++;
        GlobalValues.coinsFromLastLevel = GlobalValues.coins;
        GlobalValues.speedOfTime = 1;
        SceneManager.LoadScene("SceneLevel" + GlobalValues.level.ToString());
    }
}
