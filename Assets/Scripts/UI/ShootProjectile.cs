using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
   public class ShootProjectile : MonoBehaviour
   {
      [SerializeField] private Image _image;
      [SerializeField] private TrailRenderer _trailRenderer;

      private RectTransform _rect;

      public RectTransform Rect => _rect;

      private void Awake()
      {
         _rect = GetComponent<RectTransform>();
      }

      public void SetColor(Color color)
      {
         _image.color = color;
         _trailRenderer.startColor = color;
         _trailRenderer.endColor = color;
      }
   }
}
