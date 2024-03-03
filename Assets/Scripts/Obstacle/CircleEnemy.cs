using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 5);
    }
    
    private void FixedUpdate() {
        this.transform.position =
            Vector3.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            Destroy(this.gameObject);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            Destroy(this.gameObject);
        }
    }
}
