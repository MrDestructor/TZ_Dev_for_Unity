using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPooledCoin
{
    public CoinPooler.CoinsInfo.CoinsType CoinsType => type;   //Implementing the interface 

    [SerializeField]
    private CoinPooler.CoinsInfo.CoinsType type;   // We return the value of the variable since we cannot change it through the inspector

    private float lifeTime = 5;
    private float currentLifeTime;

    public void OnCreate(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        currentLifeTime = lifeTime;
    }

    private void Update()
    {
        if ((currentLifeTime -= Time.deltaTime) < 0)
            CoinPooler.Instance.DestroyCoin(gameObject);
    }
}
