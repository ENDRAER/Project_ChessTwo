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


    private void Awake()
    {
        _inputManager = new InputManager();
        _inputManager.Enable();
    }

    private void OnEnable()
    {
        _inputManager.Gameplay.PrimaryButton.performed += StartInteracting;
    }

    public void StartInteracting(InputAction.CallbackContext cbContext)
    {
        if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit) && hit.transform.tag == ("Interactable"))
        {
            hit.transform.GetComponent<InteracrScript>().Interacting();
        }
    }
}
