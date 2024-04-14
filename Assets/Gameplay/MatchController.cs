using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField] public enum States {defaultState, spectator, cellChoisechising }
    [NonSerialized] public FigureScript selectedFigure;
    [NonSerialized] public static MatchController StaticMatchController;
}
