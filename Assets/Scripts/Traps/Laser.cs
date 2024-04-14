using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform target;
    private Vector2 direction;
    public float rotationSpeed = 1;
    public Vector2 raycastDirection;
    public LineRenderer Line;
    public float activationTimer = 0f;
    private float timer = 0f;
    private GameObject player; // Reference to the player GameObject

    private bool cooldownCheck = false;
    private float cooldownTimer = 0f;
    public float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Find the player GameObject by tag
        if (player != null) // Check if the player was found
        {
            target = player.transform; // Assign the player's transform to target
        }
        cooldown = 3;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        cooldownTimer += Time.deltaTime;

        if (target == null) // Check if the target has been destroyed or is null
        {
            return; // Exit the Update method early if target is null
        }
        if ((cooldownCheck == false) && (cooldownTimer > cooldown)) {
            cooldownCheck = true;
            cooldownTimer = 0;
            Line.enabled = true;
            timer = 0;
        } else if ((cooldownCheck == true) && (cooldownTimer > cooldown)) {
            cooldownCheck = false;
            cooldownTimer = 0;
            Line.enabled = false;
            timer = 0;
        }
        if (cooldownCheck == true) {
            // Calculate the direction and rotation to face the target
            direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);

            // Perform the raycast and set the line renderer positions
            raycastDirection = transform.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection);
            Line.SetPosition(0, transform.position);

            // Determine the end point of the laser line
            Vector3 lineEndPoint = hit.collider != null ? hit.point : (Vector2)transform.position + raycastDirection * 1000;
            Line.SetPosition(1, lineEndPoint);

            // Check for collisions with the player
            if (hit.collider != null && hit.collider.gameObject.tag == "Player")
            {
                var def = hit.collider.gameObject.GetComponent<PlayerController>().defense;
                hit.collider.gameObject.GetComponent<PlayerController>().TakeDamage(20 - def);
            }
            timer = 0;
        }
    }
}