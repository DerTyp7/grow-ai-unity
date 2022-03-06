using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FreeTimeObject : MonoBehaviour
{
    [Header("FreeTime Object")]
    [SerializeField] string FreeTimeObjectName;
    [SerializeField] City city;

    private void Awake()
    {
        city.AddFreeTimeObject(this);
    }
}
