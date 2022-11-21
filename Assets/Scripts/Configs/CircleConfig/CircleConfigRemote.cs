using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Configs
{
    public class CircleConfigRemote : ICircleConfig, IDownloadable
    {
        private CircleConfigData _configData;

        public NumberData.NumberData GetBuyPrice(int circlesCount)
        {
            var value = _configData.firstCirclePrice * Math.Pow(_configData.nextBuyCircleCoef, circlesCount);
            return NumberData.NumberData.FromInt(Mathf.FloorToInt((float)value));
        }

        public int GetMaxCirclesCount()
        {
            return _configData.maxCirclesCount;
        }

        public float GetCircleAttackCooldown()
        {
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
            }
        }
    }
}