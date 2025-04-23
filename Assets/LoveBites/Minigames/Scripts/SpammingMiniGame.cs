using System;
using System.Collections;
using UnityEngine;

public class SpammingMiniGame : BaseMiniGame
{
    [Header("Thresholds")]
    [SerializeField] private GameObject _lowThreshold;
    [SerializeField] private GameObject _midThreshold;
    [SerializeField] private float _lowBloodBoost = 1f;
    [SerializeField] private float _midBloodBoost = 2f;

    [Header("Data")]
    [SerializeField] private Vector3 _increasePerInteraction;
    [SerializeField] private Vector3 _decreaseAmountOverTime;
    [SerializeField] private Vector3 _maxScale;
    [SerializeField] private Vector3 _minScale;

    private Coroutine _decreaseAreaOverTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        _decreaseAreaOverTime = StartCoroutine(DecreaseAreaOverTime());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopCoroutine(_decreaseAreaOverTime);
    }

    protected override void OnPressButton()
    {
        IncreaseArea(_increasePerInteraction);
    }

    private void IncreaseArea(Vector3 amount)
    {
        Vector3 targetScale = _knob.transform.localScale + amount;
        _knob.transform.localScale = ClampVector3(targetScale, _minScale, _maxScale);
    }

    private void DecreaseArea(Vector3 amount)
    {
        Vector3 targetScale = _knob.transform.localScale - amount;
        _knob.transform.localScale = ClampVector3(targetScale, _minScale, _maxScale);
    }

    private IEnumerator DecreaseAreaOverTime() 
    {
        while (true) 
        {
            DecreaseArea(_decreaseAmountOverTime);
            CheckScore();
            yield return new WaitForSeconds(0.5f); 
        }
    }

    private void CheckScore()
    {
        if (_knob.transform.localScale.magnitude > _midThreshold.transform.localScale.magnitude)
        {
            _pointsData.OnBloodUpdated.Invoke(_bloodIncrease + _midBloodBoost);
            return;
        }

        if (_knob.transform.localScale.magnitude > _lowThreshold.transform.localScale.magnitude)
        {
            _pointsData.OnBloodUpdated.Invoke(_bloodIncrease + _lowBloodBoost);
            return;
        }

        _pointsData.OnBloodUpdated.Invoke(_bloodIncrease);
    }
}
