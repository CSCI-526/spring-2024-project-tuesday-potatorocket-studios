using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GlobalValues.level = 1;
        GlobalValues.coins = 0;
        SceneManager.LoadScene("SceneLevel1");
    }

      public void PlayTutorial()
    {
        GlobalValues.level = 0;
        GlobalValues.coins = 0;
        SceneManager.LoadScene("Tutorial");
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }
}
