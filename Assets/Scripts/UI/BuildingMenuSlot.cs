using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingMenuSlot : MonoBehaviour
{
    public PlacedObjectTypeSO placedObjectTypeSO;  

    Image img;
    Button btn;
    TextMeshProUGUI textObj;

    void Start()
    {
        img = transform.Find("Image").gameObject.GetComponent<Image>();
        btn = GetComponent<Button>();
        textObj = transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();


        img.sprite = placedObjectTypeSO.iconSprite;
        textObj.text = placedObjectTypeSO.nameString;
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        GridBuildingSystem.instance.SelectBuilding(placedObjectTypeSO);
    }
}
