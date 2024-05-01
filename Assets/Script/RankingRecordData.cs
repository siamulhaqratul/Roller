using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Datum
{
    public int ranking { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public int revenue { get; set; }
}

public class RankingRoot
{
    public bool status { get; set; }
    public string message { get; set; }
    public List<Datum> data { get; set; }
}
