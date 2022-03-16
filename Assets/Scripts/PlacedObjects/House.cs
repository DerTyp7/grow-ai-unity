using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : PlacedObject
{
    [Header("House")]
    [SerializeField]
    int space = 4;

    [SerializeField]
    List<Person> persons = new List<Person>();

    City city;

    public int GetAvaiableSpace() => space - persons.Count;

    public override void OnPlace()
    {
        city = GameObject.Find("GameManager").GetComponent<City>();
        city.AddHouse(this);
    }

    public bool AddPerson(Person person)
    {
        if (!persons.Contains(person) && persons.Count < space)
        {
            persons.Add(person);
            Debug.Log(person.GetFullName() + " now lives in house");
            return true;
        }
        return false;
    }

    public void RemovePerson(Person person)
    {
        if (persons.Contains(person))
        {
            persons.Remove(person);
            Debug.Log(person.GetFullName() + " does not live in house anymore");
        }
    }
}
