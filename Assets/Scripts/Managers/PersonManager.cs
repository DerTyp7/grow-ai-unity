using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonManager : MonoBehaviour
{
    City city;
    [SerializeField] GameObject personPrefab;
    void Start()
    {
        city = GetComponent<City>();
        TimeManager.OnDayUpdate += OnDayUpdate;
    }

    void OnDayUpdate()
    {
        int avaiableSpace = city.GetAvaiableHousingSpace();
        if (avaiableSpace > 5)
        {
            SpawnPerson(5);
        }
        else
        {
            SpawnPerson(avaiableSpace);
        }
        
    }

    public void SpawnPerson(int count = 1)
    {
        for(int i = 0; i < count; i++)
        {
            Instantiate(personPrefab);
        }
    }
}
