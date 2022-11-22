using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Configs.CircleConfig
{
    public class CircleConfigRemote : ICircleConfig, IDownloadable
    {
        private CircleConfigData _configData;
        private bool _initialized;

        public NumberData GetBuyPrice(int circlesCount)
        {
            if (!_initialized)
            {
                Debug.LogError("Circle config data not downloaded");
                return NumberData.FromInt(0);
            }

            var value = _configData.firstCirclePrice * Math.Pow(_configData.nextBuyCircleKoef, circlesCount);
            return NumberData.FromInt(Mathf.FloorToInt((float)value));
        }

        public int GetMaxCirclesCount()
        {
            if (!_initialized)
            {
                Debug.LogError("Circle config data not downloaded");
                return 0;
            }
            return _configData.maxCirclesCount;
        }

        public float GetCircleAttackCooldown()
        {
            if (!_initialized)
            {
                Debug.LogError("Circle config data not downloaded");
                return 0;
            }
            
            return _configData.circleAttackCooldown;
        }

        public async UniTask LoadData(string fileName)
        {
            string url = string.Empty;
            url = "file://" + Application.dataPath + "/Resources/Configs/RemoteConfigs/" + fileName;
            UnityWebRequest request = UnityWebRequest.Get(url);
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                var text = request.downloadHandler.text;
                var data = JsonUtility.FromJson<CircleConfigData>(text);
                _configData = data;
                _initialized = true;
            }
        }
    }
}