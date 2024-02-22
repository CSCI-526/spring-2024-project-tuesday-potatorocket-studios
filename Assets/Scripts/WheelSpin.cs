using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    private float RotatePower;
    private float StopPower;

    private Rigidbody2D rbody;
    private int spinning;

    private float wheelTimer = 0f;
    private float wheelInterval = 5f; // Interval for rotation in seconds

    private float smallDelay;
    private float randomPower;
    

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
            randomPower = Random.Range(-200f, 200f); //Add random power so that you don't land on the same section every spin
            rbody.AddTorque(RotatePower + randomPower);
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
                //print("Stopped");
                GetColor();
                wheelTimer = 0f; 
                spinning = 0;
                smallDelay = 0;
            }

        }

    }

    private void GetColor(){
        float mySector = transform.eulerAngles.z;
        if (mySector > 0 && mySector <= 45)
        {
            print("Dark Blue");
        }
        else if (mySector > 45 && mySector <= 90)
        {
            print("Blue");
        }
        else if (mySector > 90 && mySector <= 135)
        {
            print("Dark Green");
        }
        else if (mySector > 135 && mySector <= 180)
        {
            print("Green");
        }
        else if (mySector > 180 && mySector <= 225)
        {
            print("Yellow");
        }
        else if (mySector > 225 && mySector <= 270)
        {
            print("Orange");
        }
        else if (mySector > 270 && mySector <= 315)
        {
            print("Red");
        }
        else if (mySector > 315 && mySector <= 360)
        {
            print("Pink");
        }
    }

}