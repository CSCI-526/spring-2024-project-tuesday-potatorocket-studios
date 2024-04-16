using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TimerScript : MonoBehaviour
{

    private Slider slider;
    public float sliderTimer;
    private bool timerIsRunning = false;
    private GameObject levelProgress;
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
        string sceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
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
        if (Input.GetKeyDown(KeyCode.Tab) && wheelModal != null)
        {
            wheelModal.SetActive(!wheelModal.activeSelf);
            timerIsRunning = !timerIsRunning;
            StartCoroutine(UpdateTimer());
            Time.timeScale = (Time.timeScale + 1) % 2;
        }
    }

    //ticks down the timer
    IEnumerator UpdateTimer()
    {
        while (timerIsRunning)
        {
            sliderTimer -= Time.deltaTime * GlobalValues.speedOfTime * GlobalValues.slowMoFactor;
            yield return new WaitForSeconds(.001f);

            if (player == null)
            {
                timerIsRunning = false;
            }

            if (sliderTimer <= 0)
            {
                Time.timeScale = 0;
                levelProgress.SetActive(true);
                analyticsScript.PublishData();
                analyticsScript.buffsAnalytics.win = 1;
                analyticsScript.PublishBuffsAnalytics();
                GlobalValues.slowMoFactor = 1;
               // gameOverWin.text = $"Time Left: {leftTimerValue}s\nCoin Count: {coinCount}\nSpikes damage: {trapsData.spike}\nLasers damage: {trapsData.laser}\nBullets damage: {trapsData.bullet}";
            }
            slider.value = sliderTimer;
        }

    }

    public void ProceedToNextLevel()
    {
        GlobalValues.level++;
        GlobalValues.coinsFromLastLevel = GlobalValues.coins;
        GlobalValues.speedOfTime = 1;
        GlobalValues.coinsCollectedAtCurrentLevel = 0;
        SceneManager.LoadScene("SceneLevel" + GlobalValues.level.ToString());
    }
}
