using UnityEngine;
using UnityEngine.UI;

public class SpriteColorRandomizer : MonoBehaviour
{
    [SerializeField] private Image _primaryImage;
    [SerializeField] private Image _secondaryImage;
    [SerializeField] private float _secondaryValueMultiplier;
    [SerializeField] private Color[] colors;

    private void Awake()
    {
        if (colors.Length == 0) return;

        Color color = colors[Random.Range(0, colors.Length - 1)];

        _primaryImage.color = color;

        color.r *= _secondaryValueMultiplier;
        color.g *= _secondaryValueMultiplier;
        color.b *= _secondaryValueMultiplier;

        _secondaryImage.color = color;
    }
}
