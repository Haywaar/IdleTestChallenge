using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ClickLogString : MonoBehaviour
    {
        [SerializeField] private Text _text;

        [SerializeField] private RectTransform _rect;

        public RectTransform Rect => _rect;
        public Text Text => _text;

        public void Init(string value, Color textColor)
        {
            _text.text = "+" + value;
            _text.color = textColor;
        }
    }
}
