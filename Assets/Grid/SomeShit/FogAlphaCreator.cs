using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FogAlphaCreator : MonoBehaviour
{
    [SerializeField] private float interval;

    void Start()
    {
        float a = 1;
        float i = 0;
        foreach (Transform tr in transform)
        {
            tr.position = new Vector3(0, i, 0);
            i -= interval;

            a -= 1f / (transform.childCount - 1);
            Material mat = tr.GetComponent<MeshRenderer>().material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, a == 0? 1 : (0.05f) / (a + 0.0495f) - 0.0495f);
        }
    }
}
