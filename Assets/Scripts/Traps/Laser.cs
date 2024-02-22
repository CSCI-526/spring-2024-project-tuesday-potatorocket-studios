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

    // Start is called before the first frame update
    void Start()
    {
        //this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        /*if (timer >= activationTimer) {
            this.enabled = true;
        }*/
        direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        raycastDirection = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection);
        Line.SetPosition(0, transform.position);
        Line.SetPosition(1, hit.point);
        if (hit.collider != null) {
            if (hit.collider.gameObject.tag == "Player") {

            }
        }
    }
}
