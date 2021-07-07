using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolContainer : MonoBehaviour
{
    public Transform Container { get; private set; }  // Container to prevent coins from appearing randomly in the project hierarchy

    public Queue<GameObject> Coins;

    public PoolContainer(Transform container)
    {
        Container = container;
        Coins = new Queue<GameObject>();
    }
}
