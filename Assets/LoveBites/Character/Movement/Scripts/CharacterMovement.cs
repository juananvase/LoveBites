using UnityEngine;
using UnityEngine.AI;

public class CharacterMovement : MonoBehaviour
{
    private NavMeshAgent _agent;

    [Header("VFX")]
    [SerializeField] private GameObject _clickEffectPrefab;

    private Renderer _clickEffect;
    private readonly int _clickTime = Shader.PropertyToID("_ClickTime");


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        if (_clickEffectPrefab != null)
        {
            GameObject clickEffect = Instantiate(_clickEffectPrefab, new Vector3(0, 0, 0), Quaternion.Euler(90, 0, 0));
            _clickEffect = clickEffect.GetComponent<Renderer>();
        }
    }

    public void MoveToPosition(Vector3 point)
    {
        _agent.destination = point;
        SpawnClickEffect(point);
    }

    private void SpawnClickEffect(Vector3 point)
    {
        if (_clickEffect != null)
        {
            _clickEffect.transform.position = point + Vector3.up * 0.01f;
            _clickEffect.material.SetFloat(_clickTime, Time.time);
        }
    }
}
