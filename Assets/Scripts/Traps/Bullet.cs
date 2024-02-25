using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) {
            Destroy(this.gameObject);
        } else {
            timer += Time.deltaTime;
            transform.position = Movement(timer);
        }
    }
    private Vector2 Movement(float timer) {
        //Moves right based on bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.tag == "border") {
        Destroy(this.gameObject); // Destroy the bullet when it hits a border
    }
    if (gameObject.tag == "PlayerBullet") {
        if (collision.tag == "Enemy") {
            Destroy(collision.gameObject); // Destroy the enemy when hit by a player bullet
            Destroy(this.gameObject); // Also destroy the bullet
        }
    } else if (gameObject.tag == "EnemyBullet") {
        if (collision.tag == "Player") {
            Destroy(collision.gameObject); // Destroy the player when hit by an enemy bullet
            Destroy(this.gameObject); // Also destroy the bullet
        }
        if (collision.tag == "PlayerBullet") {
            // This condition seems redundant with the above condition within the "EnemyBullet" check
            // Consider removing it or implementing specific logic for when a bullet hits another bullet
            Destroy(this.gameObject); // Destroy the enemy bullet when it hits a player bullet
        }
    }
}
}
