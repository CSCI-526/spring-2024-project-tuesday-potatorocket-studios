using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    public Slider slider;
    public float sliderTimer;
    public bool timerIsRunning = false;
    private Camera mainCamera;
    private GameObject levelProgress;
    public TextMeshProUGUI timerText;
    private GameObject player;
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
                timerIsRunning = false;
                // Destroy(GameObject.FindWithTag("Player"));
                GameObject gameController = GameObject.FindWithTag("GameController");
                levelProgress.SetActive(true);
                gameController.GetComponent<Analytics>().PublishData();
                float leftTimerValue = 0;
                PlayerController playerScript = player.GetComponent<PlayerController>();
                int coinCount = playerScript.coinCount;
                playerScript.currentHealth = playerScript.maxHealth;
                // Access trapsData from analyticsScript
                // StartCoroutine(respawnCoins(gameController));
                Traps trapsData = gameController.GetComponent<Analytics>().TrapsData;
                // gameController.GetComponent<CoinSpawner>().spawnCoins(Random.Range(4, 9));
                //timerText.text = "Time Left:" + leftTimerValue.ToString() + "s\nCoin Count:" + coinCount.ToString();
                timerText.text = $"Time Left: {leftTimerValue}s\nCoin Count: {coinCount}\nSpikes damage: {trapsData.spike}\nLasers damage: {trapsData.laser}\nBullets damage: {trapsData.bullet}";
            }
            slider.value = sliderTimer;
        }

    }

    // IEnumerator respawnCoins(GameObject gameController) {
    //     yield return new WaitForSeconds(10);
    //     Debug.Log(Camera.main.transform.position);
    //     gameController.GetComponent<CoinSpawner>().spawnCoins(Random.Range(4, 9));
    // }


}
