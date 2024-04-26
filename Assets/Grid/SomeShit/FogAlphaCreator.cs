using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class FogAlphaCreator : MonoBehaviour
{
    [SerializeField] private float interval;

    void Start()
    {
        float a = 1;
        float i = 0;
        foreach (Transform tr in transform)
        {
            i -= interval;
            tr.position = new Vector3(0, i, 0);
            a -= 1f / transform.childCount;
            Material mat = tr.GetComponent<MeshRenderer>().material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, (0.05f) / (a + 0.0495f) - 0.0495f);
        }
    }
}
