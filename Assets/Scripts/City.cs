using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    [Header("City")]
    [SerializeField]
    string cityName;


    [SerializeField] List<Person> persons = new List<Person>();
    [SerializeField] List<House> houses = new List<House>();
    [SerializeField] List<Company> companies = new List<Company>();
    [SerializeField] List<Workplace> workplaces = new List<Workplace>();
    [SerializeField] List<Store> stores = new List<Store>();
    [SerializeField] List<FreeTimeObject> freeTimeObjects = new List<FreeTimeObject>();

    public List<Store> GetStores() => stores;
    public List<FreeTimeObject> GetFreeTimeObjects() => freeTimeObjects;
    public List<House> GetHouses() => houses;
    public List<Workplace> GetWorkplaces() => workplaces;

    public void AddPerson(Person citizen)
    {
        if (!persons.Contains(citizen))
        {
            persons.Add(citizen);
            Debug.Log(citizen.GetFullName() + " joined " + cityName);
        }
    }

    public void RemovePerson(Person citizen)
    {
        if (persons.Contains(citizen))
        {
            persons.Remove(citizen);
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

    public House GetAvaiableHouse()
    {
        List<House> avaiableHouses = new List<House>();
        foreach (House house in houses)
        {
            if(house.GetAvaiableSpace() > 0)
                avaiableHouses.Add(house);
        }

        if(avaiableHouses.Count > 0)
        {
            return avaiableHouses[Random.Range(0, avaiableHouses.Count - 1)];
        }
        else
        {
            return null;
        }
        
    }

    public int GetAvaiableHousingSpace()
    {
        int space = 0;
        foreach(House house in houses)
        {
            space += house.GetAvaiableSpace();
        }
        return space;
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

    public void AddWorkplace(Workplace workplace)
    {
        if (!workplaces.Contains(workplace))
        {
            workplaces.Add(workplace);
            Debug.Log("workplaces built in " + cityName);
        }
    }
    public void RemoveWorkplace(Workplace workplace)
    {
        if (workplaces.Contains(workplace))
        {
            workplaces.Remove(workplace);
            Debug.Log("workplaces demolished in " + cityName);
        }
    }

    public Workplace GetAvaiableWorkplace()
    {
        List<Workplace> avaiableWorkplaces = new List<Workplace>();
        foreach (Workplace workplace in workplaces)
        {
            if (workplace.GetAvaiableSpace() > 0)
                avaiableWorkplaces.Add(workplace);
        }

        if (avaiableWorkplaces.Count > 0)
        {
            return avaiableWorkplaces[Random.Range(0, avaiableWorkplaces.Count - 1)];
        }
        else
        {
            return null;
        }
    }
}
