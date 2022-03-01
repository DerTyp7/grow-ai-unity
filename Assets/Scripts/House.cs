using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("House")]
    [SerializeField]
    int space = 1;

    [SerializeField]
    List<Person> persons = new List<Person>();

    [SerializeField]
    City city;

    void Awake()
    {
        city.AddHouse(this);
    }

    public void AddPerson(Person person)
    {
        if (!persons.Contains(person) && persons.Count < space)
        {
            persons.Add(person);
            Debug.Log(person.GetFullName() + " now lives in house");
        }
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
