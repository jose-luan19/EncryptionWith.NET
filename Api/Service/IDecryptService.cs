namespace Api.Service
{
    public interface IDecryptService
    {
        public string DecryptAES(string message);
        public string DecryptRSA(string message);
    }
}
