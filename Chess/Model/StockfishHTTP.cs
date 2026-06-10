using System.Text.Json;

namespace Chess.Model;

public class StockfishHTTP
{
    private HttpClient _client;
    private int _difficulty;
    public StockfishHTTP()
    {
        _client = new HttpClient();
        Console.WriteLine("STOCKFISH ONLINE");
        _difficulty = 10;
    }

    private string SendRequest(string fen)
    {
        string encodedFen = Uri.EscapeDataString(fen);
        string url = $"https://stockfish.online/api/s/v2.php?fen={encodedFen}&depth={_difficulty}&mode=bestmove";
        Console.WriteLine("Sending request..");
        
        using (_client)
        {
            _client.Timeout = TimeSpan.FromSeconds(10);
            HttpResponseMessage response = _client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            string data = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Request successful");
            Console.WriteLine(data);
            return data;
        }
    }

    public string GetMoveFromResponse(string fen)
    {
        string data = SendRequest(fen);
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement root = json.RootElement;
        string bestMove = root.GetProperty("bestmove").GetString();
        bestMove = bestMove.Split(' ')[1];
        Console.WriteLine($"Best move from JSON: {bestMove}");
        return bestMove;
    }
}
