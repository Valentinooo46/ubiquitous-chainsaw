using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobizon
{
    public class MobizonClient
    {
        private readonly string _apiKey;

        public MobizonClient(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async void SendMessage(string phoneNumber, string message)
        {
            // Логіка для відправки повідомлення через Mobizon API
            // Наприклад, використовуючи HttpClient для відправки POST-запиту
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "apiKey", _apiKey },
                    { "recipient", phoneNumber },
                    { "text", message }
                };

                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync("https://api.mobizon.com/service/message/send", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Не вдалося відправити повідомлення через Mobizon");
                }
            }
        }
    }
}
