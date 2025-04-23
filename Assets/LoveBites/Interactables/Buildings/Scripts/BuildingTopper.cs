using PrimeTween;
using UnityEngine;

public class BuildingTopper : MonoBehaviour
{
    [SerializeField] private GameObject _topperObject;

    private void Awake()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        Sequence.Create(cycles: -1, cycleMode: CycleMode.Restart)
            .Group(Tween.EulerAngles(_topperObject.transform, Vector3.zero, new Vector3(0, 360), 12, Ease.Linear));

        Sequence.Create(cycles: -1, CycleMode.Yoyo)
            .Group(Tween.LocalPositionY(_topperObject.transform, 0, 1, .6f, Ease.OutQuad));
    }
}
