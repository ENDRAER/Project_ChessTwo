using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_RunTo : Action
{
    public HexagonGrid _hexagonGrid;
    public MatchController _matchController;

    private void Awake()
    {
        _hexagonGrid = HexagonGrid.StaticHexagonGrid;
        _matchController = MatchController.StaticMatchController;
    }

    public override void TakeAction()
    {
        foreach (CellParameters penis in _hexagonGrid.cells)
        {
            if (penis != null)
            {
                penis.transform.localScale = Vector3.zero;
            }
        }
    }
}
