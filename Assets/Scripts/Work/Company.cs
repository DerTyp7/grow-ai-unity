using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    [Header("Company")]
    [SerializeField] string companyName = "Company";
    [SerializeField] int level = 1;

    [SerializeField] City city;

    [SerializeField] List<Workplace> workplaces= new List<Workplace>();

    public City GetCity() => city;

    void Awake()
    {
        city.AddCompany(this);
    }

    public void AddWorkplace(Workplace wp)
    {
        if (!workplaces.Contains(wp))
        {
            workplaces.Add(wp);
        }
    }
}
