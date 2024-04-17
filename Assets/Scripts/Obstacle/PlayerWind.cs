using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWind : MonoBehaviour
{
    private List<GameObject> winds;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        winds = new List<GameObject>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        foreach (GameObject go in winds) {
            rb.AddForce(go.GetComponent<Wind>().direction * go.GetComponent<Wind>().strength, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wind"))
        {
            winds.Add(other.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wind"))
        {
            winds.Remove(other.gameObject);
        }
    }
}
