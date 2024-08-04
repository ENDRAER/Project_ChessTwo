using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Action_ArrowShot : Action
{
    [SerializeField] private GameObject ArrowPF;
    [SerializeField] private float arrowSpeed;
    [NonSerialized] private float maxShotDistance = 901.7f;

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
        Collider[] hitColliders = Physics.OverlapSphere(m_figureScript.transform.position, maxShotDistance / 2);
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
            if (distanceFromCell < maxShotDistance && distanceFromCell >= 0.8f)
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
            if (distanceFromCell < maxShotDistance && distanceFromCell >= 0.8f)
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
        Rigidbody ArrowRB = Instantiate(ArrowPF, new Vector3(m_figureScript.transform.position.x, m_figureScript.transform.position.y + 0.5f, m_figureScript.transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
        Vector3 pointerPos = m_figureScript.pointerGO.transform.position;
        ArrowRB.transform.position += (pointerPos - ArrowRB.transform.position).normalized / 3;
        ArrowRB.transform.LookAt(new Vector3(pointerPos.x, pointerPos.y + 0.5f, pointerPos.z), Vector3.forward);
        ArrowRB.AddForce(ArrowRB.transform.forward * arrowSpeed, ForceMode.Impulse);
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
