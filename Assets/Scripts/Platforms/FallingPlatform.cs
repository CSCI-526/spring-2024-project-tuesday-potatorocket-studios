using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private float fallDelay = 1.0f; //Delay for fall
    private float destroyDelay = 2.0f; //Delay for destroying the platform
    [SerializeField] private Rigidbody2D rb;



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());

        }
    }

    private void Update()
    {
        if (transform.position.y < -12)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Fall()
    {

        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        rb.AddForce(new Vector2(0, -5), ForceMode2D.Impulse);

    }
}
