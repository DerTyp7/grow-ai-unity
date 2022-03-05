using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    [Header("Person")]
    [SerializeField]
    string firstName = "";

    [SerializeField]
    string lastName = "";

    [SerializeField]
    City city;

    [SerializeField]
    House house;

    [SerializeField]
    Workplace workplace;

    TimeManager timeManager;
    PersonMovement movement;

    public bool isWorking = false;

    public string GetFirstName() => firstName;
    public string GetLastName() => lastName;
    public string GetFullName() => firstName + " " + lastName;


    TimeManager.PartOfDay prevPartOfDay;
    void Awake()
    {
        city.AddCitizen(this);
        house.AddPerson(this);
        workplace.AddWorker(this);
    }

    void Start()
    {
        timeManager = GameObject.Find("GameManager").GetComponent<TimeManager>();
        movement = GetComponent<PersonMovement>();

        TimeManager.OnTimeUpdate += OnTimeUpdate;
    }

    void OnTimeUpdate()
    {
        if (prevPartOfDay != timeManager.partOfDay)
        {
            switch (timeManager.partOfDay)
            {
                case TimeManager.PartOfDay.NIGHT:
                    movement.SetTarget(house.transform);
                    break;
                case TimeManager.PartOfDay.MORNING:
                    movement.SetTarget(workplace.transform);
                    workplace.AddActiveWorker(this);
                    break;
                case TimeManager.PartOfDay.AFTERNOON:
                    break;
                case TimeManager.PartOfDay.EVENING:
                    workplace.RemoveActiveWorker(this);
                    movement.SetTarget(city.transform);
                    break;
                default:
                    movement.SetTarget(city.transform);
                    break;
            }
            prevPartOfDay = timeManager.partOfDay;
        }
    }
}
