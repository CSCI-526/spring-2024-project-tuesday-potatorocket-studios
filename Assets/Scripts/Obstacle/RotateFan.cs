using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFan : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 200;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(Vector3.right * Time.deltaTime * speed);
    }
}
