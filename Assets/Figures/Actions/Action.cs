using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : InteracrScript
{
    public FigureScript m_figureScript;
    public HexagonGrid _hexagonGrid;
    public MatchController _matchController;

    private void Start()
    {
        _hexagonGrid = HexagonGrid.StaticHexagonGrid;
        _matchController = MatchController.StaticMatchController;
        m_figureScript = transform.GetComponentInParent<FigureScript>();
    }

    public override void Interacting()
    {

    }

    public virtual void highlightCells()
    {

    }

    // CustomBehaviour
    public virtual void CustomActionCursourOnBehaviour(Transform target, Transform previousTarget)
    {

    }

    public virtual void CustomActionCursourOffBehaviour(Transform target, Transform previousTarget)
    {

    }

    public virtual void CustomActionInteractionBehaviour(Transform target)
    {

    }

    public virtual void ActingOnTact()
    {

    }
}
