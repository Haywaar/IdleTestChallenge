using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;
using Zenject.Signals;

namespace Configs
{
    public class UpgradeConfigRemoteFile : MonoBehaviour, IUpgradeConfig
    {
        private float _koef1;
        private float _koef2;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            GatherData();
            //StartCoroutine(GetData());
        }

        private IEnumerator GetData()
        {
            string url = string.Empty;
            url = "file://" + Application.dataPath + "/Resources/Configs/RemoteConfigs/" + "UpgradeConfig.json";
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
            {
                Debug.LogError(request.error);
            }
            else
            {
                var text = request.downloadHandler.text;
                UpgradeFormula formula = JsonUtility.FromJson<UpgradeFormula>(text);
                Debug.LogFormat("koef1 = {0} koef2 = {1}", formula.koef1, formula.koef2);
                _koef1 = formula.koef1;
                _koef2 = formula.koef2;
                _signalBus.Fire(new GameStartedSignal());
            }
        }
        
        private async void GatherData()
        {
            string url = string.Empty;
            url = "file://" + Application.dataPath + "/Resources/Configs/RemoteConfigs/" + "UpgradeConfig.json";
            UnityWebRequest request = UnityWebRequest.Get(url);
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
            {
                Debug.LogError(request.error);
            }
            else
            {
                var text = request.downloadHandler.text;
                UpgradeFormula formula = JsonUtility.FromJson<UpgradeFormula>(text);
                Debug.LogFormat("koef1 = {0} koef2 = {1}", formula.koef1, formula.koef2);
                _koef1 = formula.koef1;
                _koef2 = formula.koef2;
                _signalBus.Fire(new GameStartedSignal());
            }
        }

        public NumberData GetUpgradePrice(int level)
        {
            var value = Mathf.Pow((_koef1 * _koef2), level);
            return NumberData.FromInt(Mathf.FloorToInt(value));
        }
    }
}
