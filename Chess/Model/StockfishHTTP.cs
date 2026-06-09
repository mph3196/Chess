using System.Text.Json;

namespace Chess.Model;

public class StockfishHTTP
{
    private HttpClient _client;
    public StockfishHTTP()
    {
        _client = new HttpClient();
        Console.WriteLine("STOCKFISH ONLINE");
    }

    public string SendRequest(string fen)
    {
        try
        {
            string encodedFen = Uri.EscapeDataString(fen);
            string url = $"https://stockfish.online/api/s/v2.php?fen={encodedFen}&depth=10&mode=bestmove";
            Console.WriteLine("Sync request starting...");
            
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                string data = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Sync request success");
                return data;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Sync exception: {ex.GetType().Name} - {ex.Message}");
            return null;
        }
    }

    public string GetMoveFromResponse(string fen)
    {
        string data = SendRequest(fen);
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement root = json.RootElement;
        string bestMove = root.GetProperty("bestmove").GetString();
        return bestMove.Split(' ')[1];
    }
}
