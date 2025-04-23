using TMPro;
using UnityEngine;

public class UpdateAppealCount : MonoBehaviour
{
    [SerializeField] private PointsSO _pointsData;
    [SerializeField] private TextMeshProUGUI _message;

    private void OnEnable()
    {
        _pointsData.OnAppealUpdated.OnInvoked.AddListener(UpdateAppeal);
    }

    private void OnDisable()
    {
        _pointsData.OnAppealUpdated.OnInvoked.RemoveListener(UpdateAppeal);
    }
    private void UpdateAppeal(float value)
    {
        _message.SetText("Appeal: " + _pointsData.Appeal);
    }
}
