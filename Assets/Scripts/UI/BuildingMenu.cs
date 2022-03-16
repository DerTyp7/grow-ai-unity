using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMenu : MonoBehaviour
{
    
    [SerializeField] GameObject slotPrefab;
    [SerializeField] List<PlacedObjectTypeSO> placedObjectTypeSOList;

    void Start()
    {
        foreach(PlacedObjectTypeSO p in placedObjectTypeSOList)
        {
            GameObject slot = Instantiate(slotPrefab, gameObject.transform);
            slot.GetComponent<BuildingMenuSlot>().placedObjectTypeSO = p;
        }
    }


}
