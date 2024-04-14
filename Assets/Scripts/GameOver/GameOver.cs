using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverLose;
    private Analytics analyticsScript;

    void Start()
    {
        GameObject gameManagerObj = GameObject.Find("GameManager");
        analyticsScript = gameManagerObj.GetComponent<Analytics>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject levelProgress = GameObject.FindWithTag("LevelProgress");
        if (GameObject.FindGameObjectWithTag("Player") == null && levelProgress == null)
        {
            gameOverPanel.SetActive(true);
            analyticsScript.PublishData();


            analyticsScript.buffsAnalytics.win = 0;
            analyticsScript.PublishBuffsAnalytics();

            // Access trapsData from analyticsScript
            Time.timeScale = 0;
            GlobalValues.slowMoFactor = 1;
            //timerText.text = "Time Left:" + leftTimerValue.ToString() + "s\nCoin Count:" + coinCount.ToString();
            //gameOverLose.text = $"Time Left: {leftTimerValue}s\nCoin Count: {coinCount}\nSpikes damage: {trapsData.spike}\nLasers damage: {trapsData.laser}\nBullets damage: {trapsData.bullet}";
        }
    }

    public void Restart()
    {
        GlobalValues.coins = 0;
        GlobalValues.coinsCollectedAtCurrentLevel = 0;
        GlobalValues.speedOfTime = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void quit()
    {
        Application.Quit();
    }
}
