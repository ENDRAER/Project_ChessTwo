using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Action_RunTo : Action
{
    [NonSerialized] private float maxTravelDistance = 901.7f;
    [NonSerialized] private MeshRenderer[] pointerMeshRenderers = new MeshRenderer[3]; // 0 - buttom thing 1 - conus 2 - shadow
    [NonSerialized] public float MovingSpeed = 0.005f;

    public override void Interacting()
    {
        m_figureScript.pointerGO.SetActive(true);
        m_figureScript.selectedAction = this;

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
            float distanceFromCell = Vector3.Distance(target.transform.position, m_figureScript.transform.position);
            if (distanceFromCell < maxTravelDistance && distanceFromCell >= 0.8f)
                pointerMeshRenderers[2].material.color = new Color(0.1f, 0.5f, 0.1f, pointerMeshRenderers[2].material.color.a);
            else
                pointerMeshRenderers[2].material.color = new Color(0.5f, 0.1f, 0.1f, pointerMeshRenderers[2].material.color.a);
            m_figureScript.pointerGO.transform.DOKill();
            m_figureScript.pointerGO.transform.DOMove(new Vector3(target.position.x, target.position.y + 0.5f, target.position.z), 0.1f);
        }
    }

    public override InteractScript CustomActionInteractionBehaviour(Transform target)
    {
        if (target.tag == "Hexagon")
        {
            float distanceFromCell = Vector3.Distance(target.transform.position, transform.parent.parent.parent.position);
            if (distanceFromCell < maxTravelDistance && distanceFromCell >= 0.8f)
            {
                m_figureScript.FigureModel.transform.DOLookAt(target.position, 0.3f, AxisConstraint.Y);
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
        StartCoroutine(TransleteToDirection());
    }

    private IEnumerator TransleteToDirection()
    {
        Transform m_figureTransform = m_figureScript.transform;
        Transform m_pointerTransform = pointerMeshRenderers[0].transform;
        Vector3 directiom = m_figureTransform.position - m_pointerTransform.position;
        while (Vector3.Distance(m_figureTransform.position, m_pointerTransform.position) > 1.2)
        {
            float modifer = Vector3.Distance(m_figureTransform.position, m_pointerTransform.position) / 5;
            m_figureTransform.position -= directiom * (MovingSpeed * (modifer < 1 ? modifer : 1));
            yield return new WaitForEndOfFrame();
        }
    }

    public override void DisableAction()
    {
        m_figureScript.actionMenuGO.transform.DOScale(Vector3.zero, 0.1f);
        m_figureScript.cancelActionGO.transform.DOScale(Vector3.zero, 0.1f);
        m_figureScript.pointerGO.SetActive(false);
        m_figureScript.selectedAction = null;
    }
}
