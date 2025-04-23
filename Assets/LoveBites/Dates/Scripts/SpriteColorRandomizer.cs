using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteColorRandomizer : MonoBehaviour
{
    [SerializeField] private Image _primaryImage;
    [SerializeField] private Image _secondaryImage;
    [SerializeField] private float _secondaryValueMultiplier;
    [SerializeField] private Color[] colors;
    [SerializeField] private bool _autoCycle = false;
    [SerializeField] private float _cycleDuration = 1f;

    private void Awake()
    {
        Randomize();

        if (_autoCycle) StartCoroutine(AutoCycle(_cycleDuration));
    }

    private IEnumerator AutoCycle(float delay)
    {
        if (delay < 0.1f)
        {
            Debug.Log("Delay too short!");
        }

        while (true)
        {
            yield return new WaitForSeconds(delay);
            Randomize();
        }
    }

    public void Randomize()
    {
        if (colors.Length == 0) return;

        Color color = colors[Random.Range(0, colors.Length - 1)];

        _primaryImage.color = color;

        if (_secondaryImage == null) return;

        color.r *= _secondaryValueMultiplier;
        color.g *= _secondaryValueMultiplier;
        color.b *= _secondaryValueMultiplier;

        _secondaryImage.color = color;
    }
}
