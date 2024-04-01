using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public GameObject player;
    public float lifespan;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        lifespan = GlobalValues.level == 4 ? 13 : 10;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifespan);
    }
    
    private void FixedUpdate() {
        if (player != null)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
