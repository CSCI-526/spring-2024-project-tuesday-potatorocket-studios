using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour
{
    private float RotatePower;
    private float StopPower;

    private Rigidbody2D rbody;
    private int spinning;

    private float wheelTimer = 0f;
    private float wheelInterval = 5f; // Interval for rotation in seconds

        float smallDelay;
    

    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        wheelTimer = 0f; 
        spinning = 0;
        RotatePower = 800;
        StopPower = 100;
    }

    private void Update()
    {           
        wheelTimer += Time.deltaTime;
        if(wheelTimer - wheelInterval > 1 && spinning == 0){ //Spin the wheel
            spinning = 1;
            rbody.AddTorque(RotatePower);
        }

        if (rbody.angularVelocity > 0) { //Slow the wheel
            rbody.angularVelocity -= StopPower*Time.deltaTime;
        }

        if (rbody.angularVelocity <= 0) { //If we get negative speed, we stop it at 0
            rbody.angularVelocity = 0;
        }

        if(rbody.angularVelocity == 0 && spinning == 1){ //Wheel is stopped

            smallDelay +=1*Time.deltaTime; //To prevent update speed issues. Otherwise it thinks the exact moment we sping the wheel as stopped
            if(smallDelay >= 0.1f){
                print("Stopped");
                wheelTimer = 0f; 
                spinning = 0;
                smallDelay = 0;
            }

        }

    }

}