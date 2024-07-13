using DG.Tweening;
using System;
using UnityEngine;

public class Action_RunTo : Action
{
    [NonSerialized] private float maxTravelDistance = 1.7f;
    [NonSerialized] private MeshRenderer[] pointerMeshRenderers = new MeshRenderer[3]; // 0 - buttom thing 1 - conus 2 - shadow

    public override void Interacting()
    {
        m_figureScript.actionMenuGO.transform.DOScale(Vector3.zero, 0.1f);
        m_figureScript.pointerGO.SetActive(true);

        pointerMeshRenderers[0] = m_figureScript.pointerGO.GetComponent<MeshRenderer>();
        pointerMeshRenderers[1] = m_figureScript.pointerGO.transform.GetChild(0).GetComponent<MeshRenderer>();
        pointerMeshRenderers[2] = m_figureScript.pointerGO.transform.GetChild(1).GetComponent<MeshRenderer>();

        pointerMeshRenderers[0].material.DOFade(1, 1);
        pointerMeshRenderers[1].material.DOFade(1, 1);
        pointerMeshRenderers[2].material.DOFade(0.3f, 1);
        pointerMeshRenderers[2].material.color = new Color(0.1f, 0.5f, 0.1f, pointerMeshRenderers[2].material.color.a);
        highlightCells(true);
    }

    public void highlightCells(bool Mode)
    {
        Collider[] hitColliders = Physics.OverlapSphere(m_figureScript.transform.position, maxTravelDistance/2);
        foreach (Collider collider in hitColliders)
        {
            if (collider.tag == "Hexagon")
            {
                Outline cellOutline = collider.GetComponent<Outline>();
                cellOutline.enabled = Mode;
                cellOutline.OutlineColor = Color.green;
            }
        }
    }

    // CustomBehaviour
    public override void CustomActionCursourBehaviour(Transform target, Transform previousTarget)
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

    public override InteracrScript CustomActionInteractionBehaviour(Transform target)
    {
        if (target.tag == "Hexagon")
        {
            float distanceFromCell = Vector3.Distance(target.transform.position, transform.parent.parent.parent.position);
            if (distanceFromCell < maxTravelDistance && distanceFromCell >= 0.8f)
            {
                m_figureScript.FigureModel.transform.DOLookAt(target.position, 0.3f, AxisConstraint.Y);
                m_figureScript.selectedAction = this;
                highlightCells(false);
                return null;
            }
            else
            {
                pointerMeshRenderers[0].material.color = new Color(0.85f, 0, 0);
                pointerMeshRenderers[0].material.DOColor(new Color(0.85f, 0, 0), 0.2f).OnComplete(() =>
                {
                    pointerMeshRenderers[0].material.DOColor(new Color(0.85f, 0.85f, 0.85f), 0.5f);
                });

                pointerMeshRenderers[1].material.color = new Color(0.85f, 0, 0);
                pointerMeshRenderers[1].material.DOColor(new Color(0.85f, 0, 0), 0.2f).OnComplete(() =>
                {
                    pointerMeshRenderers[1].material.DOColor(new Color(0.85f, 0.85f, 0.85f), 0.5f);
                });
                return this;
            }
        }
        else
            return this;
    }

    public override void StartAction()
    {
        print("BOOB");
    }
}
