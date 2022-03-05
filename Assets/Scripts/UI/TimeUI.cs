using System;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    TextMeshProUGUI dateTimeText;

    private void Start()
    {
        TimeManager.OnTimeUpdate += OnTimeUpdate;
        dateTimeText = GetComponent<TextMeshProUGUI>();
    }

    void OnTimeUpdate()
    {
        DateTime dateTime = TimeManager.instance.GetDateTime();
        if (dateTimeText != null)
            dateTimeText.text = dateTime.ToString("hh:mm tt / dd.MM.yyyy", TimeManager.instance.cultureInfo);
    }
}