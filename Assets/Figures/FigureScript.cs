using DG.Tweening;
using System;
using UnityEngine;

public class FigureScript : InteractScript
{
    [SerializeField] public GameObject FigureModel;
    [SerializeField] public GameObject actionMenuGO;
    [SerializeField] public GameObject cancelActionGO;
    [SerializeField] public GameObject pointerGO;
    [NonSerialized] public Action selectedAction;


    public override void Interacting()
    {
        if(selectedAction == null)
            actionMenuGO.transform.DOScale(Vector3.one, 0.1f);
        else
            cancelActionGO.transform.DOScale(Vector3.one, 0.1f);
    }

    public override void CustomActionCursourBehaviour(Transform target, Transform previousTarget)
    {
        
    }

    public override InteractScript CustomActionInteractionBehaviour(Transform target)
    {
        actionMenuGO.transform.DOScale(Vector3.zero, 0.1f);
        cancelActionGO.transform.DOScale(Vector3.zero, 0.1f);
        if (target.tag == "ActionButton")
            return target.GetComponent<InteractScript>();
        else
            return null;
    }

    private void Start()
    {
        MatchController.StaticMatchController.FigureScripts.Add(this);
    }
}