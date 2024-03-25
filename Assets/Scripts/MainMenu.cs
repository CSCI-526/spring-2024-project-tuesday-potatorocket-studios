using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        MandatoryTutorial.levelSelectPin.SetActive(true);
        MandatoryTutorial.levelSelectPin.GetComponent<TMPro.TMP_InputField>().ActivateInputField();
    }
}
