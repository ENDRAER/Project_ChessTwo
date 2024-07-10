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
        Transform hitedTranform = hit.transform == null ? transform : hit.transform;
        if (hitedTranform != previousTarget)
        {
            if (_matchController.selectedAction == null)
            {
                if (previousTarget.GetComponent<Outline>() != null)
                    previousTarget.GetComponent<Outline>().enabled = false;
                if (hitedTranform.gameObject.layer == 3 && hitedTranform.tag != "Hexagon")
                    hitedTranform.GetComponent<Outline>().enabled = true;
                previousTarget = hitedTranform;
            }
            else
                _matchController.selectedAction.CustomActionCursourBehaviour(hitedTranform, previousTarget);
        }
    }

    public void StartInteracting(InputAction.CallbackContext cbContext)
    {
        Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50);
        if (hit.transform != null && hit.transform.gameObject.layer == 3)
        {
            if(_matchController.selectedAction == null && hit.transform.tag != "Hexagon")
                hit.transform.GetComponent<InteracrScript>().Interacting();
            else if(_matchController.selectedAction != null)
                _matchController.selectedAction.CustomActionInteractionBehaviour(hit.transform);
        }
    }
}
