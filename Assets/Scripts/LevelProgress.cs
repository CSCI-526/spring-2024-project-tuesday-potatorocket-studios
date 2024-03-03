using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProceedToNextLevel()
    {
        Camera.main.transform.position = new Vector3(58, 0, -10);
        GameObject.FindWithTag("LevelProgress").SetActive(false);
        GameObject.FindWithTag("Player").transform.position = new Vector3(58, -5, 0);
        GameObject gameController = GameObject.FindWithTag("GameController");
        gameController.GetComponent<CoinSpawner>().spawnCoins(Random.Range(4, 9));
        Analytics analyticsScript = gameController.GetComponent<Analytics>();
        int level = analyticsScript.trapsData.level;
        analyticsScript.trapsData = new Traps();
        analyticsScript.trapsData.level = level + 1;
        analyticsScript.flag = true;
        TimerScript timerScript = gameController.GetComponent<TimerScript>();
        timerScript.sliderTimer = 30;
        timerScript.slider.value = timerScript.sliderTimer;
        timerScript.slider.maxValue = timerScript.sliderTimer;
        timerScript.timerIsRunning = true;
        timerScript.StartTimer();
    }
}
