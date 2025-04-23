using UnityEngine;

public abstract class BaseMiniGame : MonoBehaviour
{
    [Header("Knob")]
    [SerializeField] protected EmptyEventAsset _onPressButton;
    [SerializeField] protected EmptyEventAsset _onReleaseButton;

    [SerializeField] protected GameObject _knob;

    protected virtual void OnEnable()
    {
        _onPressButton.AddListener(OnPressButton);
        _onReleaseButton.AddListener(OnReleaseButton);
    }

    protected virtual void OnDisable()
    {
        _onPressButton.RemoveListener(OnPressButton);
        _onReleaseButton.RemoveListener(OnReleaseButton);
    }

    protected virtual void OnReleaseButton() { }

    protected virtual void OnPressButton() { }

    protected Vector3 ClampVector3(Vector3 target, Vector3 min, Vector3 max)
    {
        target.x = Mathf.Clamp(target.x, min.x, max.x);
        target.y = Mathf.Clamp(target.y, min.y, max.y);
        target.z = Mathf.Clamp(target.z, min.z, max.z);

        return target;
    }

    protected float SetRandomArea(RectTransform hitArea, float min, float max)
    {
        float targetAnchorX = Random.Range(min, max);
        hitArea.anchorMin = new Vector2(targetAnchorX, hitArea.anchorMin.y);
        hitArea.anchorMax = new Vector2(targetAnchorX, hitArea.anchorMax.y);

        hitArea.anchoredPosition = Vector2.zero;

        return targetAnchorX;
    }
}
