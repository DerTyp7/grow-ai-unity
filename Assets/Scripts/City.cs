using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [Header("City")]
    [SerializeField]
    string cityName;

    [SerializeField]
    List<Person> citizens = new List<Person>();

    [SerializeField]
    List<House> houses = new List<House>();    

    [SerializeField]
    List<Workplace> workplaces = new List<Workplace>();

    public void AddCitizen(Person citizen)
    {
        if (!citizens.Contains(citizen))
        {
            citizens.Add(citizen);
            Debug.Log(citizen.GetFullName() + " joined " + cityName);
        }
    }

    public void RemoveCitizen(Person citizen)
    {
        if (citizens.Contains(citizen))
        {
            citizens.Remove(citizen);
            Debug.Log(citizen.GetFullName() + " left " + cityName);
        }
    }

    public void AddHouse(House house)
    {
        if (!houses.Contains(house))
        {
            houses.Add(house);
            Debug.Log("House built in " + cityName);
        }
    }

    public void RemoveHouse(House house)
    {
        if (houses.Contains(house))
        {
            houses.Remove(house);
            Debug.Log("House demolished in " + cityName);
        }
    }

    public void AddWorkplace(Workplace workplace)
    {
        if (!workplaces.Contains(workplace))
        {
            workplaces.Add(workplace);
            Debug.Log("Workplace built in " + cityName);
        }
    }

    public void RemoveWorkplace(Workplace workplace)
    {
        if (workplaces.Contains(workplace))
        {
            workplaces.Remove(workplace);
            Debug.Log("Workplace demolished in " + cityName);
        }
    }
}
