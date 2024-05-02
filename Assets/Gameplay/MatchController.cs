using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [NonSerialized] public static MatchController StaticMatchController;

    [SerializeField] public enum States {defaultState, spectator, cellChoisechising }
    [NonSerialized] public States CurentState;
    [NonSerialized] public FigureScript[] FigureScripts;
    [NonSerialized] public Action selectedAction;

    private void Awake()
    {
        StaticMatchController = this;
    }

    public void StartNextTact()
    {
        foreach (var figure in FigureScripts)
        {
            figure.SelectedAction.ActingOnTact();
        }
    }
}
