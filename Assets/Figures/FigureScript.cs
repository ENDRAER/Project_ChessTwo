using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureScript : InteracrScript
{
    [SerializeField] public GameObject FigureModel;
    [SerializeField] public GameObject actionMenuGO;
    [SerializeField] public GameObject pointerGO;
    [NonSerialized] public Action selectedAction;


    public override void Interacting()
    {
        if(actionMenuGO.transform.localScale.x == 0)
            actionMenuGO.transform.DOScale(Vector3.one, 0.1f);
        if (actionMenuGO.transform.localScale.x == 1)
            actionMenuGO.transform.DOScale(Vector3.zero, 0.1f);
    }
}
