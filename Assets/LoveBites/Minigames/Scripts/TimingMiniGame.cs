using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimingMiniGame : BaseMiniGame
{
    [Header("Setup")]
    [SerializeField] private Slider _slider;
    [SerializeField] private RectTransform _hitArea;

    [Header("Data")]
    [SerializeField] private float _duration = 1.5f;
    [SerializeField] private float _initialSpeedMultiplier = 1f;
    [SerializeField] private float _maxSpeedMultiplier = 2f;
    [SerializeField] private float _speedBoost = 0.5f;
    [SerializeField] private float _clickOffset = 0.05f;

    private float _hitValue;
    private float _speedMultiplier;

    private Coroutine _moveKnob = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        _speedMultiplier = _initialSpeedMultiplier;
        StartKnobMovement(_slider.maxValue);
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (_moveKnob != null)
        {
            StopCoroutine(_moveKnob);
        }
    }

    protected override void OnPressButton()
    {
        CheckHitPoint();
        _hitValue = SetRandomArea(_hitArea, _slider.minValue, _slider.maxValue);
    }

    private void CheckHitPoint()
    {
        if (_slider.value <= _hitValue + _clickOffset && _slider.value >= Mathf.Abs(_hitValue - _clickOffset))
        {
            _pointsData.OnAppealUpdated.Invoke(_AppealingIncrease);

            _speedMultiplier += _speedBoost;
            _speedMultiplier = Mathf.Clamp(_speedMultiplier, _initialSpeedMultiplier, _maxSpeedMultiplier);
        }
        else
        {
            _pointsData.OnAppealUpdated.Invoke(_AppealingDecrease);

            _speedMultiplier = _initialSpeedMultiplier;
        }
    }

    private void StartKnobMovement(float targetValue) 
    {
        if (_moveKnob == null)
        {
            _moveKnob = StartCoroutine(MoveKnob(targetValue));
        }
    }

    private void OscillateKnob(float endValue)
    {
        if (Mathf.Approximately(endValue, _slider.maxValue))
        {
            StartKnobMovement(_slider.minValue);
        }
        else
        {
            StartKnobMovement(_slider.maxValue);
        }
    }

    private IEnumerator MoveKnob(float targetValue)
    {
        float currentValue = _slider.value;
        float timer = 0f;
        while (timer < _duration)
        {
            timer += Time.deltaTime * _speedMultiplier;
            float progress = timer / _duration;
            float value = Mathf.Lerp(currentValue, targetValue, progress);
            _slider.value = value;

            yield return null;
        }

        _moveKnob = null;
        OscillateKnob(_slider.value);
    }
}
