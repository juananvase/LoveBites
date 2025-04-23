using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PassengerSO", menuName = "Scriptable Objects/PassengerSO")]
public class PassengerSO : ScriptableObject
{
    [field: SerializeField] public bool PickedUp { get; set; } = false;
    [field: SerializeField] public Building Home { get; set; }
    [field: SerializeField] public float AppealThreshold { get; private set; } = 100f;
    [field: SerializeField] public float MaxAppealThreshold { get; private set; } = 500f;
    [field: SerializeField] public float MinAppealThreshold { get; private set; } = 100f;
    [field: SerializeField] public EmptyEventAsset OnGeneratePassangerData { get; private set; }

    private void OnEnable()
    {
        OnGeneratePassangerData.OnInvoked.AddListener(GenerateData);


    }
    private void OnDisable()
    {
        OnGeneratePassangerData.OnInvoked.RemoveListener(GenerateData);
    }

    private void GenerateData()
    {
        AppealThreshold = UnityEngine.Random.Range(MinAppealThreshold, MaxAppealThreshold);
    }
}
