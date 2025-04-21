using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;

public class CharacterController : MonoBehaviour
{
    private InputSystem_Actions _input;
    private NavMeshAgent _agent;

    [Header("Movement")]
    [SerializeField] private ParticleSystem _clickEffect;
    [SerializeField] private LayerMask _clickableLayer;

    private float _lookRotationSpeed = 8f;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

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
        _input.Player.Move.performed += ctx => ClickToMove();
    }

    private void ClickToMove() 
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, _clickableLayer))
        {
            _agent.destination = hit.point;
            if (_clickEffect != null)
            {
                Instantiate(_clickEffect, hit.point += new Vector3(0f, 0.1f, 0f), _clickEffect.transform.rotation);

            }
        }
    }
}
