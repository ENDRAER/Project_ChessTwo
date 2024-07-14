using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : InteractScript
{
    [NonReorderable] protected FigureScript m_figureScript;

    private void Start()
    {
        m_figureScript = transform.GetComponentInParent<FigureScript>();
    }

    public virtual void StartAction()
    {
        Debug.LogError("There's no Acting script");
    }

    public virtual void DisableAction()
    {
        Debug.LogError("There's no Disable script");
    }
}