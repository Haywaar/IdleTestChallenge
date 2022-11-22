using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Configs.GoldByTapConfig
{
    public class GoldByTapConfigRemote : IGoldByTapConfig, IDownloadable
    {
        private GoldByTapData _goldByTapData;
        private bool _initialized;
        public NumberData GetGoldByTapValue(int level)
        {
            if (!_initialized)
            {
                Debug.LogError("GetGoldByTap config data not downloaded");
                return NumberData.FromInt(0);
            }
            
            var value = _goldByTapData.koef1 * Mathf.Pow(level, _goldByTapData.koef2);
            return NumberData.FromInt(Mathf.FloorToInt(value));
        }

        public async UniTask LoadData(string fileName)
        {
            string url;
            url = "file://" + Application.dataPath + "/Resources/Configs/RemoteConfigs/" + fileName;
            UnityWebRequest request = UnityWebRequest.Get(url);
            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError )
            {
                Debug.LogError(request.error);
            }
            else
            {
                var text = request.downloadHandler.text;
                _goldByTapData = JsonUtility.FromJson<GoldByTapData>(text);
                _initialized = true;
            }
        }
    }
}