using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenu : MonoBehaviour
{
    [SerializeField] protected FigureScript m_figureScript;
    [SerializeField] private GameObject m_Canvas;
    [NonSerialized] private List<GameObject> createdActionButtons = new List<GameObject>();
    [NonSerialized] private GameObject CameraGO;
    [NonSerialized] private float roundingOfActionButtons = 6;
    [NonSerialized] private float spacingOfActionButtons = 70;


    private void Start()
    {
        CameraGO = Camera.main.gameObject;
        ButtonSpawner();
    }

    public void ButtonSpawner()
    {
        foreach (GameObject action in m_figureScript.allActionsPF)
        {
            GameObject createdButton = Instantiate(action);
            createdButton.transform.SetParent(transform);
            createdButton.transform.localEulerAngles = Vector3.zero;
            createdButton.transform.localScale = Vector3.one;
            createdActionButtons.Add(createdButton);
        }
        ActionButtonsPositing();
    }

    public void ActionButtonsPositing()
    {
        float correctedSpacing = spacingOfActionButtons / roundingOfActionButtons;
        float maxAngle = 90 + (createdActionButtons.Count - 1) * (correctedSpacing / 2);
        for (int i = 0; i != createdActionButtons.Count; i++)
        {
            double curentAngle = (maxAngle - correctedSpacing * i) * (Math.PI / 180);
            createdActionButtons[i].transform.localPosition = new Vector3((float)Math.Cos(curentAngle) * roundingOfActionButtons, (float)Math.Sin(curentAngle) * roundingOfActionButtons - roundingOfActionButtons + 1.5f);
        }
    }

    void Update()
    {
        Vector3 v3 = CameraGO.transform.position;
        m_Canvas.transform.LookAt(v3);
    }
}
