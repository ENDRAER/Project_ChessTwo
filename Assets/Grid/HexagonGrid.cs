using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HexagonGrid : MonoBehaviour
{
    [NonSerialized] public static HexagonGrid StaticHexagonGrid;
    [NonSerialized] public int maxGridSize = 200;
    [NonSerialized] public CellParameters[,,] cells;

    private void Awake()
    {
        StaticHexagonGrid = this;
        cells = new CellParameters[maxGridSize, maxGridSize, maxGridSize];
        foreach (Transform cell in transform)
        {
            float x = cell.position.x / 0.5f + maxGridSize / 2;
            float y = cell.position.y / 0.4f + maxGridSize / 2;
            float z = cell.position.z / 0.8625f + maxGridSize / 2;
            cells[(int)x, (int)y, (int)z] = cell.GetComponent<CellParameters>();
        }
    }
}
