using System.Collections;
using UnityEngine;

public abstract class BaseMiniGame : MonoBehaviour
{
    [Header("States")]
    [SerializeField] private MinigGameEffect _miniGameEffect = MinigGameEffect.Positive;

    [Header("Knob")]
    [SerializeField] protected EmptyEventAsset _onPressButton;
    [SerializeField] protected EmptyEventAsset _onReleaseButton;
    [SerializeField] protected GameObject _knob;

    [Header("Points")]
    [SerializeField] protected PointsSO _pointsData;
    [SerializeField] protected float _bloodIncrease = 1f;
    [SerializeField] protected float _AppealingIncrease = 1f;
    [SerializeField] protected float _bloodDecrease = -0.5f;
    [SerializeField] protected float _AppealingDecrease = -0.5f;

    [Header("Data")]
    [SerializeField] protected float _miniGameDuration = 30f;
    [SerializeField] private GameplayStateEventAsset _onChangeGameplayState;

    protected virtual void OnEnable()
    {
        _onPressButton.AddListener(OnPressButton);
        _onReleaseButton.AddListener(OnReleaseButton);

        StartCoroutine(CheckTime());
    }

    protected virtual void OnDisable()
    {
        _onPressButton.RemoveListener(OnPressButton);
        _onReleaseButton.RemoveListener(OnReleaseButton);

        StopCoroutine(CheckTime());
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

    private IEnumerator CheckTime() 
    {
        yield return new WaitForSeconds(_miniGameDuration);

        _onChangeGameplayState.Invoke(GameplayState.Driving);

        if (_miniGameEffect == MinigGameEffect.Negative)
            _pointsData.OnBloodUpdated.Invoke(_bloodDecrease);

        gameObject.SetActive(false);
    }
}

public enum MinigGameEffect
{
    Positive,
    Negative
}
