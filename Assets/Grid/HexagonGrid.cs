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
    [SerializeField] public Transform trans;
    [SerializeField] public float distance;

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

    private void Update()
    {
        float a = 0;
        float b = 0;
        foreach (Transform tr in trans)
        {
            tr.position = new Vector3(0, a, 0);
            a -= distance;
            b += 1f / trans.childCount;
            Material mat = tr.GetComponent<MeshRenderer>().material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, b);
        }
    }
}
