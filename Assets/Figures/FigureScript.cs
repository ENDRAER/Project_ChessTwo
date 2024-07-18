using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FigureScript : InteractScript
{
    [Header("Links")]
    [SerializeField] public MeshRenderer[] pointerMeshRenderers = new MeshRenderer[3]; // 0 - buttom thing : 1 - conus : 2 - shadow
    [SerializeField] public GameObject pointerGO;
    [SerializeField] public GameObject FigureModel;
    [SerializeField] public GameObject actionMenuGO;
    [SerializeField] public GameObject cancelActionGO;
    [NonSerialized] public Action selectedAction;
    [Header("Stats")]
    [SerializeField] public List<GameObject> allActionsPF = new List<GameObject>();
    [SerializeField] public float health;


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

    public void GetTheDamage(GameObject Sender, float Damage)
    {
        health -= Damage;
    }
}