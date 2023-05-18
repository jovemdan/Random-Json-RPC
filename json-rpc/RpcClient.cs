using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Linq;

public class RpcClient
{
    private readonly HttpClient httpClient;
    private readonly string apiUrl;
    private int idCounter;

    public RpcClient(string apiUrl)
    {
        this.apiUrl = apiUrl;
        httpClient = new HttpClient();

        // Configuração do cabeçalho da requisição HTTP
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task GenereateUUID()
    {
        // Construção do objeto de requisição JSON-RPC
        var request = new
        {
            jsonrpc = "2.0",
            method = "generateUUIDs",
            @params = new
            {
                apiKey = "00000000-0000-0000-0000-000000000000",
                n = 1
            },
            id = 15998
        };

        // Envio da requisição para a API Random.org
        var response = await httpClient.PostAsJsonAsync(apiUrl, request);

        if (response.IsSuccessStatusCode)
        {
            // Leitura da resposta JSON-RPC como string
            var responseContent = await response.Content.ReadAsStringAsync();

            // Desserialização da resposta para um objeto dinâmico
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

            // Acessando o UUID gerado na resposta
            string uuid = responseObject.result.random.data[0];

            Console.WriteLine($"UUID: {uuid}");
        }
        else
        {
            Console.WriteLine($"Falha na requisição: {response.StatusCode}");
        }
    }

    public async Task GenereateString()
    {
        // Construção do objeto de requisição JSON-RPC
        var request = new
        {
            jsonrpc = "2.0",
            method = "generateStrings",
            @params = new
            {
                apiKey = "00000000-0000-0000-0000-000000000000",
                n = 8,
                length = 10,
                characters = "abcdefghijklmnopqrstuvwxyz",
                replacement = true
            },
            id = 42
        };

        // Envio da requisição para a API Random.org
        var response = await httpClient.PostAsJsonAsync(apiUrl, request);

        if (response.IsSuccessStatusCode)
        {
            // Leitura da resposta JSON-RPC como string
            var responseContent = await response.Content.ReadAsStringAsync();

            // Desserialização da resposta para um objeto dinâmico
            dynamic responseObject = JsonConvert.DeserializeObject(responseContent);

            // Acessando as strings gerado na resposta
            string randomString = responseObject.result.random.data[0];

            Console.WriteLine($"String: {randomString}");
        }
        else
        {
            Console.WriteLine($"Falha na requisição: {response.StatusCode}");
        }
    }
}