using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Data
{
    public User user { get; set; }
}

public class ProfileRoot
{
    public string message { get; set; }
    public Data data { get; set; }
}

public class User
{
    public string name { get; set; }
    public int balance { get; set; }
    public string thumbnail { get; set; }
}


