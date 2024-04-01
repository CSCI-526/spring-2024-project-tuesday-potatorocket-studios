using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int height;

    public int speed;

    public int waitTime;

    private float currentLocation;

    private float initialLocation;

    private Vector3 direction;

    private bool wait;

    private float waitTimer;
    // Start is called before the first frame update
    void Start()
    {
        currentLocation = this.transform.position.y;
        initialLocation = this.transform.position.y;
        direction = Vector3.up;
        wait = false;
        //waitTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLocation - initialLocation >= height)
        {
            direction = Vector3.down;
            wait = true;
        }

        if (currentLocation - initialLocation < 0)
        {
            direction = Vector3.up;
            wait = true;
        }

        if (wait)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                wait = false;
                waitTimer = 0;
            }
        }
        
    }
    
    void LateUpdate()
    {
        if (!wait)
        {
            this.transform.Translate(direction * speed * Time.deltaTime);
            currentLocation = this.transform.position.y;
        }
    }
    
}
