using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class QueryStringManager : MonoBehaviour
{
    // Fruit
    public string bearerToken;
    void Start()
    {
        // Get the full URL used to open the app
        string absoluteURL = Application.absoluteURL;

        // Check if the URL contains a query string
        if (!string.IsNullOrEmpty(absoluteURL))
        {
            // Parse the query string using the System.Uri class
            System.Uri uri = new System.Uri(absoluteURL);

            // Access the query parameters as key-value pairs
            string queryString = uri.Query;

            // Here you can use the queryString as needed in your game logic
            Debug.Log("Query String: " + queryString);
            bearerToken = GetQueryParameter(queryString, "token");
            if (string.IsNullOrEmpty(bearerToken))
            {
                bearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMzcyYjc3NWQxOWE0NTNjNzYyZGFkMjA1YTI4MGNhNDExN2Q4MDAxZDY4ZGU3MTM3NjhmYWQ1MDE4NzgxMDBiODRlYmY0MWY0YTFlNzhjOWYiLCJpYXQiOjE3MTE5NjEwNDguMjczOTE1LCJuYmYiOjE3MTE5NjEwNDguMjczOTE4LCJleHAiOjE3NDM0OTcwNDguMjY5Njc0LCJzdWIiOiI4Iiwic2NvcGVzIjpbXX0.M5uyrZTpDff5Jf_fFgGzsBlU1xVqbflE_60oRj8SV34LMtoTywcJ16ADgCQZiAkCY6YATKOq-Up92vHm7ft8ADFTzOcUlPdrV4W9L_cO2-bb9SRPKcaB7sVF-0UoqJ-oscy-cVuVTFjwOcETFJ4hmHFJM11rSiJgyWAvv0S_QZQE8kshcn0GtUSdlv9QV2jPeLx0fY9NAlnRC-rObCjAc2Ge_a-gXNhoxAn9pw6cHGkAT6hye-SQcdSqYWULlsfCf2Dzc3e8lVd25AqGBneRopIp6kX8PXkiEacPWBl8Ckx0yfb8lpax_wmUCV_Tzc5fZSjOzEa5f2_yiOeZA6C9oE5KqmLOFyaNLHpMebBuQyBNSMb9JxtU6zW763P9WckFcDT1QaIYzQRyC9-oEdMy6-waQJtJCxqNUNqz9i_oP8u3mOU2U0a6yCIIldHTMpy4H_6h2ERoc-oolfaT3PGMxywL77Hvqz6KuDr5wofcpW0Impr72UPxXnFSV8olbcEPVPn-PUVjVfFD8U9mcm0AWiAiZjfsZ8ODVPkMfx3pqdE2gD_ezs4Flzukz_j2gn-Teod1FQ_vk5MrhahY8XDzwlwyH8Hi9E_Jk0FzrAheDnroLtCG9ciM0OVQneYEURiL5im3hR7KGjMJqNpXckI3NNo13VXv5KSJzgVRe69DcME";
                Debug.Log("Got the url but not the token");
            }
        }
        else 
        {
            bearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMzcyYjc3NWQxOWE0NTNjNzYyZGFkMjA1YTI4MGNhNDExN2Q4MDAxZDY4ZGU3MTM3NjhmYWQ1MDE4NzgxMDBiODRlYmY0MWY0YTFlNzhjOWYiLCJpYXQiOjE3MTE5NjEwNDguMjczOTE1LCJuYmYiOjE3MTE5NjEwNDguMjczOTE4LCJleHAiOjE3NDM0OTcwNDguMjY5Njc0LCJzdWIiOiI4Iiwic2NvcGVzIjpbXX0.M5uyrZTpDff5Jf_fFgGzsBlU1xVqbflE_60oRj8SV34LMtoTywcJ16ADgCQZiAkCY6YATKOq-Up92vHm7ft8ADFTzOcUlPdrV4W9L_cO2-bb9SRPKcaB7sVF-0UoqJ-oscy-cVuVTFjwOcETFJ4hmHFJM11rSiJgyWAvv0S_QZQE8kshcn0GtUSdlv9QV2jPeLx0fY9NAlnRC-rObCjAc2Ge_a-gXNhoxAn9pw6cHGkAT6hye-SQcdSqYWULlsfCf2Dzc3e8lVd25AqGBneRopIp6kX8PXkiEacPWBl8Ckx0yfb8lpax_wmUCV_Tzc5fZSjOzEa5f2_yiOeZA6C9oE5KqmLOFyaNLHpMebBuQyBNSMb9JxtU6zW763P9WckFcDT1QaIYzQRyC9-oEdMy6-waQJtJCxqNUNqz9i_oP8u3mOU2U0a6yCIIldHTMpy4H_6h2ERoc-oolfaT3PGMxywL77Hvqz6KuDr5wofcpW0Impr72UPxXnFSV8olbcEPVPn-PUVjVfFD8U9mcm0AWiAiZjfsZ8ODVPkMfx3pqdE2gD_ezs4Flzukz_j2gn-Teod1FQ_vk5MrhahY8XDzwlwyH8Hi9E_Jk0FzrAheDnroLtCG9ciM0OVQneYEURiL5im3hR7KGjMJqNpXckI3NNo13VXv5KSJzgVRe69DcME";
            Debug.Log("bearer roken entered");


        }

        
    }

    public string GetQueryParameter(string queryString, string parameterName)
    {
        string[] parameters = queryString.TrimStart('?').Split('&');
        foreach (string parameter in parameters)
        {
            string[] parts = parameter.Split('=');
            if (parts.Length == 2 && parts[0] == parameterName)
            {
                return parts[1];
            }
        }
        return null;
    }
}
