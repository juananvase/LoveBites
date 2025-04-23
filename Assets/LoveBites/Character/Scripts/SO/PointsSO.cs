using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PointsSO", menuName = "Scriptable Objects/PointsSO")]
public class PointsSO : ScriptableObject
{
    [Header("Blood")]
    [field: SerializeField] public float Blood { get; private set; } = 0f;
    [field: SerializeField] public FloatEventAsset OnBloodUpdated { get; private set; }
    [field: SerializeField] public EmptyEventAsset OnResetBlood { get; private set; }

    [Header("Appeal")]
    [field: SerializeField] public float Appeal { get; private set; } = 0f;
    [field: SerializeField] public FloatEventAsset OnAppealUpdated { get; private set; }
    [field: SerializeField] public EmptyEventAsset OnResetAppeal { get; private set; }

    private void OnEnable()
    {
        OnBloodUpdated.OnInvoked.AddListener(UpdateBlood);
        OnResetBlood.OnInvoked.AddListener(ResetBlood);

        OnAppealUpdated.OnInvoked.AddListener(UpdateAppeal);
        OnResetAppeal.OnInvoked.AddListener(ResetAppeal);

    }
    private void OnDisable()
    {
        OnBloodUpdated.OnInvoked.RemoveListener(UpdateBlood);
        OnResetBlood.OnInvoked.RemoveListener(ResetBlood);

        OnAppealUpdated.OnInvoked.RemoveListener(UpdateAppeal);
        OnResetAppeal.OnInvoked.RemoveListener(ResetAppeal);
    }

    private void UpdateBlood(float blood)
    {
        Blood += blood;
        Blood = Mathf.Clamp(Blood, 0f, float.MaxValue);
    }

    private void UpdateAppeal(float appeal)
    {
        Appeal += appeal;
        Appeal = Mathf.Clamp(Appeal, 0f, float.MaxValue);
    }
    private void ResetBlood()
    {
        Blood = 0f;
    }

    private void ResetAppeal()
    {
        Appeal = 0f;
    }
}
