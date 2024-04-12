using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureScript : InteracrScript
{
    public GameObject ActionMenuGO;


    public override void Interacting()
    {
        if(ActionMenuGO.transform.localScale.x == 0)
            ActionMenuGO.transform.DOScale(Vector3.one, 0.1f);
        if (ActionMenuGO.transform.localScale.x == 1)
            ActionMenuGO.transform.DOScale(Vector3.zero, 0.1f);
    }
}
