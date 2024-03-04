using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using Unity.VisualScripting;
using UnityEngine;


public class Traps
{
    public string guid;
    public int spike;
    public int laser;
    public int bullet;
    public int level;
    public int enemy;
}

public class WheelAnalytics {
    public string guid;
    public int timeRemaining;
    public int coinCount;
    public int level;
    public int spinCount;
    public int healthLeft;
}

public class Analytics : MonoBehaviour
{

    public Traps trapsData;
    
    public bool flag;
    // Public getter for trapsData
    public Traps TrapsData => trapsData;
    public WheelAnalytics wheelAnalytics;
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalValues.guid == null)
        {
            GlobalValues.guid = System.Guid.NewGuid().ToString();
        }
        trapsData = new Traps{ level = GlobalValues.level, guid = GlobalValues.guid};
        wheelAnalytics = new WheelAnalytics{ level = GlobalValues.level, guid = GlobalValues.guid };
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PublishWheelAnalytics() {
        wheelAnalytics.coinCount = GlobalValues.coin;
        wheelAnalytics.spinCount += 1;
        wheelAnalytics.timeRemaining = (int) GameObject.FindWithTag("GameController").GetComponent<TimerScript>().sliderTimer;
        wheelAnalytics.healthLeft = (int) GameObject.FindWithTag("Player").GetComponent<PlayerController>().currentHealth;
        RestClient.Post("https://rogueroulette-efcf5-default-rtdb.firebaseio.com/Wheel/.json", JsonUtility.ToJson(wheelAnalytics));
    }

    public void PublishData() {
        if (flag)
        {
            RestClient.Post("https://rogueroulette-efcf5-default-rtdb.firebaseio.com/Traps/.json", JsonUtility.ToJson(trapsData));
            flag = false;
        }
    }


    public void updateTrapData(string trap)
    {
        if (trap == "spike")
        {
            trapsData.spike += 1;
        }
        else if (trap == "laser")
        {
            trapsData.laser += 1;
        }
        else if (trap == "enemybullet")
        {
            trapsData.bullet += 1;
        }
        else if (trap == "enemymonster")
        {
            trapsData.enemy += 1;
        }
    }
}
