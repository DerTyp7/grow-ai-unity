using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO placedObjectTypeSO)
    {
        Transform placeObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.identity);

        PlacedObject placedObject = placeObjectTransform.GetComponent<PlacedObject>();
        placedObject.placedObjectTypeSO = placedObjectTypeSO;
        placedObject.origin = origin;

        placedObject.OnPlace();
        placedObject.isBlueprint = false;

        if (placedObjectTypeSO.isWalkable) 
        {
            foreach(Vector2Int position in placedObject.GetGridPositionList())
            {
                Pathfinding.Instance.GetNode(position.x, position.y).SetIsWalkable(true);
            }

        }

        return placedObject;
    }

    PlacedObjectTypeSO placedObjectTypeSO;
    Vector2Int origin;

    public bool isBlueprint = true;
    public abstract void OnPlace();

    public List<Vector2Int> GetGridPositionList()
    {
        return placedObjectTypeSO.GetGridPositionList(origin);
    }

    public void DestroySelf()
    {
        if (placedObjectTypeSO.isWalkable)
        {
            foreach (Vector2Int position in GetGridPositionList())
            {
                Pathfinding.Instance.GetNode(position.x, position.y).SetIsWalkable(false);
            }
        }
        Destroy(gameObject);
    }
}