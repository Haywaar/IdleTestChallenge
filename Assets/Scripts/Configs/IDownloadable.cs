using Cysharp.Threading.Tasks;

namespace Configs
{
    public interface IDownloadable
    {
        public UniTask LoadData(string fileName);
    }
}