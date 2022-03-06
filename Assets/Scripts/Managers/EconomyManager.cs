using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    [SerializeField]
    float money = 0.0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddMoney(float value) => money += value;
    public float GetMoney() => money;
    public string GetMoneyStr() => money.ToString() + " €";
    public void SetMoney(float value) => money = value;
    public void RemoveMone(float value) => money -= value;
}
