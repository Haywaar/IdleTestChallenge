using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.Signals;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _shakeTime = 0.1f;
    
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
}