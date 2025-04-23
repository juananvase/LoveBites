using UnityEngine;
using UnityEngine.AI;

public class ClickEffect : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Vector3EventAsset _onMoveAgentToPoint;

    [Header("VFX")]
    [SerializeField] private GameObject _clickEffectPrefab;

    private Renderer _clickEffect;
    private readonly int _clickTime = Shader.PropertyToID("_ClickTime");

    private void Awake()
    {
        if (_clickEffectPrefab != null)
        {
            GameObject clickEffect = Instantiate(_clickEffectPrefab, new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0));
            _clickEffect = clickEffect.GetComponent<Renderer>();
        }
    }
    private void OnEnable()
    {
        _onMoveAgentToPoint.AddListener(DisplayClickEffect);
    }

    private void OnDisable()
    {
        _onMoveAgentToPoint.RemoveListener(DisplayClickEffect);
    }

    private void DisplayClickEffect(Vector3 point)
    {
        if (_clickEffect != null)
        {
            _clickEffect.transform.position = point + Vector3.up * 0.01f;
            _clickEffect.material.SetFloat(_clickTime, Time.time);
        }
    }
}
