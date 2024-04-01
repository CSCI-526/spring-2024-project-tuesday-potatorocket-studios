using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    public TMPro.TextMeshProUGUI tutorialText;
    private PlayerController playerScript;
    public GameObject arrow;
    public GameObject arrowBody;
    public GameObject arrowHead;
    private LineRenderer arrowBodyRenderer;
    private int arrowNum = 0;
    private Vector3 tutorialTextPos;
    private Vector3 coinTextPos;
    private Vector3 timerTextPos;
    private KeyCode proceedKeycode = KeyCode.Return;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObj.GetComponent<PlayerController>();
        if (GlobalValues.level == 0)
        {
            arrowBodyRenderer = arrowBody.GetComponent<LineRenderer>();
            arrowBodyRenderer.startWidth = 0.13f;
            arrowBodyRenderer.endWidth = 0.13f;
            arrowBodyRenderer.startColor = Color.white;
            arrowBodyRenderer.endColor = Color.white;
            arrow.SetActive(false);
            timerTextPos = GameObject.Find("TimerText").transform.position + new Vector3(0, -1, 0);
            coinTextPos = GameObject.Find("CoinUI").transform.position - new Vector3(1, 1, 0);
            tutorialTextPos = tutorialText.transform.position + new Vector3(0, 1.5f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GlobalValues.level == 0)
        {
            if (arrowNum == 1 && Input.GetKeyDown(proceedKeycode))
            {
                tutorialText.text = "Coins carry over and are used to spin the wheel.\n\nPress Enter to continue.";
                arrowHead.transform.position = coinTextPos;
                arrowHead.transform.eulerAngles = new Vector3(0, 0, 180 - Vector2.Angle(tutorialTextPos, coinTextPos));
                arrowBodyRenderer.SetPosition(0, tutorialTextPos);
                arrowBodyRenderer.SetPosition(1, coinTextPos);
                arrowNum += 1;
            }
            else if (arrowNum == 2 && Input.GetKeyDown(proceedKeycode))
            {
                tutorialText.text = "If you get all the coins, the timer speeds up!\n\nPress Enter to continue.";
                arrowNum += 1;
            }
            else if (arrowNum == 3 && Input.GetKeyDown(proceedKeycode))
            {
                arrowBodyRenderer.positionCount = 0;
                arrow.SetActive(false);
                tutorialText.text = "Good luck!";
                playerScript.StartLoadMenuSceneCoroutine(2.0f);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GlobalValues.coins++;

            if (SceneManager.GetActiveScene().name != "Tutorial")
            {
                Destroy(gameObject);
            }

            //check if tutorial scene
            if (SceneManager.GetActiveScene().name == "Tutorial" && name == "Coin (1)")
            {
                tutorialText.text = "Great! Now let's try to get rid of that pesky wind. Press E to spin the wheel.";
                Destroy(gameObject);
            }

            else if (SceneManager.GetActiveScene().name == "Tutorial" && name == "Coin")
            {
                tutorialText.text = "Good job! Now survive the rooms until the timer runs out\n\nPress Enter to continue.";
                arrow.SetActive(true);
                arrowHead.transform.position = timerTextPos;
                arrowBodyRenderer.SetPosition(0, tutorialTextPos);
                arrowBodyRenderer.SetPosition(1, timerTextPos);
                arrowHead.transform.Rotate(new Vector3(0, 0, Vector2.Angle(tutorialTextPos, timerTextPos)));
                arrowNum += 1;
                gameObject.transform.localScale = new Vector3(0, 0, 0);
            }
        }
    }
}
