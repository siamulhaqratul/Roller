using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class RepeData
{
    public int seat_1 { get; set; }
    public int seat_2 { get; set; }
    public int seat_3 { get; set; }
    public int total_amount { get; set; }
}

public class RepeatBidMain
{
    public string message { get; set; }
    public RepeData data { get; set; }
}

