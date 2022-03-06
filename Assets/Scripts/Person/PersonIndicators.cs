using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonIndicators : MonoBehaviour
{
    [Header("Person Indicators")]
    [SerializeField] float happiness = 1f;
    [SerializeField] float supplied = 1f;

    [Header("Decay Modifiers")]
    [SerializeField] float suppliedDecayModifier = 0.4f;
    [SerializeField] float happinessDecayModifier = 0.1f;

    public float GetHappiness() => happiness;
    public float GetSupplied() => supplied;

    void Start()
    {
        TimeManager.OnDayUpdate += OnDayUpdate;
    }

    void OnDayUpdate()
    {
        DecreaseSupplied(Random.Range(0.0f, suppliedDecayModifier)); // Random Percentage probability

        if (supplied <= 0.2f)
        {
            DecreaseHappiness(happinessDecayModifier);
        }
    }

    public void IncreaseHappiness(float value)
    {
        if(happiness + value <= 1f)
        {
            happiness += value;
        }
        else
        {
            happiness = 1f;
        }
    }

    public void DecreaseHappiness(float value)
    {
        if(happiness - value >= 0f)
        {
            happiness -= value;
        }
    }
    public void IncreaseSupplied(float value)
    {
        if (supplied + value <= 1f)
        {
            supplied += value;
        }
        else
        {
            supplied = 1f;
        }
    }

    public void DecreaseSupplied(float value)
    {
        if (supplied - value >= 0f)
        {
            supplied -= value;
        }
    }
}
