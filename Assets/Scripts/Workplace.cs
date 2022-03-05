using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workplace : MonoBehaviour
{
    [Header("Workplace")]
    [SerializeField]
    int space = 1;

    [SerializeField]
    float salary = 4.5f;

    [SerializeField]
    List<Person> workers = new List<Person>();

    [SerializeField]
    List<Person> activeWorkers = new List<Person>(); // Workers which are currently present and working

    [SerializeField]
    City city;

    public void AddActiveWorker(Person worker) => activeWorkers.Add(worker);
    public void RemoveActiveWorker(Person worker) => activeWorkers.Remove(worker);

    void Awake()
    {
        city.AddWorkplace(this);

    }

    void Start()
    {
        TimeManager.OnHourUpdate += OnHourUpdate;
    }

    void OnHourUpdate()
    {
        EconomyManager.instance.AddMoney(salary * activeWorkers.Count);
    }

    public void AddWorker(Person worker)
    {
        if (!workers.Contains(worker) && workers.Count < space)
        {
            workers.Add(worker);
            Debug.Log(worker.GetFullName() + " now works");
        }
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
