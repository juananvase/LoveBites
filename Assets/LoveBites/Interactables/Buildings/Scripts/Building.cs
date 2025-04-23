using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour, IInteractable
{
    [Header("Building")]
    [SerializeField] private BuildingState _buildingState;
    [SerializeField] private GameObject _indicator;
    [SerializeField] private EmptyEventAsset _onFinishActivity;


    [Header("Passanger")]
    [SerializeField] private PassengerSO _passangerData;

    [Header("Movement")]
    [SerializeField] private Vector3EventAsset _onMoveAgentToPoint;
    [SerializeField] private Vector3EventAsset _onDestinationReached;
    [SerializeField] private Transform _parkingPoint;

    [Header("Character")]
    [SerializeField] private GameplayStateEventAsset _onChangeGameplayState;

    [Header("MiniGame")]
    [SerializeField] private PointsSO _pointsData;
    public UnityEvent OnStartMinigame;

    private void Awake()
    {
        ChangeState(BuildingState.Idle);
        _indicator.SetActive(false);
    }

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
            switch (_buildingState)
            {
                case BuildingState.Pickup:
                    _onFinishActivity.Invoke();
                    _passangerData.PickedUp = true;
                    _buildingState = BuildingState.Idle;
                    break;
                case BuildingState.Drop:
                    _onFinishActivity.Invoke();
                    _passangerData.PickedUp = false;
                    _pointsData.OnResetBlood.Invoke();
                    _buildingState = BuildingState.Idle;

                    break;
                case BuildingState.Play:
                    _onChangeGameplayState.Invoke(GameplayState.MiniGame);
                    OnStartMinigame.Invoke();
                    _buildingState = BuildingState.Idle;
                    break;
            }
        }
    }

    public void ChangeState(BuildingState state) 
    {
        _buildingState=state;
    }

    public void IndicatorOn() 
    {
        _indicator.SetActive(true);
    }
    public void IndicatorOff()
    {
        _indicator.SetActive(false);
    }
}

public enum BuildingState
{
    Idle,
    Pickup,
    Drop,
    Play
}
