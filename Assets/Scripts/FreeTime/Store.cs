using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] string storeName;
    [SerializeField] City city;
    [SerializeField] List<PersonIndicators> personIndicatorsInStore = new List<PersonIndicators>();
    

    void Start()
    {
        city.AddStore(this);
        TimeManager.OnMinuteUpdate += OnMinuteUpdate;
    }

    void OnMinuteUpdate()
    {
        foreach (PersonIndicators personIndicator in personIndicatorsInStore)
        {
            personIndicator.IncreaseSupplied(0.05f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if(collision.GetComponent<Person>() != null)
            {
                if(collision.GetComponent<Person>().status == PersonStatus.STORE)
                {
                    personIndicatorsInStore.Add(collision.GetComponent<PersonIndicators>());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.GetComponent<Person>() != null)
            {
                if (personIndicatorsInStore.Contains(collision.GetComponent<PersonIndicators>()))
                {
                    personIndicatorsInStore.Remove(collision.GetComponent<PersonIndicators>());
                }
            }
        }
    }

}
