using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : InteracrScript
{
    [SerializeField] protected FigureScript m_figureScript;
    [SerializeField] protected HexagonGrid _hexagonGrid;
    [SerializeField] protected MatchController _matchController;

    private void Start()
    {
        _hexagonGrid = HexagonGrid.StaticHexagonGrid;
        _matchController = MatchController.StaticMatchController;
        m_figureScript = transform.GetComponentInParent<FigureScript>();
    }

    public override void Interacting()
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
