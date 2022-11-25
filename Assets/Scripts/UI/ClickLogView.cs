using Configs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Zenject.Signals;

namespace UI
{
    public class ClickLogView : MonoBehaviour
    {
        [SerializeField] private ClickLogString _logStringPrefab;
        [SerializeField] private RectTransform _startLogPos;
        [SerializeField] private RectTransform _endLogPos;
        [SerializeField] private float _flyTime;
        [SerializeField] private float _randomXOffset = 30;
    
        [SerializeField] private RectTransform _clickStringRoot;
        private SignalBus _signalBus;
        private LevelColorConfig _levelColorConfig;

        private ObjectPool<ClickLogString> _pool;

        [Inject]
        private void Construct(SignalBus signalBus, LevelColorConfig levelColorConfig)
        {
            _signalBus = signalBus;
            _levelColorConfig = levelColorConfig;
        }

        private void Awake()
        {
            _signalBus.Subscribe<AddMoneySignal>(OnMoneyAdded);
            _pool = new ObjectPool<ClickLogString>(CreatedLogString, OnTakeFromPool, OnReturnedToPool,
                OnDestroyedPoolObject);
        }

        private void OnMoneyAdded(AddMoneySignal signal)
        {
            var clickLogString = _pool.Get();
            clickLogString.Init(signal.Value.ToString(), _levelColorConfig.GetColorForLevel(signal.Level));

            var randomX = UnityEngine.Random.Range(-1 * _randomXOffset, _randomXOffset);
            var randomOffset = new Vector3(randomX, 0, 0);
            clickLogString.Rect.localPosition = _startLogPos.localPosition + randomOffset;
        
            var tween = clickLogString.Rect.DOLocalMoveY(_endLogPos.localPosition.y, _flyTime);
            clickLogString.Text.DOFade(0, _flyTime);
            tween.onComplete += () =>
            {
                _pool.Release(clickLogString);
            };
        }

        private ClickLogString CreatedLogString()
        {
            var go = Instantiate(_logStringPrefab);
            go.transform.SetParent(_clickStringRoot);
            go.transform.localScale = Vector3.one;
            return go;
        }
    
        private void OnTakeFromPool(ClickLogString logString)
        {
            logString.gameObject.SetActive(true);
        }
    
        private void OnReturnedToPool(ClickLogString logString)
        {
            logString.gameObject.SetActive(false);
        }
    
        private void OnDestroyedPoolObject(ClickLogString logString)
        {
            Destroy(logString);
        }
    }
}