using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinWinBoss : MonoBehaviour
{
    public GameObject coin;
    public bool coinSpawner;
    // Start is called before the first frame update
    void Start()
    {
        coinSpawner = false;
        StartCoroutine(SpawnCoin());
    }
    IEnumerator SpawnCoin()
    {
        while (true)
        {
            if (coinSpawner)
            {
                GameObject.Find("CoinSpawner (1)").GetComponent<CoinWinBoss>().coinSpawner = true;
                GameObject.Find("CoinSpawner (2)").GetComponent<CoinWinBoss>().coinSpawner = true;
                Instantiate(coin, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(0f);
        }
    }
}
