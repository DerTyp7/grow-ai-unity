using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] int areaHeight;
    [SerializeField] int areaWidth;
    List<AreaObject> areaObjects;
    [SerializeField] GameObject testPrefab;
    void Start()
    {
        //int gridWidth = GridBuildingSystem.instance.buildingGrid.GetWidth();
        //int gridHeight = GridBuildingSystem.instance.buildingGrid.GetWidth();

        int areaWidthCount = GridBuildingSystem.instance.buildingGrid.GetWidth() / areaWidth;
        int areaHeightCount = GridBuildingSystem.instance.buildingGrid.GetHeight() / areaHeight;

        for(int heightCounter = 0; heightCounter < areaHeightCount; heightCounter++)
        {
            Debug.Log("---- New Row -----");
            for (int widthCounter = 0; widthCounter < areaWidthCount; widthCounter++)
            {
                Debug.Log("---- New Area -----");

                List<GameObject> testGameObjs = new List<GameObject>();
                for (int x = 0; x < areaWidth; x++)
                {
                    for (int y = 0; y < areaHeight; y++)
                    {
                        
                        Debug.Log((x + widthCounter * areaWidth).ToString() + "," + (y + heightCounter * areaHeight).ToString());
                        GameObject testObj = Instantiate(testPrefab);
                        testObj.transform.position = GridBuildingSystem.instance.buildingGrid.GetWorldPosition((x + widthCounter * areaWidth), (y + heightCounter * areaHeight));
                        testObj.transform.name = widthCounter + " - " + heightCounter;
                        testGameObjs.Add(testObj);
                    }
                }

                Color color = Random.ColorHSV();
                foreach (GameObject testObj in testGameObjs)
                {
                    testObj.GetComponent<SpriteRenderer>().color = color;
                }

            }
        }
        
       
    }




    public class AreaObject
    {
        public int[,] tileArray;

        public AreaObject(int[,] _tileArray) {
            tileArray = _tileArray;
        }

    }
}
