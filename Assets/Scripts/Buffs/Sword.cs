using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private GameObject player;

    public float speed = 75f;

    private GameObject[] enemies;

   // public Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //this.transform.eulerAngles = new Vector3(0, 0, -45);
        this.transform.position = player.transform.position;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    private void FixedUpdate()
    {
        if (enemies != null)
        {

            var idx = 0;
            var closest = 10000000000000000f;
            var i = 0;
            foreach (var e in enemies)
            {

                var d = Vector3.Distance(this.transform.position, e.transform.position);
                if (d < closest)
                {
                    closest = d;
                    idx = i;
                }

                i++;
            }
            //this.transform.LookAt(enemies[idx].transform.position);

            transform.right = transform.position - enemies[idx].transform.position;
        }
    }

    private void LateUpdate()
    {
        this.transform.position = player.transform.position;
    }

}
