using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written with https://www.youtube.com/watch?v=dulosHPl82A
public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] List<PlacedObjectTypeSO> placedObjectTypeSOList = new List<PlacedObjectTypeSO>(); // https://youtu.be/dulosHPl82A?t=1192
    PlacedObjectTypeSO placedObjectTypeSO;

    Grid<GridObject> grid;
    void Awake()
    {
        int gridWidth = 20;
        int gridHeight = 20;
        float cellSize = 2f;

        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
        placedObjectTypeSO = placedObjectTypeSOList[0];
    }
    public class GridObject
    {
        int x, y;
        Grid<GridObject> grid;
        PlacedObject placedObject;
        public GridObject(Grid<GridObject> _grid, int _x, int _y)
        {
            grid = _grid;
            x = _x;
            y = _y;
        }
        public void SetPlacedObject(PlacedObject _placedObject) => placedObject = _placedObject;
        public PlacedObject GetPlacedObject() => placedObject;
        public void ClearPlacedObject() => placedObject = null;

        public bool CanBuild()
        {
            return placedObject == null;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            grid.GetXY(mousePosition, out int x, out int y);

            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, y), PlacedObjectTypeSO.Dir.Down);

            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if(!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    // Cannot build here
                    canBuild = false;
                    break;
                }
            }

            if (canBuild)
            {
                PlacedObject placedObject = PlacedObject.Create(grid.GetWorldPosition(x, y), new Vector2Int(x, y), PlacedObjectTypeSO.Dir.Down, placedObjectTypeSO);
                
                
                foreach(Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                }
                
            }
            else
            {
                Debug.Log("Cannot build here!" + " " + mousePosition);
            }
        }else if (Input.GetMouseButtonDown(1))
        {
            GridObject gridObject = grid.GetGridObject(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            PlacedObject placedObject = gridObject.GetPlacedObject();
            if (placedObject != null)
            {
                placedObject.DestroySelf();

                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                }
            }

        }
    }
}
