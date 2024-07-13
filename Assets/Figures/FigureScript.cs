using DG.Tweening;
using System;
using UnityEngine;

public class FigureScript : InteracrScript
{
    [SerializeField] public GameObject FigureModel;
    [SerializeField] public GameObject actionMenuGO;
    [SerializeField] public GameObject pointerGO;
    [NonSerialized] public Action selectedAction;


    public override void Interacting()
    {
        actionMenuGO.transform.DOScale(Vector3.one, 0.1f);
    }

    public override void CustomActionCursourBehaviour(Transform target, Transform previousTarget)
    {
        
    }

    public override InteracrScript CustomActionInteractionBehaviour(Transform target)
    {
        if (target.tag == "ActionButton")
        {
            return target.GetComponent<InteracrScript>();
        }
        else
        {
            actionMenuGO.transform.DOScale(Vector3.zero, 0.1f);
            return null;
        }
    }

    private void Start()
    {
        MatchController.StaticMatchController.FigureScripts.Add(this);
    }
}