using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public TMPro.TextMeshProUGUI tutorialText;
    private PlayerController playerScript;
    public GameObject arrow;

    private Vector3 tutorialTextPos;
    private Vector3 coinTextPos;
    private Vector3 timerTextPos;


    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObj.GetComponent<PlayerController>();
        if (arrow)
        {
            arrow.SetActive(false);
            timerTextPos = GameObject.Find("TimerText").transform.position + new Vector3(0, -1, 0);
            coinTextPos = GameObject.Find("CoinUI").transform.position - new Vector3(1, 1, 0);
            tutorialTextPos = tutorialText.transform.position + new Vector3(0, 1.5f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GlobalValues.arrowNum);
        if (arrow && !arrow.activeSelf && GlobalValues.arrowNum < 4)
        {
            if (GlobalValues.arrowNum == 1 && GlobalValues.level != 0 && GlobalValues.coins == 1)
            {
                tutorialText.text = "Use coins to spin the wheel";
                 
                arrow.transform.position = coinTextPos- new Vector3(0, 1, 0);

                GlobalValues.arrowNum += 1;
            
                arrow.SetActive(true);
                StartCoroutine(hideArrow());
            }
            else if (GlobalValues.speedOfTime == 2 && !GlobalValues.speedTutorialAlreadyShown)
            {   
               
                tutorialText.text = "If you get all the coins in a room, the timer speeds up!";
               
                arrow.transform.position = timerTextPos - new Vector3(0, 1, 0);
       
                GlobalValues.arrowNum += 1;
          
                arrow.SetActive(true);
                StartCoroutine(hideArrow());
                GlobalValues.speedTutorialAlreadyShown = true;
                
            }
            else if (!GlobalValues.carryOverTutorialAlreadyShown && GlobalValues.level != 1 && GlobalValues.level != 0 && transform.localScale != new Vector3(0, 0, 0) && GlobalValues.coinsFromLastLevel > 0)
            {
            
                tutorialText.text = "Coins carry over to the subsequent rooms!";
          
                arrow.transform.position = coinTextPos - new Vector3(0, 1, 0);
                
                GlobalValues.arrowNum += 1;
         
                
                arrow.SetActive(true);
                GlobalValues.carryOverTutorialAlreadyShown = true;
                StartCoroutine(hideArrow());
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GlobalValues.coins++;
            GlobalValues.coinsCollectedAtCurrentLevel++;

            if (SceneManager.GetActiveScene().name != "Tutorial")
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
            }

            //check if tutorial scene
            if (SceneManager.GetActiveScene().name == "Tutorial" && name == "Coin (1)")
            {
                tutorialText.text = "Great! Now let's try to get rid of that pesky wind. Press E to spin the wheel.";
                GlobalValues.arrowNum = 0;
                Destroy(gameObject);
            }

            else if (SceneManager.GetActiveScene().name == "Tutorial" && name == "Coin")
            {
                tutorialText.text = "Good job! Survive the rooms until the timer runs out! Good luck!";
                arrow.SetActive(true);
                arrow.transform.position = timerTextPos- new Vector3(0, 1, 0);
               

                GlobalValues.arrowNum += 1;
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                playerScript.StartLoadMenuSceneCoroutine(2.5f);
            }
        }
    }

    IEnumerator hideArrow()
    {
        yield return new WaitForSeconds(2);
        arrow.SetActive(false);
        tutorialText.text = "";
    }
}
