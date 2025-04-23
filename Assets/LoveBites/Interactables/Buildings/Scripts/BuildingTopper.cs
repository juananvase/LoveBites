using PrimeTween;
using UnityEngine;

public class BuildingTopper : MonoBehaviour
{
    [SerializeField] private GameObject _topperObject;

    private Sequence turn;
    private Sequence bob;

    private void OnEnable()
    {
        StartAnimation();
    }

    private void OnDisable()
    {
        if (turn.isAlive) turn.Stop();
        if (bob.isAlive) bob.Stop();
    }

    private void StartAnimation()
    {
        turn = Sequence.Create(cycles: -1, cycleMode: CycleMode.Restart)
            .Group(Tween.EulerAngles(_topperObject.transform, Vector3.zero, new Vector3(0, 360), 12, Ease.Linear));

        bob = Sequence.Create(cycles: -1, CycleMode.Yoyo)
            .Group(Tween.LocalPositionY(_topperObject.transform, 0, 1, .6f, Ease.OutQuad));
    }
}
