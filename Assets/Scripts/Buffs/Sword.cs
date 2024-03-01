using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    private float speed = 75f;

    public GameObject sword;

    public Animator anim;

    private float timer;

   // public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(-0.4f, 0, 0);
        //this.transform.eulerAngles = new Vector3(0, 0, -45);
        this.transform.position = player.transform.position + offset;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        /*this.transform.Rotate(Vector3.forward, speed * Time.deltaTime);

         if (this.transform.eulerAngles.z >= 45 & this.transform.eulerAngles.z <= 135)
         {
             resetSlash();
         }*/

    }

    private void LateUpdate()
    {
        this.transform.position = player.transform.position + offset;
    }


    private void resetSlash()
    {
        offset *= -1;
        this.transform.Rotate(0,180,0);
        this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, -45);
        this.transform.position = player.transform.position + offset;
    }

}
