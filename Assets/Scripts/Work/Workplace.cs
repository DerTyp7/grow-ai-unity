using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workplace : PlacedObject
{
    [Header("Workplace")]
    [SerializeField] int space = 4;
    [SerializeField] float salary = 4.5f;

    [SerializeField] List<Person> workers = new List<Person>();
    [SerializeField] List<Person> activeWorkers = new List<Person>(); // Workers which are currently present and working

    City city;
    public void AddActiveWorker(Person worker) => activeWorkers.Add(worker);
    public void RemoveActiveWorker(Person worker) => activeWorkers.Remove(worker);
    public int GetAvaiableSpace() => space - workers.Count;

    public override void OnPlace()
    {
        city = GameObject.Find("GameManager").GetComponent<City>();
        city.AddWorkplace(GetComponent<Workplace>());
        TimeManager.OnHourUpdate += OnHourUpdate;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Person>() != null)
        {
            if (workers.Contains(collision.GetComponent<Person>()))
            {
                if (!activeWorkers.Contains(collision.GetComponent<Person>()))
                {
                    activeWorkers.Add(collision.GetComponent<Person>());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Person>() != null)
        {
            if (activeWorkers.Contains(collision.GetComponent<Person>()))
            {
                activeWorkers.Remove(collision.GetComponent<Person>());
            }
        }
    }

    void OnHourUpdate()
    {
        EconomyManager.instance.AddMoney(salary * activeWorkers.Count);
    }

    public bool AddWorker(Person worker) // True: Worker is added - False: no enough space for worker
    {
        if (!workers.Contains(worker) && workers.Count < space)
        {
            workers.Add(worker);
            Debug.Log(worker.GetFullName() + " now works");
            return true;
        }
        else
            return false;
    }

    public void RemoveWorker(Person worker)
    {
        if (workers.Contains(worker))
        {
            workers.Remove(worker);
            Debug.Log(worker.GetFullName() + " does not work anymore");
        }
    }
}
