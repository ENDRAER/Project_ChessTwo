using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : InteractScript
{
    [NonSerialized] protected FigureScript m_figureScript;

    private void Start()
    {
        m_figureScript = transform.GetComponentInParent<FigureScript>();
    }

    public void StartAction()
    {
        m_figureScript.FigureModel.transform.DOLookAt(m_figureScript.pointerGO.transform.position, 0.3f, AxisConstraint.Y).OnComplete(()=>CustomAction());
    }

    public virtual void CustomAction()
    {
        Debug.LogError("There's no Acting script");
    }

    public virtual void DisableAction()
    {
        Debug.LogError("There's no Disable script");
    }
}