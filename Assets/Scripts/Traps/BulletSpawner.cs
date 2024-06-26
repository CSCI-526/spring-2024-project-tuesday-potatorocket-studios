using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    enum SpawnerType { Spin, Down, Up, Left, Right, Track }

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletLife = 1f;
    public float speed = 1f;


    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;
    [SerializeField] private float rotationSpeed = 1f;


    private GameObject spawnedBullet;
    private float timer = 0f;
    private bool cooldownCheck = false;
    private float cooldownTimer = 0f;
    public float cooldown;
    private float direction = 1f;
    private GameObject player; // Reference to the player GameObject
    public Transform target;
    private Vector2 trackDirection;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject by tag
        if (player != null) // Check if the player was found
        {
            target = player.transform; // Assign the player's transform to target
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        cooldownTimer += Time.deltaTime;
        //transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);
        float angle = transform.eulerAngles.z;
        //float direction = 1f;

        if (spawnerType == SpawnerType.Down)
        {
            if (angle > 180f)
            {
                angle -= 360f;
            }
            if ((angle < -180f) || (angle >= 0f))
            {
                direction = direction * -1f;
            }
        }
        else if (spawnerType == SpawnerType.Up)
        {
            if (angle > 180f)
            {
                angle -= 360f;
            }
            if ((angle <= 0f) || (angle > 180f))
            {
                direction = direction * -1f;
            }
        }
        else if (spawnerType == SpawnerType.Left)
        {
            if ((transform.localRotation.eulerAngles.z < 90f) || (transform.localRotation.eulerAngles.z > 270f))
            {
                direction = direction * -1f;
            }
        }
        else if (spawnerType == SpawnerType.Right)
        {
            if (angle > 180f)
            {
                angle -= 360f;
            }
            if ((angle < -90f) || (angle > 90f))
            {
                direction = direction * -1f;
            }
        }
        else if (spawnerType == SpawnerType.Track) {
            if (target == null) // Check if the target has been destroyed or is null
            {
                return; // Exit the Update method early if target is null
            }
            trackDirection = target.position - transform.position;
            angle = Mathf.Atan2(trackDirection.y, trackDirection.x) * Mathf.Rad2Deg;
            Quaternion trackRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, trackRotation, rotationSpeed * Time.deltaTime);

        }
        if (spawnerType != SpawnerType.Track) {
            transform.Rotate(0, 0, rotationSpeed * direction * Time.deltaTime);
        }

        //if (timer > cooldown) {
        if ((cooldownCheck == false) && (cooldownTimer > cooldown))
        {
            cooldownCheck = true;
            cooldownTimer = 0;
            timer = 0;
        }
        else if ((cooldownCheck == true) && (cooldownTimer > cooldown))
        {
            cooldownCheck = false;
            cooldownTimer = 0;
            timer = 0;
        }
        if (cooldownCheck == true)
        {
            if (timer >= firingRate)
            {
                Fire();
                timer = 0;
            }
        }
    }

    private void Fire()
    {
        if (bullet)
        {
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.transform.rotation = transform.rotation;
            //print(timer);
        }
    }
}
