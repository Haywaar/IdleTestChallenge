using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ClickLogString : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private RectTransform _rect;

    public RectTransform Rect => _rect;

    public void Init(string value, Color textColor)
    {
        _text.text = "+" + value;
        _text.color = textColor;
    }
}
