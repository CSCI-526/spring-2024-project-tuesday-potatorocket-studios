using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public string levelName;
    public int levelNumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenScene() {
        GlobalValues.level = levelNumber;
        GlobalValues.coin = 0;
        SceneManager.LoadScene(levelName.ToString());
    }
}
