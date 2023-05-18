//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using json_rpc.Bitcoin;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Program
{
    public static async Task Main(string[] args)
    {
        // URL da API JSON-RPC - Random
        var apiUrl = "https://api.random.org/json-rpc/2/invoke";

        var client = new RpcClient(apiUrl);

        try
        {
            // Obter UUID
            await client.GenereateUUID();

            // Obter String
            await client.GenereateString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
    }
}
