using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    private Slider slider;
    public float sliderTimer;
    private bool timerIsRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        slider = GameObject.Find("SliderTimer").GetComponent<Slider>();
        slider.maxValue = sliderTimer;
        slider.value = sliderTimer;

        StartTimer();
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
            if (sliderTimer <= 0)
            {
                timerIsRunning = false;
            }

            slider.value = sliderTimer;
        }
    }


}
