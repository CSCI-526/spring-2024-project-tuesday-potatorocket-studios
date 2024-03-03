using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MandatoryTutorial : MonoBehaviour
{
    public Button tutorialButton;
    public Button playButton;

    public static int gamePlayedFlag = 0;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = tutorialButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick); //Listen for a click


        if (gamePlayedFlag == 0)
        {
            playButton.interactable = false; //Turn off play button at the start
        }
        else
        {
            playButton.interactable = true;
        }
    }

    void TaskOnClick()
    {
        playButton.interactable = true; //Turn on play button
        gamePlayedFlag = 1;
    }

}
