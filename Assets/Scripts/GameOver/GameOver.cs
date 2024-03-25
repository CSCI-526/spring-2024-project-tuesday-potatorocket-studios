using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    private TimerScript gameManagerScript;
    public TextMeshProUGUI gameOverLose;
    private PlayerController playerScript;
    private Analytics analyticsScript;

    void Start()
    {

        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObj.GetComponent<TimerScript>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObj.GetComponent<PlayerController>();
        analyticsScript = gameManagerObj.GetComponent<Analytics>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject levelProgress = GameObject.FindWithTag("LevelProgress");
        if (GameObject.FindGameObjectWithTag("Player") == null && levelProgress == null)
        {
            gameOverPanel.SetActive(true);
            gameManagerScript.GetComponent<Analytics>().PublishData();

            float leftTimerValue = Mathf.Ceil(gameManagerScript.sliderTimer);
            int coinCount = GlobalValues.coins;
            // Access trapsData from analyticsScript
            Traps trapsData = analyticsScript.TrapsData;

            //timerText.text = "Time Left:" + leftTimerValue.ToString() + "s\nCoin Count:" + coinCount.ToString();
            gameOverLose.text = $"Time Left: {leftTimerValue}s\nCoin Count: {coinCount}\nSpikes damage: {trapsData.spike}\nLasers damage: {trapsData.laser}\nBullets damage: {trapsData.bullet}";
        }
    }

    public void Restart()
    {
        GlobalValues.coins = 0;
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
