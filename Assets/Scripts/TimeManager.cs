using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static Action OnTimeUpdate;
    public static Action OnSecondUpdate;
    public static Action OnMinuteUpdate;
    public static Action OnHourUpdate;
    public static Action OnDayUpdate;
    public static Action OnMonthUpdate;
    public static Action OnYearUpdate;

    public static TimeManager instance;

    public enum PartOfDay
    {
        MORNING,
        AFTERNOON,
        EVENING,
        NIGHT
    }

    public PartOfDay partOfDay;

    [SerializeField]
    float intervalTime = 1.0f; // 1.0f -> 1 real second is 1 ingame minute

    int minutesPerInterval = 1;

    public CultureInfo cultureInfo = new CultureInfo("en-us");
    DateTime dateTime = new DateTime(1, 1, 1, 0, 0, 0);
    DateTime newDateTime;
    float timer;

    public DateTime GetDateTime() => dateTime;

    public string GetTime() => dateTime.ToString("hh:mm tt", cultureInfo);
    public string GetDate() => dateTime.ToString("dd/mm/yyyy", cultureInfo);
    public float GetintervalTime() => intervalTime;

    [Header("ReadOnly")]
    [SerializeField]
    string readOnlyTimeString;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        timer = intervalTime;
        newDateTime = dateTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            newDateTime = dateTime.AddMinutes(minutesPerInterval);

            if(newDateTime.Second != dateTime.Second)
                OnSecondUpdate?.Invoke();
            if (newDateTime.Minute != dateTime.Minute)
                OnMinuteUpdate?.Invoke();
            if (newDateTime.Hour != dateTime.Hour)
                OnHourUpdate?.Invoke();
            if(newDateTime.Day != dateTime.Day)
                OnDayUpdate?.Invoke();
            if(newDateTime.Month != dateTime.Month)
                OnMonthUpdate?.Invoke();
            if(dateTime.Year != dateTime.Year)
                OnYearUpdate?.Invoke();

            dateTime = newDateTime;
            CheckPartsOfDay();
            timer = intervalTime;
            readOnlyTimeString = GetTime() + " " + GetDate();
            OnTimeUpdate?.Invoke();
        }
    }

    void CheckPartsOfDay()
    {
        if (dateTime.Hour >= 22)
            partOfDay = PartOfDay.NIGHT;
        else if (dateTime.Hour < 6)
            partOfDay = PartOfDay.NIGHT;
        else if (dateTime.Hour >= 6 && dateTime.Hour < 12)
            partOfDay = PartOfDay.MORNING;
        else if (dateTime.Hour >= 12 && dateTime.Hour < 17)
            partOfDay = PartOfDay.AFTERNOON;
        else if (dateTime.Hour >= 17 && dateTime.Hour < 22)
            partOfDay = PartOfDay.EVENING;
    }
}