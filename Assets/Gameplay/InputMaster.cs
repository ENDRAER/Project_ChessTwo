using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMaster : MonoBehaviour
{
    [SerializeField] public Camera Camera;
    [SerializeField] public GameObject CameraGO;
    [SerializeField] public MatchController _matchController;
    [NonSerialized] public InputManager _inputManager;
    [NonSerialized] public Transform previousTarget;


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
        previousTarget = previousTarget == null ? transform : previousTarget;
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
        Transform hitedTranform = hit.transform == null ? transform : hit.transform;
        if (hitedTranform != null && hitedTranform.gameObject.layer == 3 && _matchController.selectedAction == null && hitedTranform.tag != "Hexagon")
        {
            hitedTranform.GetComponent<InteractScript>().Interacting();
            if(hitedTranform.GetComponent<InteractScript>().customBehaviour)
                _matchController.selectedAction = hitedTranform.GetComponent<InteractScript>();
        }
        else if (_matchController.selectedAction != null)
        {
            InteractScript returnedInteractScript = _matchController.selectedAction.CustomActionInteractionBehaviour(hitedTranform);
            if (returnedInteractScript == null)
                _matchController.selectedAction = null; 
            else if (_matchController.selectedAction != returnedInteractScript)
            {
                returnedInteractScript.Interacting();
                if (!returnedInteractScript.customBehaviour)
                    _matchController.selectedAction = null;
                else
                    _matchController.selectedAction = returnedInteractScript;
            }
        }
    }
}
