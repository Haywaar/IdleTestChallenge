using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Digger
{
    public class PlayerController : MonoBehaviour 
    {
        private RaycastHit hitObject;
        private DiggerManager _diggerManager;

        [Inject]
        private void Construct(DiggerManager diggerManager)
        {
            _diggerManager = diggerManager;
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
                           _diggerManager.OnPlayerClicked();
                        }
                    }
                }
            }
        }
    }
}
