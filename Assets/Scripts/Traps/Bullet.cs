using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float speed = 1f;
    public float rotation = 0f;
    private GameObject player;

    private Vector2 spawnPoint;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
        player = GameObject.FindGameObjectWithTag("Player"); //new

        gameObject.tag = "EnemyBullet";
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife)
        {
            Destroy(this.gameObject);
        }
        else
        {
            timer += Time.deltaTime;
            transform.position = Movement(timer);
        }
    }
    private Vector2 Movement(float timer)
    {
        //Moves right based on bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "border")
        {
            Destroy(this.gameObject); // Destroy the bullet when it hits a border
        }



        if (collision.gameObject.layer == 3)
        {
            Destroy(this.gameObject);
        }
        if (collision.collider.tag == "Player")
        {

            Destroy(this.gameObject); // Also destroy the bullet
        }

    }
}
