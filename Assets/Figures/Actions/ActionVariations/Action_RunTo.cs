using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_RunTo : Action
{
    public override void TakeAction()
    {
        _matchController.selectedFigure = transform.GetComponentInParent<FigureScript>();
        _matchController.CurentState = MatchController.States.cellChoisechising;
        highlightCells();
    }

    public override void highlightCells()
    {
        foreach (CellParameters CellCP in _hexagonGrid.cells)
        {
            if (CellCP != null)
            {
                float distanceFromCell = Vector3.Distance(CellCP.transform.position, transform.parent.parent.parent.position);
                if (distanceFromCell < 1.7f && distanceFromCell >= 0.8f)
                {
                    Outline cellOutline = CellCP.GetComponent<Outline>();
                    cellOutline.enabled = true;
                    cellOutline.OutlineColor = Color.green;
                }
            }
        }
    }
}
