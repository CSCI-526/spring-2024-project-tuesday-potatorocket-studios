using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MandatoryTutorial : MonoBehaviour
{
    public Button tutorialButton;
    public Button playButton;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = tutorialButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick); //Listen for a click
        playButton.interactable = false; //Turn off play button at the start
        
    }

    void TaskOnClick(){
        playButton.interactable = true; //Turn on play button
    }
    
}
