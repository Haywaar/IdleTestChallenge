using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.Signals;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _shakeTime = 0.1f;

    [Header("Change from 0 if you want diggers attack not in center of enemy")] [SerializeField]
    private float _randomOffsetRadius;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Awake()
    {
        _signalBus.Subscribe<AttackSignal>(VisualizeAttack);
    }

    private void VisualizeAttack(AttackSignal signal)
    {
        _image.transform.localPosition = Vector3.zero;
        _image.transform.DOShakePosition(_shakeTime, 50f, 10, 11);
    }

    public Vector3 Position()
    {
        var randomX = Random.Range(-1 * _randomOffsetRadius, _randomOffsetRadius);
        var randomY = Random.Range(-1 * _randomOffsetRadius, _randomOffsetRadius);
        return _image.transform.position + new Vector3(randomX, randomY);
    }
}