using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [NonSerialized] public static MatchController StaticMatchController;

    [NonSerialized] public FigureScript[] FigureScripts;
    [NonSerialized] public InteracrScript selectedAction;

    private void Awake()
    {
        StaticMatchController = this;
    }

    public void StartNextTact()
    {
        foreach (var figure in FigureScripts)
        {
            figure.selectedAction.ActingOnTact();
        }
    }
}
