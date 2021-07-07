using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBalance : MonoBehaviour
{
    [SerializeField]
    private Text balanceText;
    private static int balance = 30;

    [HideInInspector]
    public int currentBalance;

    private void Start()
    {
        currentBalance = balance;
    }

    private void Update()
    {
        if (currentBalance == 0)   // Check if the balance of coins is not equal to zero
            currentBalance = balance;

        balanceText.text = currentBalance.ToString();
    }
}
