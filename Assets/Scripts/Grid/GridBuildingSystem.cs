using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuildingSystem : MonoBehaviour
{
    [SerializeField] PlacedObjectTypeSO placedObjectTypeSO;

    Grid<GridObject> grid;
    void Awake()
    {
        int gridWidth = 20;
        int gridHeight = 20;
        float cellSize = 2f;

        grid = new Grid<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
    }
    public class GridObject
    {
        int x, y;
        Grid<GridObject> grid;
        Transform transform;
        public GridObject(Grid<GridObject> _grid, int _x, int _y)
        {
            grid = _grid;
            x = _x;
            y = _y;
        }
        public void SetTransform(Transform _transform) => transform = _transform;
        public Transform GetTransform() => transform;
        public void ClearTransform() => transform = null;

        public bool CanBuild()
        {
            return transform == null;
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
                Transform builtTransform = Instantiate(placedObjectTypeSO.prefab, grid.GetWorldPosition(x, y), Quaternion.identity);
                
                foreach(Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(builtTransform);
                }
                
            }
            else
            {
                Debug.Log("Cannot build here!" + " " + mousePosition);
            }
        }
    }
}