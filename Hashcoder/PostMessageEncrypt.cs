using RestSharp;

namespace Hashcoder
{
    public static class PostMessageEncrypt
    {

        public static string PostAES(string message)
        {
            var client = new RestClient("https://localhost:7091");

            var request = new RestRequest("DataEncrypt/AES", Method.Post);
            request.RequestFormat = DataFormat.Json;
            request.AddBody("\"" + message + "\"");

            var response = client.Execute(request).Content;

            var charsToRemove = new string[] { "\"" };
            foreach (var c in charsToRemove)
            {
                response = response.Replace(c, string.Empty);
            }

            return response;
        }

    }
}
