using Newtonsoft.Json;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ApiManager : MonoBehaviour
{
    public static ApiManager Instance;
    public TextMeshProUGUI coin_text;
    public int coins = 0;
    public int rankNumber;
    public Transform recordHolder;
    public Transform rankingHolder;
    public Image rankingRecord;
    public Image records;
    public Image profilePic;
    private string url = "https://yaahabibi.com/api/games/spin/bet";
    private string resultUrl = "https://yaahabibi.com/api/games/spin/publish-result";
    private string repeatUrl = "https://yaahabibi.com/api/games/spin/repeat";
    //public TextMeshProUGUI[] pot_text;


    public TextMeshProUGUI betThisRound;
    public TextMeshProUGUI roundNumber;
    public TextMeshProUGUI total_win;

    public List<int> myBets = new List<int>();
    public List<int> myBetAmounts = new List<int>();
    public bool isHistoryActive = false;
    public bool isRankingActive = false;
    private int r1, r2, r3;
    void Start()
    {
        StartCoroutine(GetPlayerProfileData());

        if (PlayerPrefs.GetInt("Toogle value") == 1)
        {
            Debug.Log("Repeat Bet Enabled");
            GameObject.Find("Toggle").GetComponent<Toggle>().isOn = true;
            PublicRepeatBet();
        }
        else
        {
            Debug.Log("Repeat Bet Disabled");
            GameObject.Find("Toggle").GetComponent<Toggle>().isOn = false;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        /*if(GameObject.Find("History").activeSelf)
            isHistoryActive = false ;
*/
    }

    public void HistoryRequest(int quantity)
    {
        if (!isHistoryActive)
        {
            isHistoryActive = true;
            StartCoroutine(GetHistoryData(quantity));
        }

    }
    public IEnumerator GetHistoryData(int quantity)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://yaahabibi.com/api/games/spin/my-history?limit=" + quantity);

        string bearerToken = GetComponent<QueryStringManager>().bearerToken;
        request.SetRequestHeader("Authorization", "Bearer " + bearerToken);
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");
        request.SetRequestHeader("Access-Control-Allow-Origin", "https://yaahabibi.live/games/spinner/");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {

            Debug.Log(request.downloadHandler.text);
            string responseData = request.downloadHandler.text;
            RootHistory myDeserializedClass = JsonConvert.DeserializeObject<RootHistory>(responseData);

            foreach (var data in myDeserializedClass.data.history)
            {
                Image obj = Instantiate(records, recordHolder.transform.position, Quaternion.identity);
                obj.transform.SetParent(recordHolder);
                obj.transform.localScale = recordHolder.localScale;
                foreach (var bet in data.bets)
                {
                    int amount = bet.amount;
                    myBets.Add(bet.seat_no);
                    myBetAmounts.Add(amount);
                }

                string date = data.created_at.ToString();
                obj.GetComponent<RecordData>().date.text = date;
                obj.GetComponent<RecordData>().round.text = "Round: " + data.id.ToString();

                int winNumber = data.winner_seat;
                int winAmount = data.reward;
                bool isWinning = false;
                int winningBetAmount = 0;

                for (int i = 0; i < myBets.Count; i++)
                {
                    if(winNumber == myBets[i])
                    {
                        isWinning = true;
                        winningBetAmount = myBetAmounts[i];
                    }
                    Debug.Log(i);
                    obj.GetComponent<RecordData>().betImages[i].GetComponent<Image>().sprite = SpiningManager.Instance.winningElements[myBets[i] - 1].sprite;
                    obj.GetComponent<RecordData>().betAmounts[i].GetComponent<TextMeshProUGUI>().text = myBetAmounts[i].ToString();
                }

                if(isWinning)
                {
                    obj.GetComponent<RecordData>().winAmounts.GetComponent<TextMeshProUGUI>().text = "+"+(winningBetAmount*winAmount).ToString();
                }
                else
                {
                    obj.GetComponent<RecordData>().winAmounts.GetComponent<TextMeshProUGUI>().text = winningBetAmount.ToString();
                }
                
                obj.GetComponent<RecordData>().winImage.GetComponent<Image>().sprite = SpiningManager.Instance.winningElements[winNumber - 1].sprite;

                if (myBets.Count < 2)
                {
                    obj.GetComponent<RecordData>().betImages[1].gameObject.SetActive(false);
                    obj.GetComponent<RecordData>().betAmounts[1].gameObject.SetActive(false);
                }

                ClearLastTenPrefabs();
            }


        }
    }

    private void ClearLastTenPrefabs()
    {

        myBets.Clear();
        myBetAmounts.Clear();
    }



    public IEnumerator GetPlayerProfileData()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log("In_1");
        UnityWebRequest request = UnityWebRequest.Get("https://yaahabibi.com/api/user/balance");
        Debug.Log("In_2");
        string bearerToken = GameObject.Find("BackendManager").GetComponent<QueryStringManager>().bearerToken;
        Debug.Log("In_3");
        Debug.Log(bearerToken);
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");
        request.SetRequestHeader("Authorization", "Bearer " + bearerToken);
        request.SetRequestHeader("Access-Control-Allow-Origin", "https://yaahabibi.live/games/spinner/");

        //request.SetRequestHeader("Access-Control-Allow-Origin", "*");



        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {

            Debug.Log(request.downloadHandler.text);
            string responseData = request.downloadHandler.text;
            ProfileRoot myDeserializedClass = Newtonsoft.Json.JsonConvert.DeserializeObject<ProfileRoot>(responseData);

            coins = myDeserializedClass.data.user.balance;
            string profilePicLink = myDeserializedClass.data.user.thumbnail;
            StartCoroutine(LoadProfilePicFromURL(profilePicLink));
            /*  if (coins <= 0)
              {
                  foreach (Button bidbtn in bidButtons)
                  {
                      bidbtn.GetComponent<Button>().enabled = false;

                  }

                  foreach (Button colliderBtn in colliderBtns)
                  {
                      colliderBtn.GetComponent<Button>().enabled = false;

                  }



              }
              else
              {
                  notEnoughCoin.SetActive(false);
                  canBid = true;

              }*/
            Debug.Log("Coins " + coins);
            coin_text.text = coins.ToString();

        }

    }

    IEnumerator LoadProfilePicFromURL(string url)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            // Get the downloaded texture
            Texture2D texture = DownloadHandlerTexture.GetContent(www);

            // Convert the texture into a sprite
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

            // Apply the sprite to the Image component
            if (profilePic != null)
            {
                profilePic.sprite = sprite;
            }
        }
        else
        {
            Debug.Log("Failed to load image: " + www.error);
        }
    }

    public void RankingRequest(int quantity)
    {
        if (!isRankingActive)
        {
            isRankingActive = true;
            StartCoroutine(GetRankingData(quantity));
        }
        
    }

    public IEnumerator GetRankingData(int quantity)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://yaahabibi.com/api/games/spin/leaderboard?limit=" + quantity);
        string bearerToken = GetComponent<QueryStringManager>().bearerToken;
        request.SetRequestHeader("Authorization", "Bearer " + bearerToken);
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");
        request.SetRequestHeader("Access-Control-Allow-Origin", "https://yaahabibi.live/games/spinner/");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {

            Debug.Log(request.downloadHandler.text);
            string responseData = request.downloadHandler.text;
            RankingRoot myDeserializedClass = JsonConvert.DeserializeObject<RankingRoot>(responseData);

            foreach (var data in myDeserializedClass.data.history)
            {
                Image obj = Instantiate(rankingRecord, rankingHolder.transform.position, Quaternion.identity);
                obj.transform.SetParent(rankingHolder);
                obj.transform.localScale = rankingHolder.localScale;

                rankNumber++;
                obj.GetComponent<RRecord>().rank.GetComponent<TextMeshProUGUI>().text = rankNumber.ToString();
                obj.GetComponent<RRecord>().name.GetComponent<TextMeshProUGUI>().text = data.user.name;
                obj.GetComponent<RRecord>().earnings.GetComponent<TextMeshProUGUI>().text = data.earnings.ToString();

                string imageLink = data.user.avatar;
                Debug.Log(imageLink);

                StartCoroutine(LoadImageCoroutine(imageLink, obj));



            }

            IEnumerator LoadImageCoroutine(string url, Image obj)
            {
                using (WWW www = new WWW(url))
                {
                    yield return www;

                    if (string.IsNullOrEmpty(www.error))
                    {
                        Texture2D texture = www.texture;
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

                        obj.GetComponent<RRecord>().playerImage.GetComponent<Image>().sprite = sprite;

                    }
                    else
                    {
                        Debug.LogError("Failed to load image: " + www.error);
                    }
                }
            }


        }

    }
    public void PublicPostData(string deck, int amount)
    {
        StartCoroutine(PostData(deck, amount.ToString()));
        
    }

    private IEnumerator PostData(string element_no, string amount)
    {
        WWWForm form = new WWWForm();
        form.AddField("amount", amount);
        form.AddField("seat_no", element_no);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        string bearerToken = GameObject.Find("BackendManager").GetComponent<QueryStringManager>().bearerToken;
        request.SetRequestHeader("Authorization", "Bearer " + bearerToken);
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");
        request.SetRequestHeader("Access-Control-Allow-Origin", "https://yaahabibi.live/games/spinner/");

        Debug.Log("Data: "+ element_no + " " + amount);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error posting data: " + request.error);
        }
        else
        {

            string responseData = request.downloadHandler.text;
            Debug.Log("gdg" + responseData);
            Debug.Log("Data posted successfully from deck");


        }


    }

    public void PublicResultApi(int fruitNo, int reward)
    {
        StartCoroutine(PublishResult(fruitNo.ToString(), reward.ToString()));

    }

    public IEnumerator PublishResult(string element_no, string reward)
    {
        WWWForm form = new WWWForm();
        form.AddField("seat_no", element_no);
        form.AddField("reward", reward);


        UnityWebRequest request = UnityWebRequest.Post(resultUrl, form);
        string bearerToken = GameObject.Find("BackendManager").GetComponent<QueryStringManager>().bearerToken;
        request.SetRequestHeader("Authorization", "Bearer " + bearerToken);
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");
        request.SetRequestHeader("Access-Control-Allow-Origin", "https://yaahabibi.live/games/spinner/");


        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error posting data: " + request.error);
        }
        else
        {

            string responseData = request.downloadHandler.text;
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(responseData);

            betThisRound.text = myDeserializedClass.data.result.bet_amount.ToString();
            roundNumber.text = myDeserializedClass.data.result.round_number.ToString();
            total_win.text = myDeserializedClass.data.result.reward_amount.ToString();

            Debug.Log("gdg" + responseData);
            Debug.Log("Result Data posted successfully from deck");


        }


    }

    public void PublicRepeatBet()
    {
        StartCoroutine(RepeatBidWebRequest());
    }

    IEnumerator RepeatBidWebRequest()
    {
        yield return new WaitForSeconds(0.2f);
        WWWForm form = new WWWForm();
        form.AddField("Dummy_1", 1);



        UnityWebRequest request = UnityWebRequest.Post(repeatUrl, form);
        string bearer = GameObject.Find("BackendManager").GetComponent<QueryStringManager>().bearerToken;
        request.SetRequestHeader("Authorization", "Bearer " + bearer);
        request.SetRequestHeader("Access-Control-Allow-Origin", "*");
        request.SetRequestHeader("Access-Control-Allow-Origin", "https://yaahabibi.live/games/spinner/");
        //request.SetRequestHeader("Access-Control-Allow-Origin", "*");



        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error posting data: " + request.error);
        }
        else
        {

            string responseData = request.downloadHandler.text;
            Debug.Log("repeat bid" + responseData);
            RepeatBidMain myDeserializedClass = JsonConvert.DeserializeObject<RepeatBidMain>(responseData);

            //pot_text[0].text = myDeserializedClass.data.seat_1.ToString();
            /*Debug.Log("Repeat" + myDeserializedClass.data.seat_1);
            Debug.Log("Repeat" + myDeserializedClass.data.seat_2);
            Debug.Log("Repeat" + myDeserializedClass.data.seat_3);*/
            //pot_text[1].text = myDeserializedClass.data.seat_2.ToString();
            //pot_text[2].text = myDeserializedClass.data.seat_3.ToString();
            //my_Bid.text = myDeserializedClass.data.total_amount.ToString();
            /*PlayerPrefs.SetInt("seat1_amount", myDeserializedClass.data.seat_1);
            PlayerPrefs.SetInt("seat2_amount", myDeserializedClass.data.seat_2);
            PlayerPrefs.SetInt("seat3_amount", myDeserializedClass.data.seat_3);
            PlayerPrefs.Save();*/
            r1 = myDeserializedClass.data.seat_1;
            r2 = myDeserializedClass.data.seat_2;
            r3 = myDeserializedClass.data.seat_3;
            if (CoinManager.Instance.pot_coin1 == 0 && CoinManager.Instance.pot_coin2 == 0 && CoinManager.Instance.pot_coin1 == 0)
            {
                if (r1 > 0)
                {
                    Debug.Log("Seat1: " + r1);
                    CoinManager.Instance.bidbtn(1);
                    CoinManager.Instance.SpendCoins(r1);
                }
                if (r2 > 0)
                {
                    Debug.Log("Seat2: " + r2);
                    CoinManager.Instance.bidbtn(2);
                    CoinManager.Instance.SpendCoins(r2);
                }
                if (r3 > 0)
                {
                    Debug.Log("Seat3: " + r3);
                    CoinManager.Instance.bidbtn(3);
                    CoinManager.Instance.SpendCoins(r3);
                }
            }
            

        }


    }

}
