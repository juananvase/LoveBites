using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;
using System.Collections;
using UnityEngine.Events;

public class CustomCharacterController : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private GameplayState _gameplayState;
    [SerializeField] private GameplayStateEventAsset _onChangeGameplayState;

    [Header("MiniGames")]
    [SerializeField] private EmptyEventAsset _onPressButton;
    [SerializeField] private EmptyEventAsset _onReleaseButton;

    [Header("Interaction")]
    [SerializeField] private LayerMask _clickableLayer = 1 << 0;
    [SerializeField] private LayerMask _hoverableLayer = 1 << 0;

    [Header("Movement")]
    [SerializeField] private Vector3EventAsset _onMoveAgentToPoint;

    private InputSystem_Actions _input;

    private void Awake()
    {
        _input = new InputSystem_Actions();
        AssignInput();
    }

    private void OnEnable()
    {
        _input.Enable();
        _onChangeGameplayState.AddListener(ChangeGameplayState);
    }

    private void OnDisable()
    {
        _input.Disable();
        _onChangeGameplayState.RemoveListener(ChangeGameplayState);

    }

    private void ChangeGameplayState(GameplayState targetGameplayState)
    {
        _gameplayState = targetGameplayState;
    }

    private void Start()
    {
        _gameplayState = GameplayState.Driving;

        StartCoroutine(CheckCursorHover());
    }

    private IEnumerator CheckCursorHover()
    {
        RaycastHit hit;
        while (true) 
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, _hoverableLayer))
            {
                hit.transform.TryGetComponent(out IHoverable hoverableObject);

                if (hoverableObject != null)
                {
                    hoverableObject.OnHover();
                }
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void AssignInput()
    {
        _input.Player.Interact.performed += ctx => OnLeftClick();
        _input.Player.Interact.canceled += ctx => OnLeftClickCancel();
    }

    private void OnLeftClickCancel()
    {
        if (_gameplayState == GameplayState.MiniGame)
        {
            _onReleaseButton.Invoke();
            return;
        }
    }

    private void OnLeftClick() 
    {
        if (_gameplayState == GameplayState.MiniGame)
        {
            _onPressButton.Invoke();
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, _clickableLayer))
        {
            hit.transform.TryGetComponent(out IInteractable interactableObject);

            if (interactableObject != null) 
            {
                interactableObject.OnInteract();
                return;
            }

            _onMoveAgentToPoint.Invoke(hit.point);
        }
    }
}

public enum GameplayState
{
    Driving,
    MiniGame
}
