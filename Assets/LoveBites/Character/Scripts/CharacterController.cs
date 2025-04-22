using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System;

public class CharacterController : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private LayerMask _interactableLayer;

    [Header("Movement")]
    [SerializeField] private LayerMask _clickableLayer;

    private InputSystem_Actions _input;
    private NavMeshAgent _agent;

    [Header("VFX")]
    [SerializeField] private GameObject _clickEffectPrefab;

    private Renderer _clickEffect;
    private readonly int _clickTime = Shader.PropertyToID("_ClickTime");


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _input = new InputSystem_Actions();
        AssignInput();

        if (_clickEffectPrefab != null)
        {
            GameObject clickEffect = Instantiate(_clickEffectPrefab, new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0));
            _clickEffect = clickEffect.GetComponent<Renderer>();
        }
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
        _input.Player.Interact.performed += ctx => Interact();
    }

    private void Interact() 
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, _clickableLayer))
        {
            _agent.destination = hit.point;

            SpawnClickEffect(hit);
        }
    }

    private void SpawnClickEffect(RaycastHit hit)
    {
        if (_clickEffect != null)
        {
            _clickEffect.transform.position = hit.point + Vector3.up * 0.01f;
            _clickEffect.material.SetFloat(_clickTime, Time.time);
        }
    }
}
