using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GlobalValues.level = 1;
        GlobalValues.coin = 0;
        SceneManager.LoadScene("Level1 - Alpha Separate Scene");
    }

      public void PlayTutorial()
    {
        GlobalValues.level = 0;
        GlobalValues.coin = 0;
        SceneManager.LoadScene("Tutorial");
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }
}
