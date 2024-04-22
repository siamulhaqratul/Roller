using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Bet
{
    public int id { get; set; }
    public int amount { get; set; }
    public int is_win { get; set; }
    public int seat_no { get; set; }
    public string time { get; set; }
}

public class Data2
{
    public List<History2> history { get; set; }
}

public class History2
{
    public int id { get; set; }
    public int reward { get; set; }
    public int winner_seat { get; set; }
    public string created_at { get; set; }
    public List<Bet> bets { get; set; }
}

public class RootHistory
{
    public Data2 data { get; set; }
}

