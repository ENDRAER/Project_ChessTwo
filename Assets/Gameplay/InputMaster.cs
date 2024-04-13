using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMaster : MonoBehaviour
{
    public Camera Camera;
    public GameObject CameraGO;
    public InputManager _inputManager;
    public Transform previousInducedGO;


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
        if (hit.transform != null && hit.transform.tag == ("Interactable"))
        {
            if(previousInducedGO != null)
                previousInducedGO.GetComponent<Outline>().enabled = false;
            hit.transform.GetComponent<Outline>().enabled = true;
            previousInducedGO = hit.transform;
        }
        else if (previousInducedGO != null)
        {
            previousInducedGO.GetComponent<Outline>().enabled = false;
            previousInducedGO = null;
        }
    }

    public void StartInteracting(InputAction.CallbackContext cbContext)
    {
        if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50) && hit.transform.tag == ("Interactable"))
        {
            hit.transform.GetComponent<InteracrScript>().Interacting();
        }
    }
}
