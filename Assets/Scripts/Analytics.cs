using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using Unity.VisualScripting;
using UnityEngine;


public class Traps
{
    public int spike;
    public int laser;
    public int bullet;
}

public class Analytics : MonoBehaviour
{

    private Traps trapsData;
    
    private bool flag;
    // Start is called before the first frame update
    void Start()
    {
        trapsData = new Traps();
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag && GameObject.FindGameObjectWithTag("Player") == null)
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
    }
}