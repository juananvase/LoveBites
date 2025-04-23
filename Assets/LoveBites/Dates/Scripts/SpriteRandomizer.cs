using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpriteRandomizer : MonoBehaviour
{
    [SerializeField] private Image _foreground;
    [SerializeField] private Sprite[] _foregroundSprites;
    [SerializeField] private Image _background;
    [SerializeField] private Sprite[] _backgroundSprites;
    [SerializeField] private Color[] _backgroundColors;
    [SerializeField] private bool _autoCycle = false;
    [SerializeField] private float _cycleDuration = 1f;

    private void OnValidate()
    {
        if (_foreground == null) _foreground = GetComponent<Image>();
    }

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

        while (true) {
            yield return new WaitForSeconds(delay);
            Randomize();
        }
    }
    
    public void Randomize()
    {
        int index = Random.Range(0, _foregroundSprites.Length - 1);

        _foreground.sprite = _foregroundSprites[index];

        if (_background != null)
        {
            _background.sprite = _backgroundSprites[index];

            if (_backgroundColors.Length > 0) _background.color = _backgroundColors[Random.Range(0, _backgroundColors.Length - 1)];
        }
    }
}
