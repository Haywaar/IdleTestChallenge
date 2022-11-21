using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Zenject.Signals;

namespace Digger
{
    public class PlayerController : MonoBehaviour 
    {
        private RaycastHit hitObject;
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Update()
        {
            //TODO - add if (game.inProgress)
            var touchPos = Input.mousePosition;

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                var pointerEventData = new PointerEventData(EventSystem.current) { position = touchPos };
                var raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerEventData, raycastResults);

                if(raycastResults.Count > 0)
                {
                    foreach(var result in raycastResults)
                    {
                        if (result.gameObject.tag.Equals("Enemy"))
                        {
                            _signalBus.Fire(new PlayerClickedSignal());
                           break;
                        }
                        
                        if (result.gameObject.tag.Equals("Circle"))
                        {
                            // circle selected
                            var circleDiggerView = result.gameObject.GetComponent<CircleDiggerView>();
                            if (circleDiggerView != null)
                            {
                                _signalBus.Fire(new CircleClickedSignal(circleDiggerView.DiggerId, circleDiggerView.Level));
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}
