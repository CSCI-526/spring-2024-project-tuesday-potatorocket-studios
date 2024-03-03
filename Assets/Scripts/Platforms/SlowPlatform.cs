using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlatform : MonoBehaviour
{
    public float slowdownSpeed = 1f;
    public GameObject Player;
    private float originalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        originalSpeed = Player.GetComponent<PlayerController>().moveSpeed; //Get the original speed
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Player.GetComponent<PlayerController>().moveSpeed = slowdownSpeed; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            Player.GetComponent<PlayerController>().moveSpeed = originalSpeed; 
        }
    }
}
