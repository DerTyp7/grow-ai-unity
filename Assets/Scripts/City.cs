using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [Header("City")]
    [SerializeField]
    string cityName;


    [SerializeField] List<Person> citizens = new List<Person>();
    [SerializeField] List<House> houses = new List<House>();
    [SerializeField] List<Company> companies = new List<Company>();
    [SerializeField] List<Store> stores = new List<Store>();
    [SerializeField] List<FreeTimeObject> freeTimeObjects = new List<FreeTimeObject>();

    public List<Store> GetStores() => stores;
    public List<FreeTimeObject> GetFreeTimeObjects() => freeTimeObjects;

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

    public void AddCompany(Company company)
    {
        if (!companies.Contains(company))
        {
            companies.Add(company);
            Debug.Log("Company built in " + cityName);
        }
    }

    public void RemoveCompany(Company company)
    {
        if (companies.Contains(company))
        {
            companies.Remove(company);
            Debug.Log("Company demolished in " + cityName);
        }
    }

    public void AddStore(Store store)
    {
        if (!stores.Contains(store))
        {
            stores.Add(store);
            Debug.Log("Store built in " + cityName);
        }
    }

    public void RemoveStore(Store store)
    {
        if (stores.Contains(store))
        {
            stores.Remove(store);
            Debug.Log("Store demolished in " + cityName);
        }
    }
    public void AddFreeTimeObject(FreeTimeObject freeTimeObject)
    {
        if (!freeTimeObjects.Contains(freeTimeObject))
        {
            freeTimeObjects.Add(freeTimeObject);
            Debug.Log("FreeTimeObject built in " + cityName);
        }
    }

    public void RemoveFreeTimeObject(FreeTimeObject freeTimeObject)
    {
        if (freeTimeObjects.Contains(freeTimeObject))
        {
            freeTimeObjects.Remove(freeTimeObject);
            Debug.Log("FreeTimeObject demolished in " + cityName);
        }
    }
}
