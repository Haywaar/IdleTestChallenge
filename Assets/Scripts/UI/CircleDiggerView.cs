using Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.Signals;

namespace UI
{
    public class CircleDiggerView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private Text _levelText;
        
        [SerializeField] private ShootProjectile _shootProjectile;
        [SerializeField] private float _projFlyTime;
        private SignalBus _signalBus;
        private LevelColorConfig _colorConfig;
        private EnemyView _enemyView;

        private int _diggerId;

        public int DiggerId => _diggerId;

        private int _level;

        public int Level => _level;

        [Inject]
        private void Construct(SignalBus signalBus, LevelColorConfig colorConfig, EnemyView enemyView)
        {
            _signalBus = signalBus;
            _colorConfig = colorConfig;
            _enemyView = enemyView;
        }

        public void Initialize(int diggerId, int level)
        {
            _signalBus.Subscribe<AttackSignal>(VisualizeAttack);
            _signalBus.Subscribe<UpgradeDiggerSignal>(VisualizeUpgrade);
            _diggerId = diggerId;
            _level = level;
            
            _levelText.text = _level.ToString();
            _button.onClick.AddListener(() =>
            {
                _signalBus.Fire(new CircleClickedSignal(DiggerId, Level));
            });
        }

        private void VisualizeUpgrade(UpgradeDiggerSignal signal)
        {
            if (signal.Id == _diggerId)
            {
                _level = signal.Level;
                SetSpriteColor(_level);
                _levelText.text = _level.ToString();
            }
        }

        private void VisualizeAttack(AttackSignal signal)
        {
            if (signal.DiggerId == _diggerId)
            {
                // Circle shake logic
                transform.DOPunchScale(Vector3.one * 1.3f, 0.3f, 20);
                
                // Shoot logic
                _shootProjectile.transform.localPosition = Vector3.zero;
                _shootProjectile.SetColor(_colorConfig.GetColorForLevel(signal.Level));
                _shootProjectile.gameObject.SetActive(true);
                var tween = _shootProjectile.Rect.DOMove(_enemyView.Position(), _projFlyTime);
                tween.onComplete += () =>
                {
                    _shootProjectile.gameObject.SetActive(false);
                };
            }
        }

        private void SetSpriteColor(int level)
        {
            _image.color = _colorConfig.GetColorForLevel(level);
        }
    }
}