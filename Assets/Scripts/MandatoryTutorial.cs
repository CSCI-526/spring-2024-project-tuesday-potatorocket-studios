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
    public static GameObject levelSelectPin;

    public static int gamePlayedFlag = 0;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = tutorialButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick); //Listen for a click
        levelSelectPin = GameObject.Find("LevelSelectPin");
        levelSelectPin.SetActive(false);

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

    public void CheckPin(string s)
    {
        if (levelSelectPin.GetComponent<TMP_InputField>().text.Equals("1234"))
        {
            SceneManager.LoadScene("LevelSelectScene");
        }
        else
        {
            levelSelectPin.SetActive(false);
        }
    }
}
