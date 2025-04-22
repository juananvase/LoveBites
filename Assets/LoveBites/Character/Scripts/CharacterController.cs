using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private LayerMask _clickableLayer = 1 << 0;
    private InputSystem_Actions _input;

    private CharacterMovement _characterMovement;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();

        _input = new InputSystem_Actions();
        AssignInput();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void AssignInput()
    {
        _input.Player.Interact.performed += ctx => OnLeftClick();
    }

    private void OnLeftClick() 
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, _clickableLayer))
        {
            hit.transform.TryGetComponent(out IInteractable interactableObject);

            if (interactableObject != null) 
            {
                interactableObject.Interact(hit);
                return;
            }

            if (_characterMovement != null)
            {
                _characterMovement.MoveToPosition(hit.point);
            }
        }
    }
}
