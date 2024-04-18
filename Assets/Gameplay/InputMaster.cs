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
    [SerializeField] public Transform previousInduced;


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
        if (hit.transform != null && hit.transform != previousInduced)
        {
            if (previousInduced != null)
                previousInduced.GetComponent<Outline>().enabled = false;
            hit.transform.GetComponent<Outline>().enabled = true;
            previousInduced = hit.transform;
        }
        else if (hit.transform == null && hit.transform != previousInduced)
        {
            if (previousInduced != null)
                previousInduced.GetComponent<Outline>().enabled = false;
            previousInduced = null;
        }
    }

    public void StartInteracting(InputAction.CallbackContext cbContext)
    {
        Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50);
        if (hit.transform != null)
        {
            hit.transform.GetComponent<InteracrScript>().Interacting();
        }
    }
}
