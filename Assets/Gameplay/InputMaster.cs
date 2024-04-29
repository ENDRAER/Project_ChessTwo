using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMaster : MonoBehaviour
{
    [SerializeField] public Camera Camera;
    [SerializeField] public GameObject CameraGO;
    [SerializeField] public MatchController _matchController;
    [NonSerialized] public InputManager _inputManager;
    [SerializeField] public Transform previousTarget;


    private void Awake()
    {
        _inputManager = new InputManager();
        _inputManager.Enable();
    }

    private void OnEnable()
    {
        _inputManager.Gameplay.PrimaryButton.performed += StartInteracting;
    }

    private void Update()
    {
        Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50);
        if (hit.transform != null && hit.transform != previousTarget)
        {
            if (_matchController.selectedAction == null)
            {
                if (previousTarget != null)
                    previousTarget.GetComponent<Outline>().enabled = false;
                if (hit.transform.gameObject.layer == 3 && hit.transform.tag != "Hexagon")
                {
                    hit.transform.GetComponent<Outline>().enabled = true;
                    previousTarget = hit.transform;
                }
            }
            else
                _matchController.selectedAction.CustomActionCursourOnBehaviour(hit.transform, previousTarget);
        } // on cursour target GO
        else if (hit.transform != null && hit.transform != previousTarget)
        {
            if (_matchController.selectedAction == null)
            {
                if (previousTarget != null)
                    previousTarget.GetComponent<Outline>().enabled = false;
                previousTarget = null;
            }
            else
                _matchController.selectedAction.CustomActionCursourOnBehaviour(hit.transform, previousTarget);
        } // on cursour untarget GO
    }

    public void StartInteracting(InputAction.CallbackContext cbContext)
    {
        Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50);
        if (hit.transform != null && hit.transform.gameObject.layer == 3)
        {
            if(_matchController.selectedAction == null || hit.transform.tag != "Hexagon")
                hit.transform.GetComponent<InteracrScript>().Interacting();
            else if(_matchController.selectedAction != null)
                _matchController.selectedAction.CustomActionInteractionBehaviour(hit.transform);
        }
    }
}
