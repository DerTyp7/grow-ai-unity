using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    TextMeshProUGUI moneyText;

    void Start()
    {
        TimeManager.OnTimeUpdate += OnTimeUpdate;
        moneyText = GetComponent<TextMeshProUGUI>();
    }

    void OnTimeUpdate()
    {
        if (moneyText != null)
            moneyText.text = EconomyManager.instance.GetMoneyStr();
    }
}
