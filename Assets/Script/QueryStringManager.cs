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
                bearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiY2M3ZTI1ZTBhODA3ZTFiOWVjZTkwM2I2NjE4YmRmNDAxMDI0NjMxYmYxM2UzNjBlYTEyNzMyYjA5NWUwYjg5ZDE5NzNhODIxOTg4OTA3ZTciLCJpYXQiOjE3MTQ0NjEwMzUuNDc4ODA5LCJuYmYiOjE3MTQ0NjEwMzUuNDc4ODE0LCJleHAiOjE3NDU5OTcwMzUuNDc0Mjg5LCJzdWIiOiIxOSIsInNjb3BlcyI6W119.GYlPPslynwK5B2TXbnivm2R--nh8VARZe2RLjFevf4T9jDWqfB8_AAyZ7qnPx_Edk83mMf862QCUouJpZmkin0qu_CR1Sx6IXiOo4x48InUagacn6ZKaD-tpOQAPuF6LlbHbrWYdsCp-i5vc3CM7IiccNSKrJfOB6acigY3WmNrlaVjJg1SKtWKEqBqE78_mlDKUE4LPplgo-qQnyNLj4BPyTs9i9ndQQfv8jS-sUP-xfPFGlOi6KcWe5Etwg34kDACRQsFXxheddQnqbcraxU-XCHumANkkRc4LTeDKpkqW8MvTEwE4t2bVm5HAqpMi29_d6Dx2OlH5FK-SE5eyoAT8HXYij8qNrouZOuM-kQh7CaBP6fS0mykzygpS0ktnB_TPrFtIzzduucJ0pUgVMnkEG6PkcDyb29WmZGIDYzZ9snpZ7l-h5GwwU14hj8enX1AomNXlsxIA-4zFgo2yUfGyJTCtHXw0Pu7a_o40pLzOMyt7NIabu-MLV8TgKVrCRXKEbNyPGeGqgnd93O50E4XOLyG7hll0va_DbaDWwD6VqK6Hd2D91LhKk1K9oBkjqVkjpHcdYgSAfrlnEO2HKCjGYPCFW4E24Os8Nzr5MonRp52aJ2a1hsNmhp58jk6UPhr7gK3172HTKBtK2Asygu5ai5T5E-vPCqqzdciR2ko";
                Debug.Log("Got the url but not the token");
            }
        }
        else 
        {
            bearerToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiY2M3ZTI1ZTBhODA3ZTFiOWVjZTkwM2I2NjE4YmRmNDAxMDI0NjMxYmYxM2UzNjBlYTEyNzMyYjA5NWUwYjg5ZDE5NzNhODIxOTg4OTA3ZTciLCJpYXQiOjE3MTQ0NjEwMzUuNDc4ODA5LCJuYmYiOjE3MTQ0NjEwMzUuNDc4ODE0LCJleHAiOjE3NDU5OTcwMzUuNDc0Mjg5LCJzdWIiOiIxOSIsInNjb3BlcyI6W119.GYlPPslynwK5B2TXbnivm2R--nh8VARZe2RLjFevf4T9jDWqfB8_AAyZ7qnPx_Edk83mMf862QCUouJpZmkin0qu_CR1Sx6IXiOo4x48InUagacn6ZKaD-tpOQAPuF6LlbHbrWYdsCp-i5vc3CM7IiccNSKrJfOB6acigY3WmNrlaVjJg1SKtWKEqBqE78_mlDKUE4LPplgo-qQnyNLj4BPyTs9i9ndQQfv8jS-sUP-xfPFGlOi6KcWe5Etwg34kDACRQsFXxheddQnqbcraxU-XCHumANkkRc4LTeDKpkqW8MvTEwE4t2bVm5HAqpMi29_d6Dx2OlH5FK-SE5eyoAT8HXYij8qNrouZOuM-kQh7CaBP6fS0mykzygpS0ktnB_TPrFtIzzduucJ0pUgVMnkEG6PkcDyb29WmZGIDYzZ9snpZ7l-h5GwwU14hj8enX1AomNXlsxIA-4zFgo2yUfGyJTCtHXw0Pu7a_o40pLzOMyt7NIabu-MLV8TgKVrCRXKEbNyPGeGqgnd93O50E4XOLyG7hll0va_DbaDWwD6VqK6Hd2D91LhKk1K9oBkjqVkjpHcdYgSAfrlnEO2HKCjGYPCFW4E24Os8Nzr5MonRp52aJ2a1hsNmhp58jk6UPhr7gK3172HTKBtK2Asygu5ai5T5E-vPCqqzdciR2ko";
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
