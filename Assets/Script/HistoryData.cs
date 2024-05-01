using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Bet2
{
    public int id { get; set; }
    public int bet_amount { get; set; }
    public int reward_amount { get; set; }
    public bool is_win { get; set; }
    public int car_id { get; set; }
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
    public int winner_card_id { get; set; }
    public string created_at { get; set; }
    public List<Bet2> bets { get; set; }
}

public class RootHistory
{
    public string message { get; set; }
    public Data2 data { get; set; }
}


