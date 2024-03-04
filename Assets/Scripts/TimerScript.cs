using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    private Slider slider;
    public float sliderTimer;
    private int lastLevel = 2;
    private bool timerIsRunning = false;
    private Camera mainCamera;
    private GameObject levelProgress;
    public TextMeshProUGUI timerText;
    private GameObject player;
    private GameObject gameController;
    private Analytics analyticsScript;
    // Start is called before the first frame update
    void Start()
    {
        levelProgress = GameObject.FindWithTag("LevelProgress");
        levelProgress.SetActive(false);
        slider = GameObject.Find("SliderTimer").GetComponent<Slider>();
        slider.maxValue = sliderTimer;
        slider.value = sliderTimer;
        mainCamera = Camera.main;
        StartTimer();
        player = GameObject.FindWithTag("Player");
        gameController = GameObject.FindWithTag("GameController");
        analyticsScript = gameController.GetComponent<Analytics>();
        analyticsScript.trapsData.level = GlobalValues.level;
    }

    public void StartTimer()
    {
        timerIsRunning = true;
        StartCoroutine(UpdateTimer());
    }


    //ticks down the timer
    IEnumerator UpdateTimer()
    {
        while (timerIsRunning)
        {
            sliderTimer -= Time.deltaTime;
            yield return new WaitForSeconds(.001f);

            if (player == null)
            {
                timerIsRunning = false;
            }

            if (sliderTimer <= 0)
            {
                Traps trapsData = analyticsScript.TrapsData;
                // if (GlobalValues.level == lastLevel) {
                //     Destroy(player);
                // } else {
                    timerIsRunning = false;
                    player.GetComponent<PlayerController>().invincible = true;
                    levelProgress.SetActive(true);
                    analyticsScript.PublishData();
                    float leftTimerValue = 0;
                    PlayerController playerScript = player.GetComponent<PlayerController>();
                    int coinCount = playerScript.coinCount;
                    timerText.text = $"Time Left: {leftTimerValue}s\nCoin Count: {coinCount}\nSpikes damage: {trapsData.spike}\nLasers damage: {trapsData.laser}\nBullets damage: {trapsData.bullet}";
                // }
            }
            slider.value = sliderTimer;
        }

    }

    public void ProceedToNextLevel()
    {
        /*Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + 58, 0, -10);
        GameObject.FindWithTag("LevelProgress").SetActive(false);
        player.transform.position = new Vector3(Camera.main.transform.position.x, -5, 0);
        player.GetComponent<PlayerController>().currentHealth = 100;
        gameController.GetComponent<CoinSpawner>().spawnCoins(Random.Range(4, 9));
        int level = analyticsScript.trapsData.level;
        analyticsScript.trapsData = new Traps{level = level + 1};
        analyticsScript.flag = true;
        sliderTimer = 30;
        slider.value = sliderTimer;
        slider.maxValue = sliderTimer;
        timerIsRunning = true;
        StartTimer();*/
        
        PlayerController playerScript = player.GetComponent<PlayerController>();
        GlobalValues.coin = playerScript.coinCount;
        GlobalValues.level++;
        SceneManager.LoadScene("Level2-Alpha");

    }
}
