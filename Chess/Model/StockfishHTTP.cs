using System.Text.Json;

namespace Chess.Model;

public class StockfishHTTP
{
    private int _difficulty;
    public StockfishHTTP()
    {
        Console.WriteLine("STOCKFISH ONLINE");
        _difficulty = 10;
    }

    private string SendRequest(string fen)
    {
        string encodedFen = Uri.EscapeDataString(fen);
        string url = $"https://stockfish.online/api/s/v2.php?fen={encodedFen}&depth={_difficulty}&mode=bestmove";
        Console.WriteLine("Sending request..");
        HttpClient client = new HttpClient(); 
        
        using (client)
        {
            client.Timeout = TimeSpan.FromSeconds(10);
            HttpResponseMessage response = client.GetAsync(url).Result;
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

    public void ChangeDifficulty(int amount)
    {
        _difficulty += amount;
        if (_difficulty < 1)
        {
            _difficulty = 1;
        }
        if (_difficulty > 15)
        {
            _difficulty = 15;
        }
    }

        public string Difficulty
    {
        get
        {
            string difficulty;
            if (_difficulty < 10)
            {
                difficulty = "Easy";
            }
            else if (_difficulty < 5)
            {
                difficulty = "Very Easy";
            }
            else if (_difficulty > 10)
            {
                difficulty = "Hard";
            }
            else
            {
                difficulty = "Normal";
            }
            return difficulty;
        }
    }
}
