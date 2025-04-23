using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MoveAgentToParkingPoint : MonoBehaviour, IInteractable
{
    [Header("Movement")]
    [SerializeField] private Vector3EventAsset _onMoveAgentToPoint;
    [SerializeField] private Vector3EventAsset _onDestinationReached;
    [SerializeField] private Transform _parkingPoint;

    [Header("Character")]
    [SerializeField] private GameplayStateEventAsset _onChangeGameplayState;

    [Header("MiniGame")]
    public UnityEvent OnStartMinigame;

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
            _onChangeGameplayState.Invoke(GameplayState.MiniGame);
            OnStartMinigame.Invoke();
        }
    }
}
