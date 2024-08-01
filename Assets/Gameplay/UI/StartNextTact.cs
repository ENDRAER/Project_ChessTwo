using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNextTact : InteractScript
{
    [SerializeField] private MatchController _matchController;

    public override void Interacting()
    {
        _matchController.StartNextTact();
    }
}
