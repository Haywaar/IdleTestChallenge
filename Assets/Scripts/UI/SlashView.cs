using System;
using Configs;
using DG.Tweening;
using Digger;
using UnityEngine;
using Zenject;
using Zenject.Signals;
using Random = UnityEngine.Random;

namespace UI
{
    /// <summary>
    /// Visualisation of slashes, made by user click on cube
    /// </summary>
    public class SlashView : MonoBehaviour
    {
        [SerializeField] private RectTransform _slash;
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private float xLeft;
        [SerializeField] private float minY;
        [SerializeField] private float xRight;
        [SerializeField] private float maxY;
        [SerializeField] private float slashTime = 0.1f;

        private SignalBus _signalBus;
        private Tween _slashTween;

        private bool _slashToLeft;
        private LevelColorConfig _colorConfig;

        [Inject]
        private void Construct(SignalBus signalBus, LevelColorConfig colorConfig)
        {
            _signalBus = signalBus;
            _colorConfig = colorConfig;
        }

        private void Awake()
        {
            _signalBus.Subscribe<AttackSignal>(OnAttack);
            _signalBus.Subscribe<UpgradeDiggerSignal>(OnUpgradeDigger);
        }

        private void OnAttack(AttackSignal signal)
        {
            if (signal.DiggerId == DiggerManager.PlayerDiggerId)
            {
                ShowSlashAnim();
            }
        }

        private void ShowSlashAnim()
        {
            var yLeft = Random.Range(minY, maxY);
            var yRight = Random.Range(minY, maxY);

            Vector3 leftPoint = new Vector3(xLeft, yLeft, 0);
            Vector3 rightPoint = new Vector3(xRight, yRight, 0);

            _slash.gameObject.SetActive(true);
            _trailRenderer.enabled = false;

            if (_slashToLeft)
            {
                _slash.localPosition = leftPoint;
                _slashTween = _slash.DOLocalMove(rightPoint, slashTime);
            }
            else
            {
                _slash.localPosition = rightPoint;
                _slashTween = _slash.DOLocalMove(leftPoint, slashTime);
            }

            _trailRenderer.enabled = true;
            _slashToLeft = !_slashToLeft;


            _slashTween.onComplete += () => { _slash.gameObject.SetActive(false); };
        }

        private void OnUpgradeDigger(UpgradeDiggerSignal obj)
        {
            if (obj.Id == DiggerManager.PlayerDiggerId)
            {
                var color = _colorConfig.GetColorForLevel(obj.Level);
                _trailRenderer.startColor = color;
                _trailRenderer.endColor = color;
            }
        }
    }
}