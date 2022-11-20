using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.Signals;

namespace Digger
{
    public class CircleDiggerView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private List<Color> _levelColors;
        private SignalBus _signalBus;

        private int _diggerId;

        public int DiggerId => _diggerId;

        private int _level;

        public int Level => _level;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize(int diggerId, int level)
        {
            _signalBus.Subscribe<AttackSignal>(VisualizeAttack);
            _signalBus.Subscribe<UpgradeDiggerSignal>(VisualizeUpgrade);
            _diggerId = diggerId;
            _level = level;
        }

        private void VisualizeUpgrade(UpgradeDiggerSignal signal)
        {
            if (signal.Id == _diggerId)
            {
                _level = signal.Level;
                SetSpriteColor(_level);
            }
        }

        private void VisualizeAttack(AttackSignal signal)
        {
            if (signal.DiggerId == _diggerId)
            {
                //TODO - anim
            }
        }

        private void SetSpriteColor(int level)
        {
            _image.color = _levelColors[level - 1];
        }
    }
}