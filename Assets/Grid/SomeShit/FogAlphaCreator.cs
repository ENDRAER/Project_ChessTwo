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
            tr.position = new Vector3(UnityEngine.Random.Range(-0.3f,0.3f), i, UnityEngine.Random.Range(-0.3f, 0.3f));
            i -= interval;

            a -= 1f / (transform.childCount - 1);
            Material mat = tr.GetComponent<MeshRenderer>().material;
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, a == 0? 1 : (0.23f) / (a + 0.2f) - 0.19f);
            mat.SetFloat("_Speed", 0.2f);
        }
    }
}
