using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MandatoryTutorial : MonoBehaviour
{
    public Button tutorialButton;
    public Button playButton;
    public Button levelSelectorButton;
    //public static GameObject levelSelectPin;

    public static int gamePlayedFlag = 0;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = tutorialButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick); //Listen for a click

        if (gamePlayedFlag == 0)
        {
            playButton.interactable = false; //Turn off play button at the start
            levelSelectorButton.interactable = false;
        }
        else
        {
            playButton.interactable = true;
            levelSelectorButton.interactable = true;
        }
    }

    void TaskOnClick()
    {
        playButton.interactable = true; //Turn on play button
        levelSelectorButton.interactable = true;
        gamePlayedFlag = 1;
    }
}
