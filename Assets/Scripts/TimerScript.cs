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
        if (SceneManager.GetActiveScene().name != "Tutorial") { StartTimer(); }
        player = GameObject.FindWithTag("Player");
        gameController = GameObject.FindWithTag("GameController");
        analyticsScript = gameController.GetComponent<Analytics>();
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
                timerText.text = $"Time Left: {leftTimerValue}s\nCoin Count: {coinCount}\nSpikes damage: {trapsData.spike}\nLasers damage: {trapsData.laser}\nBullets damage: {trapsData.bullet}";
            }
            slider.value = sliderTimer;
        }

    }

    public void ProceedToNextLevel()
    {
        GlobalValues.level++;
        GlobalValues.coinsFromLastLevel = GlobalValues.coins;
        SceneManager.LoadScene("SceneLevel" + GlobalValues.level.ToString());
    }
}
