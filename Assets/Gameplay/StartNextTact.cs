using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNextTact : InteracrScript
{
    [SerializeField] private MatchController _matchController; 

    public override void Interacting()
    {
        _matchController.StartNextTact();
    }
}
