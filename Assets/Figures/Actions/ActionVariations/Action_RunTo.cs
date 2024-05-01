using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Action_RunTo : Action
{
    [NonSerialized] private float maxTravelDistance = 1.7f;
    [NonSerialized] private MeshRenderer[] pointerMeshRenderers = new MeshRenderer[3]; // 0 - buttom thing 1 - conus 2 - shadow

    public override void Interacting()
    {
        _matchController.selectedAction = this;
        _matchController.CurentState = MatchController.States.cellChoisechising;
        _matchController.selectedFigure = m_figureScript;
        m_figureScript.pointerGO.SetActive(true);

        pointerMeshRenderers[0] = m_figureScript.pointerGO.GetComponent<MeshRenderer>();
        pointerMeshRenderers[1] = m_figureScript.pointerGO.transform.GetChild(0).GetComponent<MeshRenderer>();
        pointerMeshRenderers[2] = m_figureScript.pointerGO.transform.GetChild(1).GetComponent<MeshRenderer>();

        pointerMeshRenderers[0].material.DOFade(1, 1);
        pointerMeshRenderers[1].material.DOFade(1, 1);
        pointerMeshRenderers[2].material.DOFade(0.3f, 1);
        pointerMeshRenderers[2].material.color = new Color(0.1f, 0.5f, 0.1f, pointerMeshRenderers[2].material.color.a);
        highlightCells();
    }

    public override void highlightCells()
    {
        foreach (CellParameters CellCP in _hexagonGrid.cells)
        {
            if (CellCP != null)
            {
                float distanceFromCell = Vector3.Distance(CellCP.transform.position, transform.parent.parent.parent.position);
                if (distanceFromCell < maxTravelDistance && distanceFromCell >= 0.8f)
                {
                    Outline cellOutline = CellCP.GetComponent<Outline>();
                    cellOutline.enabled = true;
                    cellOutline.OutlineColor = Color.green;
                }
            }
        }
    }

    // CustomBehaviour
    public override void CustomActionCursourOnBehaviour(Transform target, Transform previousTarget)
    {
        if (target.tag == "Hexagon")
        {
            float distanceFromCell = Vector3.Distance(target.transform.position, transform.parent.parent.parent.position);
            if (distanceFromCell < maxTravelDistance && distanceFromCell >= 0.8f)
                pointerMeshRenderers[2].material.color = new Color(0.1f, 0.5f, 0.1f, pointerMeshRenderers[2].material.color.a);
            else
                pointerMeshRenderers[2].material.color = new Color(0.5f, 0.1f, 0.1f, pointerMeshRenderers[2].material.color.a);
            m_figureScript.pointerGO.transform.DOKill();
            m_figureScript.pointerGO.transform.DOMove(new Vector3(target.position.x, target.position.y + 0.5f, target.position.z), 0.1f);
        }
    }

    public override void CustomActionInteractionBehaviour(Transform target)
    {
        m_figureScript.FigureModel.transform.DOLookAt(target.position, 0.3f, AxisConstraint.Y);
        _matchController.selectedFigure = null;
    }

    public override void ActingOnTact()
    {

    }
}
