using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private float timer;

    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 4)
        {
            int loc = Random.Range(-1, 1);
            //enemy from left
            if (loc == -1)
            {
                var enemyLoc = new Vector3(-16, Random.Range(-8, 5), 0);
                Instantiate(enemy, enemyLoc, Quaternion.identity);
            }
            else if (loc == 0)
            {
                var enemyLoc = new Vector3(Random.Range(-13, 16), 8, 0);
                Instantiate(enemy, enemyLoc, Quaternion.identity);
            }
            else if (loc == 1)
            {
                var enemyLoc = new Vector3(16, Random.Range(-8, 5), 0);
                Instantiate(enemy, enemyLoc, Quaternion.identity);
            }
            
            timer = 0f;
        }
    }
}
