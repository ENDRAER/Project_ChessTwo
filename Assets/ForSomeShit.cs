using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSomeShit : MonoBehaviour
{
    float prevpos = 5;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            transform.DOMove(new Vector3(5, 5, 0), transform.position.z/20);

        print(prevpos - transform.position.z);
        prevpos = transform.position.z;
    }
}
