using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class RankData
{
    public List<History> history { get; set; }
}

public class History
{
    public int id { get; set; }
    public int earnings { get; set; }
    public RankUser user { get; set; }
}

public class RankingRoot
{
    public string message { get; set; }
    public RankData data { get; set; }
}

public class RankUser
{
    public string name { get; set; }
    public string avatar { get; set; }
}

