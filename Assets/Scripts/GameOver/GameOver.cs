using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    private TimerScript gameManagerScript; // Assuming TimerScript is the correct component name
    private Text timerText;
    private PlayerController playerScript;
    private Analytics analyticsScript; // Reference to the Analytics script
    void Start()
    {
        // Find the GameManager object and TimerScript component once at the start
        GameObject gameManagerObj = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObj.GetComponent<TimerScript>(); // Correct syntax for GetComponent

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObj.GetComponent<PlayerController>();
        analyticsScript = gameManagerObj.GetComponent<Analytics>();
        // Properly initialize timerText
        GameObject textObj = GameObject.Find("Canvas/GameOverPanel/gameOver_Stats");
        if (textObj != null)
        {
            timerText = textObj.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameObject.FindGameObjectWithTag("Player") == null && gameManagerScript != null && timerText != null)
        {
            gameOverPanel.SetActive(true);
            
            // Use the TimerScript component's sliderTimer directly
            float leftTimerValue = Mathf.Floor(gameManagerScript.sliderTimer);
            int coinCount = playerScript.coinCount;
            // Access trapsData from analyticsScript
            Traps trapsData = analyticsScript.TrapsData;

            //timerText.text = "Time Left:" + leftTimerValue.ToString() + "s\nCoin Count:" + coinCount.ToString();
            timerText.text = $"Time Left: {leftTimerValue}s\nCoin Count: {coinCount}\nSpikes damage: {trapsData.spike}\nLasers damage: {trapsData.laser}\nBullets damage: {trapsData.bullet}";

        }
    }

    public void Restart()
    {
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
