using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BalancingMiniGame : BaseMiniGame
{
    [Header("Setup")]
    [SerializeField] private Slider _slider;
    [SerializeField] private RectTransform _hitArea;

    [Header("Data")]
    [SerializeField] private float _duration = 1.5f;
    [SerializeField] private float _clickOffset = 0.05f;
    [SerializeField] private float _initialHitPointDelay = 0.5f;
    [SerializeField] private float _minHitPointDelay = 0.25f;
    [SerializeField] private float _hitPointDelayBoost = 0.05f;

    private float _hitValue;
    private float _hitPointDelay;

    private Coroutine _moveKnob = null;
    private Coroutine _checkHitPoint = null;

    protected override void OnEnable()
    {
        base.OnEnable();
        _hitValue = SetRandomArea(_hitArea, _slider.minValue, _slider.maxValue);
        _hitPointDelay = _initialHitPointDelay;

        _checkHitPoint = StartCoroutine(CheckHitPoint());
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (_moveKnob != null)
        {
            StopCoroutine(_moveKnob);
        }

        if (_checkHitPoint != null)
        {
            StopCoroutine(_checkHitPoint);
        }
    }

    protected override void OnPressButton()
    {
        StartKnobMovement(_slider.maxValue);
    }

    protected override void OnReleaseButton()
    {
        StartKnobMovement(_slider.minValue);
    }

    private void StartKnobMovement(float targetValue)
    {
        if (_moveKnob != null)
        {
            StopCoroutine(_moveKnob);
        }
        _moveKnob = StartCoroutine(MoveKnob(targetValue));
    }

    private IEnumerator MoveKnob(float targetValue)
    {
        float currentValue = _slider.value;
        float timer = 0f;
        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float progress = timer / _duration;
            float value = Mathf.Lerp(currentValue, targetValue, progress);
            _slider.value = value;

            yield return null;
        }

        _moveKnob = null;
    }

    private IEnumerator CheckHitPoint()
    {
        while (true) 
        {
            if (_slider.value <= _hitValue + _clickOffset && _slider.value >= Mathf.Abs(_hitValue - _clickOffset))
            {
                _pointsData.OnAppealUpdated.Invoke(_AppealingIncrease);

                _hitPointDelay -= _hitPointDelayBoost;
                _hitPointDelay = Mathf.Clamp(_hitPointDelay, _minHitPointDelay ,_initialHitPointDelay);
                yield return new WaitForSeconds(_hitPointDelay);
            }
            else
            {
                _pointsData.OnAppealUpdated.Invoke(_AppealingDecrease);

                _hitPointDelay = _initialHitPointDelay;
                yield return new WaitForSeconds(_hitPointDelay);
            }
        }
    }
}
