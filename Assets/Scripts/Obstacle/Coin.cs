using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class Coin : MonoBehaviour
{


  
    public TMPro.TextMeshProUGUI tutorialText;
    private PlayerController playerScript; 

   
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObj.GetComponent<PlayerController>();
        

         
    }

    // Update is called once per frame
    void Update()
    {

      
    }

        public void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.tag == "Player")
        {
            
            

            playerScript.coinCount++;
            GlobalValues.coin = playerScript.coinCount;
            
            if (SceneManager.GetActiveScene().name != "Tutorial"){
                Destroy(gameObject);
             }


            //check if tutorial scene
            if (SceneManager.GetActiveScene().name == "Tutorial" && name == "Coin (1)")
           
            {
                    tutorialText.text = "Great! Now let's try to get rid of that pesky wind. Press F to spin the wheel, you might get lucky.";
                    Destroy(gameObject);
            }
                 

            else if (SceneManager.GetActiveScene().name == "Tutorial" && name == "Coin")
            {
                    tutorialText.text = "Good job! Now you are ready to play. Survive in the next room with traps and monsters. Collect coins so you can use the wheel for help!";
                   playerScript.StartLoadMenuSceneCoroutine(4.0f);
                   Destroy(gameObject);
                
            }

    }
    }

   


   

  
}
