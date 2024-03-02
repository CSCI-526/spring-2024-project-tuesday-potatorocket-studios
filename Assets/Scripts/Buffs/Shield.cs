using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerScript;
    public float timer = 0f;
    public float shieldTimer;

    // Start is called before the first frame update
    void Start()
    {
        shieldTimer = 10f;
        this.gameObject.SetActive(false);
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shieldTimer) {
            timer = 0;
            playerScript.isShielded = false;
            this.gameObject.SetActive(false);
        }
    }

    /*void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.tag == "Coin")
        {
            //do null check in case coin is fading out when player touches it
            if (theCollision.gameObject != null)
            {
                Destroy(theCollision.gameObject);

            }
            playerScript.coinCount++;
        }
    }*/
}