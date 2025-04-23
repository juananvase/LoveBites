using System;
using TMPro;
using UnityEngine;

public class UpdateBloodCount : MonoBehaviour
{
    [SerializeField] private PointsSO _pointsData;
    [SerializeField] private TextMeshProUGUI _message;

    private void OnEnable()
    {
        _pointsData.OnBloodUpdated.OnInvoked.AddListener(UpdateBlood);
    }

    private void OnDisable()
    {
        _pointsData.OnBloodUpdated.OnInvoked.RemoveListener(UpdateBlood);
    }
    private void UpdateBlood(float value)
    {
        _message.SetText("Blood: "+_pointsData.Blood);
    }

}
