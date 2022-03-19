using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written with https://www.youtube.com/watch?v=dulosHPl82A
public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem instance;
    public Grid<GridObject> buildingGrid;

    public int gridWidth;
    public int gridHeight;
    public float cellSize;

    PlacedObjectTypeSO selectedPlacedObjectTypeSO;
    Transform selectedGameObjectTransform;

    Vector3 selectedWayStart;

    public List<GameObject> blueprintWayList = new List<GameObject>();
    bool isPlacingWay = false;

    public class GridObject
    {
        int x, y;
        bool isAccessable;
        Grid<GridObject> grid;
        PlacedObject placedObject;
        public GridObject(Grid<GridObject> _grid, int _x, int _y, bool _isAccessable = true) // FOR DEBUG TRUE
        {
            grid = _grid;
            x = _x;
            y = _y;
            isAccessable = _isAccessable;
        }
        public void SetPlacedObject(PlacedObject _placedObject) => placedObject = _placedObject;
        public PlacedObject GetPlacedObject() => placedObject;
        public void ClearPlacedObject() => placedObject = null;
        public void SetIsAccessable(bool _isAccessable) => isAccessable = _isAccessable;
        public void SwitchIsAccessable() => isAccessable = !isAccessable;
        public bool IsAccessable() => isAccessable;
        public bool CanBuild()
        {
            return placedObject == null && isAccessable;
        }
    }


    void Awake()
    {
        if (instance == null)
            instance = this;

        buildingGrid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }

    void Update()
    {
        if (selectedGameObjectTransform != null)
        {
            if (selectedPlacedObjectTypeSO.placedObjectType == PlacedObjectTypeSO.PlacedObjectType.WAY)
            {
                UpdateWayDrawing();
            }
            else
            {
                UpdateSelectedGameObject();
            }
        }
    }

    void UpdateWayDrawing()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        List<GameObject> dontDeleteBlueprintList = new List<GameObject>();

        if (isPlacingWay)
        {
            foreach (Vector3 position in Pathfinding.Instance.FindPath(selectedWayStart, mousePosition, true))
            {
                GameObject placedBuilding = PlaceBuilding(position);
                if (placedBuilding != null)
                {
                    placedBuilding.GetComponent<PlacedObject>().isBlueprint = true;
                    dontDeleteBlueprintList.Add(placedBuilding);
                    blueprintWayList.Add(placedBuilding);
                }
                else
                {
                    buildingGrid.GetXY(position, out int x, out int y);
                    PlacedObject tempPlacedObject = buildingGrid.GetGridObject(x, y).GetPlacedObject();
                    if (tempPlacedObject.isBlueprint)
                    {
                        dontDeleteBlueprintList.Add(tempPlacedObject.gameObject);
                    }
                }
            }

        }
        
        foreach(GameObject blueprint in blueprintWayList)
        {
            if (!dontDeleteBlueprintList.Contains(blueprint))
            {
                Destroy(blueprint);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isPlacingWay)
            {
                // PLACE EVERYTHING
                foreach (GameObject blueprint in blueprintWayList)
                {
                    blueprint.GetComponent<PlacedObject>().isBlueprint = false;
                    blueprintWayList.Remove(blueprint);
                }
                isPlacingWay = false;
            }
            else
            {
                isPlacingWay = true;
                selectedWayStart = mousePosition;
            }
        }
    }

    void UpdateSelectedGameObject()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        buildingGrid.GetXY(mousePosition, out int x, out int y);

        selectedGameObjectTransform.position = buildingGrid.GetWorldPosition(x, y);

        List<Vector2Int> gridPositionList = selectedPlacedObjectTypeSO.GetGridPositionList(new Vector2Int(x, y));
        if (!CanBuild(gridPositionList))
        {
            selectedGameObjectTransform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (selectedPlacedObjectTypeSO.placedObjectType != PlacedObjectTypeSO.PlacedObjectType.WAY)
        {
            selectedGameObjectTransform.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (Input.GetMouseButtonDown(0))
        {
                
            PlaceBuilding(selectedGameObjectTransform.position);
        }
            

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {
            DeselectBuilding();
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

    public GameObject PlaceBuilding(Vector3 position)
    {
        position = new Vector3(position.x, position.y);
        buildingGrid.GetXY(position, out int x, out int y);

        List<Vector2Int> gridPositionList = selectedPlacedObjectTypeSO.GetGridPositionList(new Vector2Int(x, y));

       

        if (CanBuild(gridPositionList))
        {
            PlacedObject placedObject = PlacedObject.Create(buildingGrid.GetWorldPosition(x, y), new Vector2Int(x, y), selectedPlacedObjectTypeSO);

            foreach (Vector2Int gridPosition in gridPositionList)
            {
                buildingGrid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            }
            return placedObject.gameObject;
        }
        else
        {
            //Debug.Log("Cannot build here!" + " " + position);
        }
        return null;
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
        isPlacingWay = false;
    }

}
