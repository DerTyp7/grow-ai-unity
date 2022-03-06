using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PersonStatus
{
    WORK,
    SLEEP,
    FREETIME,
    PARK,
    STORE,
}

public class Person : MonoBehaviour
{
    [Header("Person")]

    [SerializeField] string firstName = "";
    [SerializeField] string lastName = "";
    [SerializeField] City city;
    [SerializeField] House house;
    [SerializeField] Workplace workplace;

    public PersonStatus status;

    PersonMovement movement;
    PersonIndicators indicators;

    public string GetFirstName() => firstName;
    public string GetLastName() => lastName;
    public string GetFullName() => firstName + " " + lastName;

    System.DateTime goToWorkDateTime;

   
    void Awake()
    {
        city.AddCitizen(this);
        house.AddPerson(this);
        workplace.AddWorker(this);
    }

    void Start()
    {
        movement = GetComponent<PersonMovement>();
        indicators = GetComponent<PersonIndicators>();

        TimeManager.OnMinuteUpdate += OnMinuteUpdate;
        TimeManager.OnDayUpdate += OnDayUpdate;

        SetBehaivorDateTimes();
        FreeTime();
    }
    void OnDayUpdate()
    {
        SetBehaivorDateTimes();
    }
    void OnMinuteUpdate()
    {

        // Work -> FreeTime -> Sleep

        if (TimeManager.instance.GetDateTime() > goToWorkDateTime.AddHours(12)) // Sleep
        {
            Sleep();
        }else if (TimeManager.instance.GetDateTime() > goToWorkDateTime.AddHours(8)) // FreeTime
        {
            FreeTime();
        }
        else if (TimeManager.instance.GetDateTime() > goToWorkDateTime) // Work
        {
            Work();
        } 
    }
    void FreeTime()
    {
        if(status == PersonStatus.STORE)
        {
            if(indicators.GetSupplied() == 1.0f)
            {
                status = PersonStatus.FREETIME;
            }
        }
        else
        {
            status = PersonStatus.FREETIME;
        }
        
        if(indicators.GetSupplied() < 0.3f || status == PersonStatus.STORE)
        {
            status = PersonStatus.STORE;
            Debug.Log(city.GetStores().Count - 1);
            movement.SetTarget(city.GetStores()[Random.Range(0, city.GetStores().Count-1)].transform);
        }
        else if(status == PersonStatus.FREETIME)
        {
            status = PersonStatus.PARK;
            movement.SetTarget(city.GetFreeTimeObjects()[Random.Range(0, city.GetFreeTimeObjects().Count)].transform);
        }
    }

    void Work()
    {
        status = PersonStatus.WORK;
        movement.SetTarget(workplace.transform);
    }

    void Sleep()
    {
        status = PersonStatus.SLEEP;
        movement.SetTarget(house.transform);
    }

    void SetBehaivorDateTimes()
    {
        System.DateTime currentDateTime = TimeManager.instance.GetDateTime();
        goToWorkDateTime = new System.DateTime(currentDateTime.Year,
            currentDateTime.Month,
            currentDateTime.Day,
            Random.Range(4, 9),
            Random.Range(0, 59),
            currentDateTime.Second);

    }
}
