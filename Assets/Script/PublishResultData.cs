using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class ResultData
{
    public Result result { get; set; }
}

public class Result
{
    public int bet_amount { get; set; }
    public int reward_amount { get; set; }
    public int round_number { get; set; }
}

public class Root
{
    public string message { get; set; }
    public ResultData data { get; set; }
}

