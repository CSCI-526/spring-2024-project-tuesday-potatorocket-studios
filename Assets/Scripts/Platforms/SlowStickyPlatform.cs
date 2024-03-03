using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowStickyPlatform : MonoBehaviour
{
    public GameObject Player;
    private float originalSpeed;
    private float originalJumpForce;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        originalSpeed = Player.GetComponent<PlayerController>().moveSpeed; //Get the original speed
        originalJumpForce = Player.GetComponent<PlayerController>().jumpForce; //Get the original speed
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Player.GetComponent<PlayerController>().moveSpeed = 1;
            Player.GetComponent<PlayerController>().jumpForce = 0f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Player.GetComponent<PlayerController>().moveSpeed = originalSpeed; 
            Player.GetComponent<PlayerController>().jumpForce = originalJumpForce;
        }
    }
}
