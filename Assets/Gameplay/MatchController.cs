using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [NonSerialized] public static MatchController StaticMatchController;

    [NonSerialized] public List<FigureScript> FigureScripts = new List<FigureScript>();
    [NonSerialized] public InteractScript selectedAction;

    private void Awake()
    {
        StaticMatchController = this;
    }

    public void StartNextTact()
    {
        foreach (FigureScript figure in FigureScripts)
        {
            if (figure.selectedAction != null)
            {
                figure.selectedAction.StartAction();
                figure.selectedAction = null;
            }
        }
    }
}
