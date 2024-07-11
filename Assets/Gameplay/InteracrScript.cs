using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteracrScript : MonoBehaviour
{
    public virtual void Interacting()
    {

    }

    public virtual void CustomActionCursourBehaviour(Transform target, Transform previousTarget)
    {

    }

    public virtual InteracrScript CustomActionInteractionBehaviour(Transform target)
    {
        return null;
    }
}
