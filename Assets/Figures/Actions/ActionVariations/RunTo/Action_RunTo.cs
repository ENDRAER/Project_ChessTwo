using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Action_RunTo : Action
{
    [NonSerialized] private float maxTravelDistance = 1.7f;
    [NonSerialized] public float MovingSpeed = 0.03f;
    [NonSerialized] public float jumpHeigh = 0.5f;

    public override void Interacting()
    {
        m_figureScript.pointerGO.SetActive(true);
        m_figureScript.selectedAction = this;

        m_figureScript.pointerMeshRenderers[0].material.DOFade(1, 1);
        m_figureScript.pointerMeshRenderers[1].material.DOFade(1, 1);
        m_figureScript.pointerMeshRenderers[2].material.DOFade(0.3f, 1);
        m_figureScript.pointerMeshRenderers[2].material.color = new Color(0.1f, 0.5f, 0.1f, m_figureScript.pointerMeshRenderers[2].material.color.a);
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

    public override void CustomActionCursourBehaviour(Transform target, Transform previousTarget)
    {
        if (target.tag == "Hexagon")
        {
            float distanceFromCell = Vector3.Distance(target.transform.position, m_figureScript.transform.position);
            if (distanceFromCell < maxTravelDistance && distanceFromCell >= 0.8f)
                m_figureScript.pointerMeshRenderers[2].material.color = new Color(0.1f, 0.5f, 0.1f, m_figureScript.pointerMeshRenderers[2].material.color.a);
            else
                m_figureScript.pointerMeshRenderers[2].material.color = new Color(0.5f, 0.1f, 0.1f, m_figureScript.pointerMeshRenderers[2].material.color.a);
            m_figureScript.pointerGO.transform.DOKill();
            m_figureScript.pointerGO.transform.DOMove(target.position, 0.1f);
        }
    }

    public override InteractScript CustomActionInteractionBehaviour(Transform target)
    {
        if (target.tag == "Hexagon")
        {
            float distanceFromCell = Vector3.Distance(target.transform.position, transform.parent.parent.parent.position);
            if (distanceFromCell < maxTravelDistance && distanceFromCell >= 0.8f)
            {
                highlightCells(false);
                return null;
            }
            else
            {
                m_figureScript.pointerMeshRenderers[0].material.color = new Color(0.85f, 0, 0);
                m_figureScript.pointerMeshRenderers[0].material.DOColor(new Color(0.85f, 0, 0), 0.2f).OnComplete(() =>
                {
                    m_figureScript.pointerMeshRenderers[0].material.DOColor(new Color(0.85f, 0.85f, 0.85f), 0.5f);
                });

                m_figureScript.pointerMeshRenderers[1].material.color = new Color(0.85f, 0, 0);
                m_figureScript.pointerMeshRenderers[1].material.DOColor(new Color(0.85f, 0, 0), 0.2f).OnComplete(() =>
                {
                    m_figureScript.pointerMeshRenderers[1].material.DOColor(new Color(0.85f, 0.85f, 0.85f), 0.5f);
                });
                return this;
            }
        }
        else
            return this;
    }

    public override void CustomAction()
    {
        StartCoroutine(TransleteToDirection());
    }

    private IEnumerator TransleteToDirection()
    {
        Transform m_figureTransform = m_figureScript.transform;
        Vector3 firstFigurePos = m_figureTransform.position;
        Vector3 lastPointerPos = m_figureScript.pointerGO.transform.position;
        float firstDistance = Vector3.Distance(firstFigurePos, lastPointerPos);
        float distance = 100;
        Vector3 directiom = (m_figureTransform.position - lastPointerPos).normalized;
        Vector3 previousLinearPosition = m_figureTransform.position;

        while (distance >= MovingSpeed)
        {
            m_figureTransform.position = previousLinearPosition;
            distance = Vector3.Distance(new Vector3(m_figureTransform.position.x, 0 , m_figureTransform.position.z), new Vector3(lastPointerPos.x, 0, lastPointerPos.z));
            m_figureTransform.position -= MovingSpeed * directiom;
            previousLinearPosition = m_figureTransform.position;

            if (1 - (1f / firstDistance * distance) < 0.7f)
                m_figureTransform.position += new Vector3(0, (float)Math.Sin(1f / (firstDistance * 0.7f) * (firstDistance - distance) * 90 * (Math.PI / 180f)) * jumpHeigh, 0);
            else
                m_figureTransform.position += new Vector3(0, (float)Math.Cos(1f / (firstDistance * 0.3f) * (firstDistance - distance - firstDistance * 0.7f) * 90 * (Math.PI / 180f)) * jumpHeigh, 0);

            m_figureScript.pointerGO.transform.position = lastPointerPos;
            yield return new WaitForFixedUpdate();
        }
        m_figureTransform.position = lastPointerPos;
        DisableAction();
    }

    public override void DisableAction()
    {
        m_figureScript.actionMenuGO.transform.DOScale(Vector3.zero, 0.1f);
        m_figureScript.cancelActionGO.transform.DOScale(Vector3.zero, 0.1f);
        m_figureScript.pointerGO.SetActive(false);
        m_figureScript.selectedAction = null;
    }
}
