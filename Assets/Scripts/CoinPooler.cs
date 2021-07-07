using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPooler : MonoBehaviour
{
    public static CoinPooler Instance;


    [Serializable]
    public struct CoinsInfo
    {
        public enum CoinsType  // many types of coins can be added
        {
            GOLD_Coin
            //OTHER_Coin
        }

        public int CoinCount;  // number of coins to spawn
        public GameObject CoinPrefab;
        public CoinsType TypeCoin;  // type of coin
    }

    [SerializeField]
    private List<CoinsInfo> coinsInfo; // Declaring the CoinsInfo structure

    private Dictionary<CoinsInfo.CoinsType, PoolContainer> poolContainers;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        InitCoinPool();
    }

    private void InitCoinPool()
    {
        poolContainers = new Dictionary<CoinsInfo.CoinsType, PoolContainer>();
        var emptyCoin = new GameObject();

        foreach (var coin in coinsInfo)   // We place each object in a container and add it to the pool
        {
            var container = Instantiate(emptyCoin, transform, false);
            container.name = coin.ToString();

            poolContainers[coin.TypeCoin] = new PoolContainer(container.transform);

            for (int i = 0; i < coin.CoinCount; i++)
            {
                var coinPrefab = InstantiateCoinPrefab(coin.TypeCoin, container.transform);
                poolContainers[coin.TypeCoin].Coins.Enqueue(coinPrefab);
            }
        }

        Destroy(emptyCoin);
    }

    private GameObject InstantiateCoinPrefab(CoinsInfo.CoinsType type, Transform parent)    // Instantiating initial prefabs
    {
        var coinPrefab = Instantiate(coinsInfo.Find(x => x.TypeCoin == type).CoinPrefab, parent);
        coinPrefab.SetActive(false);
        return coinPrefab;
    }

    public GameObject GetCoin(CoinsInfo.CoinsType type)
    {
        var coinPrefab = poolContainers[type].Coins.Count > 0 ?
            poolContainers[type].Coins.Dequeue() : InstantiateCoinPrefab(type, poolContainers[type].Container);

        coinPrefab.SetActive(true);

        return coinPrefab;
    }

    public void DestroyCoin(GameObject coin)
    {
        poolContainers[coin.GetComponent<IPooledCoin>().CoinsType].Coins.Enqueue(coin);
        coin.SetActive(false);
    }
}
