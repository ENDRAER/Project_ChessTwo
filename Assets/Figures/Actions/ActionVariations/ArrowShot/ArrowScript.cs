using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private float damage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Figure")
        {
            other.GetComponent<FigureScript>().GetTheDamage(gameObject, damage);
        }
        Destroy(gameObject);
    }
}
