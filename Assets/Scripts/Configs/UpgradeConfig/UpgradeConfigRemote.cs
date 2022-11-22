using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Configs.UpgradeConfig
{
    public class UpgradeConfigRemote : IUpgradeConfig, IDownloadable
    {
        private UpgradeConfigData _configData;

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
                _configData = JsonUtility.FromJson<UpgradeConfigData>(text);
            }
        }

        public NumberData GetUpgradePrice(int level)
        {
            var value = Mathf.Pow((_configData.koef1 * _configData.koef2), level);
            return NumberData.FromInt(Mathf.FloorToInt(value));
        }
    }
}