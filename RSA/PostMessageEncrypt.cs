using RestSharp;

namespace Post
{
    public static class PostMessageEncrypt
    {

        public static string PostRSA(string message)
        {
            var client = new RestClient("https://localhost:7091");

            var request = new RestRequest("DataEncrypt/RSA", Method.Post);
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
