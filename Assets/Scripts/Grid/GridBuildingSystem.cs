using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written with https://www.youtube.com/watch?v=dulosHPl82A
public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem instance;
    public Grid<GridObject> buildingGrid;

    PlacedObjectTypeSO selectedPlacedObjectTypeSO;
    Transform selectedGameObjectTransform;

    void Start()
    {
        if (instance == null)
            instance = this;

        int gridWidth = GridInfo.instance.gridWidth;
        int gridHeight = GridInfo.instance.gridHeight;
        float cellSize = GridInfo.instance.cellSize;

        buildingGrid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    void Update()
    {
        if(selectedGameObjectTransform != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            buildingGrid.GetXY(mousePosition, out int x, out int y);

            selectedGameObjectTransform.position = buildingGrid.GetWorldPosition(x, y);

            List<Vector2Int> gridPositionList = selectedPlacedObjectTypeSO.GetGridPositionList(new Vector2Int(x, y), PlacedObjectTypeSO.Dir.Down);
            if (!CanBuild(gridPositionList))
            {
                selectedGameObjectTransform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                selectedGameObjectTransform.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding();
            }else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                DeselectBuilding();
            }
        }        
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

    public void DemolishBuilding(Vector3 position)
    {
        GridObject gridObject = buildingGrid.GetGridObject(position); // Camera.main.ScreenToWorldPoint(Input.mousePosition)
        PlacedObject placedObject = gridObject.GetPlacedObject();

        if (placedObject != null)
        {
            placedObject.DestroySelf();

            List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                buildingGrid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
            }
        }
    }

    bool CanBuild(List<Vector2Int> gridPositionList)
    {
        bool canBuild = true;
        foreach (Vector2Int gridPosition in gridPositionList)
        {
            if (!buildingGrid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
            {
                // Cannot build here
                canBuild = false;
                break;
            }
        }
        return canBuild;
    }

    public void PlaceBuilding()
    {
        Vector3 position = selectedGameObjectTransform.position;

        Vector3 mousePosition = new Vector3(position.x, position.y);
        buildingGrid.GetXY(mousePosition, out int x, out int y);

        List<Vector2Int> gridPositionList = selectedPlacedObjectTypeSO.GetGridPositionList(new Vector2Int(x, y), PlacedObjectTypeSO.Dir.Down);

       

        if (CanBuild(gridPositionList))
        {
            PlacedObject placedObject = PlacedObject.Create(buildingGrid.GetWorldPosition(x, y), new Vector2Int(x, y), PlacedObjectTypeSO.Dir.Down, selectedPlacedObjectTypeSO);

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                buildingGrid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            }

        }
        else
        {
            Debug.Log("Cannot build here!" + " " + mousePosition);
        }
    }

    public void SelectBuilding(PlacedObjectTypeSO placedObjectTypeSO)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectedGameObjectTransform = Instantiate(placedObjectTypeSO.prefab, mousePosition, Quaternion.identity);
        selectedPlacedObjectTypeSO = placedObjectTypeSO;
    }

    public void DeselectBuilding()
    {
        Destroy(selectedGameObjectTransform.gameObject);
        selectedPlacedObjectTypeSO = null;
        selectedGameObjectTransform = null;
    }

}
