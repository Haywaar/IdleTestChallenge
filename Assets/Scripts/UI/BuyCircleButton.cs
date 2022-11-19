using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class BuyCircleButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _priceText;

        [SerializeField] private Color _activeColor = Color.white;
        [SerializeField] private Color _inactiveColor = Color.gray;

        public void Init(UnityAction callback, NumberData numberData)
        {
            _button.onClick.AddListener(callback);
            SetPrice(numberData);
        }

        public void SetPrice(NumberData numberData)
        {
            _priceText.text = numberData.ToString();
        }

        public void SetMax()
        {
            _priceText.text = "Max!";
        }

        public void SetInteractable(bool isActive)
        {
            _button.enabled = isActive;
            _button.image.color = isActive ? _activeColor : _inactiveColor;
        }
    }
}