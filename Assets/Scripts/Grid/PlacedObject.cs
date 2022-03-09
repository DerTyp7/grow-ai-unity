using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO)
    {
        Transform placeObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.identity);

        PlacedObject placedObject = placeObjectTransform.GetComponent<PlacedObject>();
        placedObject.placedObjectTypeSO = placedObjectTypeSO;
        placedObject.origin = origin;
        placedObject.dir = dir;

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
    PlacedObjectTypeSO.Dir dir;

    public List<Vector2Int> GetGridPositionList()
    {
        return placedObjectTypeSO.GetGridPositionList(origin, PlacedObjectTypeSO.Dir.Down);
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