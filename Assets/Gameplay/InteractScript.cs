using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{
    [SerializeField] public bool customBehaviour;

    public virtual void Interacting()
    {

    }

    public virtual void CustomActionCursourBehaviour(Transform target, Transform previousTarget)
    {

    }

    public virtual InteractScript CustomActionInteractionBehaviour(Transform target)
    {
        return null;
    }
}
