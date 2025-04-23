using Unity.VisualScripting;
using UnityEngine;

public class MoveAgentToParkingPoint : MonoBehaviour, IInteractable
{
    [Header("Movement")]
    [SerializeField] private Vector3EventAsset _onMoveAgentToPoint;
    [SerializeField] private Vector3EventAsset _onDestinationReached;

    [SerializeField] private Transform _parkingPoint;

    private void OnEnable()
    {
        _onDestinationReached.AddListener(CheckParkingPoint);
    }

    private void OnDisable()
    {
        _onDestinationReached.RemoveListener(CheckParkingPoint);
    }

    public void OnInteract()
    {
        _onMoveAgentToPoint.Invoke(_parkingPoint.position);
    }

    private void CheckParkingPoint(Vector3 playerPosition)
    {
        if (playerPosition.x == _parkingPoint.position.x && playerPosition.z == _parkingPoint.position.z) 
        {
            Debug.Log("Do the cool thing");
        }
    }
}
