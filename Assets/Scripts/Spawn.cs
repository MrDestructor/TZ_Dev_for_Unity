using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private CoinPooler.CoinsInfo.CoinsType CoinsType;
    [SerializeField]
    private Transform spawnPosition;

    private CoinBalance balance;

    private void Start()
    {
        balance = GetComponent<CoinBalance>();
    }

    public void SpawnCoin()
    {
        var xPos = Random.Range(-0.6f, 0.6f);   // Random deviation along the x-axis 
        var zPos = Random.Range(-0.6f, 0.6f);   //Random deviation along the z-axis 

        var coin = CoinPooler.Instance.GetCoin(CoinsType);

        balance.currentBalance--;   // subtract the balance of coins

        if (balance.currentBalance != 0)
            coin.GetComponent<Coin>().OnCreate(spawnPosition.position + new Vector3(xPos, spawnPosition.position.y, zPos), transform.rotation);   // Spawn coin
    }
}
