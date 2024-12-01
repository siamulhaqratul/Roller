using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class RepeatBet
{
    public int car_id { get; set; }
    public int bet_amount { get; set; }
}

public class RepeatData
{
    public int total_amount { get; set; }
    public List<RepeatBet> bets { get; set; }
}

public class RepeatRoot
{
    public string message { get; set; }
    public RepeatData data { get; set; }
}


