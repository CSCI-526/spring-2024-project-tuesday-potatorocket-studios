using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    public GameObject coinPrefab;


    // Start is called before the first frame update
    void Start()
    {
        //spawn random number of coins between 3 and 8
        int numCoins = Random.Range(3, 8);

        spawnCoins(numCoins);
    }

    void spawnCoins(int numCoins)
    {
        for (int i = 0; i < numCoins; i++)
        {
            //spawn coins at random positions in lower part of screen (can change position later depending on level design)
            Vector3 coinPos = new Vector3(Random.Range(-8, 8), Random.Range(-3, 1), 0);
            var coin = Instantiate(coinPrefab, coinPos, Quaternion.identity);
            coin.gameObject.layer = 2;

            //StartCoroutine(FadeCoin(coin));

        }
    }


    // Update is called once per frame
    void Update()
    {

    }

    //fade the coin out and destroy it after 5 seconds
    IEnumerator FadeCoin(GameObject coin)
    {
        yield return new WaitForSeconds(10);

        if (coin != null)
        {

            //fade coin out, null checks in case player already collided with it
            for (float f = 1f; f >= 0; f -= 0.1f)
            {
                if (coin != null)
                {
                    Color c = coin.GetComponent<SpriteRenderer>().color;
                    c.a = f;
                    coin.GetComponent<SpriteRenderer>().color = c;

                    yield return new WaitForSeconds(.1f);
                }
            }
        }

        if (coin != null) { Destroy(coin); }

    }



}