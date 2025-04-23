using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStage _gameStage;
    [SerializeField] private PointsSO _pointsData;
    [SerializeField] private PassengerSO _passangerData;
    [SerializeField] private EmptyEventAsset _onGeneratePassangerData;
    [SerializeField] private EmptyEventAsset _onFinishMinigame;
    [SerializeField] private EmptyEventAsset _onFinishActivity;


    [SerializeField] private List<Building> _homes = new();
    [SerializeField] private List<Building> _restaurants = new();
    [SerializeField] private List<Building> _coffes = new();
    [SerializeField] private List<Building> _clubs = new();


    private void OnEnable()
    {
        _onFinishMinigame.OnInvoked.AddListener(MoveToNextStage);
        _onFinishActivity.OnInvoked.AddListener(MoveToNextStage);


    }
    private void OnDisable()
    {
        _onFinishMinigame.OnInvoked.RemoveListener(MoveToNextStage);
        _onFinishActivity.OnInvoked.RemoveListener(MoveToNextStage);
    }

    private void Start()
    {
        _passangerData.Home = null;
        ChoosePlace(_homes, GameStage.Pickup, BuildingState.Pickup);
        _pointsData.OnResetBlood.Invoke();
        _pointsData.OnResetAppeal.Invoke();
    }

    public void ChangeGameStage(GameStage gameStage) 
    {
        _gameStage = gameStage;
    }

    public void MoveToNextStage()
    {
        switch (_gameStage)
        {
            case GameStage.Pickup:
                ChangeStateOfAllBuildings(_coffes, BuildingState.Play, true);
                _gameStage = GameStage.Coffe;
                break;
            case GameStage.Coffe:
                ChangeStateOfAllBuildings(_restaurants, BuildingState.Play, true);
                ChangeStateOfAllBuildings(_coffes, BuildingState.Idle, false);
                _gameStage = GameStage.Dinner;
                break;
            case GameStage.Dinner:
                ChangeStateOfAllBuildings(_clubs, BuildingState.Play, true);
                ChangeStateOfAllBuildings(_restaurants, BuildingState.Idle, false);
                _gameStage = GameStage.Club;
                break;
            case GameStage.Club:
                ChangeStateOfAllBuildings(_clubs, BuildingState.Idle, false);
                TakeGirlHome();
                _gameStage = GameStage.Drop;
                break;
            case GameStage.Drop:
                _passangerData.OnGeneratePassangerData.Invoke();
                ChoosePlace(_homes, GameStage.Pickup, BuildingState.Pickup);
                break;
        }
    }

    private void ChangeStateOfAllBuildings(List<Building> list, BuildingState buildingState, bool indicatorOn) 
    {
        foreach (Building building in list) 
        {
            building.ChangeState(buildingState);

            if(indicatorOn == true) building.IndicatorOn();
            else building.IndicatorOff();
        }
    }

    public void ChoosePlace(List<Building> list, GameStage gameStage, BuildingState buildingState) 
    {
        if (_passangerData.Home != null)
        {
            _passangerData.Home.ChangeState(BuildingState.Idle);
            _passangerData.Home.IndicatorOff();

        }

        _passangerData.OnGeneratePassangerData.Invoke();
        int randIndex = UnityEngine.Random.Range(0, list.Count);
        Building building = list[randIndex];
        building.ChangeState(buildingState);
        building.IndicatorOn();
        ChangeGameStage(gameStage);
        _passangerData.Home =  building;
    }

    public void TakeGirlHome() 
    {
        if(_passangerData.PickedUp == false) return;

        _passangerData.Home.IndicatorOn();

        if (_pointsData.Appeal >= _passangerData.AppealThreshold)
        {
            _passangerData.Home.ChangeState(BuildingState.Play);
            _passangerData.PickedUp = false;
        }
        else 
        {
            _passangerData.Home.ChangeState(BuildingState.Drop);
        }

        _pointsData.OnResetAppeal.Invoke();
    }

    public void GeneratePassangerData() 
    {
        _onGeneratePassangerData.Invoke();
    }

}

public enum GameStage
{
    Pickup,
    Coffe,
    Dinner,
    Club,
    Drop
}
