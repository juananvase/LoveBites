using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector3EventAsset _onMoveAgentToPoint;
    [SerializeField] private Vector3EventAsset _onDestinationReached;

    private Coroutine _checkDestinationReached;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        _onMoveAgentToPoint.AddListener(MoveToPosition);
    }

    private void OnDisable()
    {
        _onMoveAgentToPoint.RemoveListener(MoveToPosition);
    }

    private void MoveToPosition(Vector3 point)
    {
        _agent.destination = point;

        if (_checkDestinationReached != null) 
            StopCoroutine(_checkDestinationReached);

        _checkDestinationReached = StartCoroutine(CheckDestinationReached(point));
    }

    private IEnumerator CheckDestinationReached(Vector3 destination) 
    {
        while (_agent.transform.position.x != destination.x && _agent.transform.position.z != destination.z) 
        {
            yield return new WaitForSeconds(0.2f);
        }

        _onDestinationReached.Invoke(transform.position);
        yield return null;
    }
    
}
