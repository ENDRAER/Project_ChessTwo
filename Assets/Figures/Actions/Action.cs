using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : InteracrScript
{
    [NonReorderable] protected FigureScript m_figureScript;

    private void Start()
    {
        m_figureScript = transform.GetComponentInParent<FigureScript>();
    }

    public virtual void ActingOnTact()
    {

    }
}
// do not read this pls