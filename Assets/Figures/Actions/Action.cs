using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public HexagonGrid _hexagonGrid;
    public MatchController _matchController;

    private void Awake()
    {
        _hexagonGrid = HexagonGrid.StaticHexagonGrid;
        _matchController = MatchController.StaticMatchController;
    }

    public virtual void TakeAction()
    {
        
    }

    public virtual void highlightCells() 
    {
        
    }
}
