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
            Destroy(gameObject);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "border")
        {
            Destroy(gameObject); // Destroy the bullet when it hits a border
        }

        if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {

            Destroy(gameObject); // Also destroy the bullet
        }
        if (collision.tag == "Shield")
        {
            Destroy(gameObject); // Destroy the bullet when it hits a shield
        }
    }
}
