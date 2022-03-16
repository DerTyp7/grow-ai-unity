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


    // DEBUG
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            movement.SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    void Start()
    {
        Register();
        movement = gameObject.GetComponent<PersonMovement>();
        indicators = gameObject.GetComponent<PersonIndicators>();
        transform.position = house.transform.position;

        TimeManager.OnDayUpdate += DestroyInactive;
        TimeManager.OnDayUpdate += CheckWorkplace;
        TimeManager.OnDayUpdate += OnDayUpdate;
        TimeManager.OnMinuteUpdate += OnMinuteUpdate;

        SetBehaivorDateTimes();
    }

    void Register()
    {
        // Register To City
        city = GameObject.Find("GameManager").GetComponent<City>();
        city.AddPerson(this);
        // Register To House
        house = city.GetAvaiableHouse();
        if(house != null)
            house.AddPerson(this);
        // Register To Workplace
        workplace = city.GetAvaiableWorkplace();
        if(workplace != null)
            workplace.AddWorker(this);
    }

    void CheckWorkplace()
    {
        if (workplace == null)
        {
            workplace = city.GetAvaiableWorkplace();
            workplace.AddWorker(this);
        }
    }
    void DestroyInactive()
    {
        // Destroy a person if they do not find a house anymore
        if (house == null)
        {
            if(city != null)
                city.RemovePerson(this);
            if (workplace != null)
                workplace.RemoveWorker(this);

            Destroy(gameObject);
        }
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
        }
        else if (TimeManager.instance.GetDateTime() > goToWorkDateTime.AddHours(8)) // FreeTime
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
        if (status == PersonStatus.STORE)
        {
            if (indicators.GetSupplied() == 1.0f)
            {
                status = PersonStatus.FREETIME;
            }
        }
        else if (status != PersonStatus.PARK)
        {
            status = PersonStatus.FREETIME;
        }

        if (indicators.GetSupplied() < 0.3f || status == PersonStatus.STORE)
        {
            status = PersonStatus.STORE;
            Debug.Log(city.GetStores().Count - 1);
            //movement.SetTarget(city.GetStores()[Random.Range(0, city.GetStores().Count - 1)].transform.position);
        }
        else if (status != PersonStatus.PARK)
        {
            status = PersonStatus.PARK;// Check if any Object exsits
            //movement.SetTarget(city.GetFreeTimeObjects()[Random.Range(0, city.GetFreeTimeObjects().Count)].transform.position);
        }
    }

    void Work()
    {
        status = PersonStatus.WORK;
        movement.SetTarget(workplace.transform.position);
    }

    void Sleep()
    {
        status = PersonStatus.SLEEP;
        Debug.Log(house.transform.position);
        movement.SetTarget(house.transform.position);
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





    /*
    void Start ()
    {
        
        city.AddCitizen(this);
        house.AddPerson(this);
        workplace.AddWorker(this);
        /* foreach (Workplace w in city.GetWorkplaces())
         {
             if(workplace == null)
             {
                 if (w.AddWorker(this))
                 {
                     workplace = w;
                     Debug.Log("Workplace added to " + GetFullName());
                 }

             }
         }


         foreach (House h in city.GetHouses())
         {
             if(house == null)
             {
                 if (h.AddPerson(this))
                 {
                     house = h;
                     Debug.Log("House added to " + GetFullName());
                 }
             }
         }


        movement = GetComponent<PersonMovement>();
        indicators = GetComponent<PersonIndicators>();

        TimeManager.OnMinuteUpdate += OnMinuteUpdate;
        TimeManager.OnDayUpdate += OnDayUpdate;

        SetBehaivorDateTimes();
        Sleep();
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
        else if (status != PersonStatus.PARK)
        {
            status = PersonStatus.FREETIME;
        }
        
        if(indicators.GetSupplied() < 0.3f || status == PersonStatus.STORE)
        {
            status = PersonStatus.STORE;
            Debug.Log(city.GetStores().Count - 1);
            movement.SetTarget(city.GetStores()[Random.Range(0, city.GetStores().Count-1)].transform.position);
        }
        else if(status != PersonStatus.PARK)
        {
            status = PersonStatus.PARK;// Check if any Object exsits
            movement.SetTarget(city.GetFreeTimeObjects()[Random.Range(0, city.GetFreeTimeObjects().Count)].transform.position);
        }
    }

    void Work()
    {
        status = PersonStatus.WORK;
        movement.SetTarget(workplace.transform.position);
    }

    void Sleep()
    {
        status = PersonStatus.SLEEP;
        Debug.Log(house.transform.position);
        movement.SetTarget(house.transform.position);
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

    }*/
}
