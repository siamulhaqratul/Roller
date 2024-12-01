using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public int[] deck;
    //int[] save_deck;
    public int bid_btn_states;
    public int coins = 0;
    public TextMeshProUGUI coin_text;
    public TextMeshProUGUI[] pot_text;
    public int[] pot_coin;
    //public int pot_coin1_save, pot_coin2_save, pot_coin3_save, pot_coin4_save, pot_coin5_save, pot_coin6_save, pot_coin7_save, pot_coin8_save, pot_coin9_save, pot_coin10_save;
    public int spend_amount;
    public bool savedCoins = false;
    public Image newGame;
    void Start()
    {
        
        coin_text.text = ApiManager.Instance.coins.ToString();
        for (int i = 0; i < 10; i++)
        {
            pot_text[i].text = "" + pot_coin[i];
        }
        /*save_deck1 = PlayerPrefs.GetInt("save_deck1");
        save_deck2 = PlayerPrefs.GetInt("save_deck2");
        save_deck3 = PlayerPrefs.GetInt("save_deck3");*/

        /*if (PlayerPrefs.GetInt("Toogle value")==1) 
        {
            *//*pot_coin1_save = PlayerPrefs.GetInt("Pot_Coins_1");
            //Debug.Log(pot_coin1_save);
            //pot_text[0].text = pot_coin1_save.ToString();*//*
            pot_text[0].text = "" + pot_coin1_save;
            *//*pot_coin2_save = PlayerPrefs.GetInt("Pot_Coins_2");*//*
            pot_text[1].text = "" + pot_coin2_save;
            *//*pot_coin3_save = PlayerPrefs.GetInt("Pot_Coins_3");*//*
            pot_text[2].text = "" + pot_coin3_save;


        }*/


    }

    private void Awake()
    {
        Instance = this;
        
    }

    void Update()
    {

        
        /*if (deck1 == 1 && deck2 == 1)
        {
            GameObject.Find("C").GetComponent<Button>().interactable = false;
        }
        if (deck1 == 1 && deck3 == 1)
        {
            GameObject.Find("B").GetComponent<Button>().interactable = false;
        }
        if (deck3 == 1 && deck2 == 1)
        {
            GameObject.Find("A").GetComponent<Button>().interactable = false;
        }*/

    }

    public void bidbtn(int bid_btn_state)
    { 
        bid_btn_states = bid_btn_state;
        if (bid_btn_states == 1)
        {
            deck[0] = 1;
        }
        if (bid_btn_states == 2)
        {
            deck[1] = 1;
        }
        if (bid_btn_states == 3)
        {
            deck[2] = 1;
        }
        if(bid_btn_states == 4)
        {
            deck[3] = 1;
        }
        if (bid_btn_states == 5)
        {
            deck[4] = 1;
        }
        if (bid_btn_states == 6)
        {
            deck[5] = 1;
        }
        if (bid_btn_states == 7)
        {
            deck[6] = 1;
        }
        if (bid_btn_states == 8)
        {
            deck[7] = 1;
        }
        if (bid_btn_states == 9)
        {
            deck[8] = 1;
        }
        if(bid_btn_states == 10)
        {
            deck[9] = 1;
        }
        //PlayerPrefs.Save();
       
    }

    public void SpendCoins(int amount)
    {
        coins = ApiManager.Instance.coins;
        if (coins >= amount)
        {
            ApiManager.Instance.coins -= amount;
            coins -= amount;
            coin_text.text = coins.ToString();

            switch (bid_btn_states)
            {
                case 1:
                    pot_coin[0] += amount;
                    /*pot_coin1_save = pot_coin1;
                    PlayerPrefs.SetInt("Pot_Coins_1", pot_coin1_save);*/
                    pot_text[0].text = pot_coin[0].ToString();
                    ApiManager.Instance.PublicPostData("1", amount);
                    Debug.Log("Enter to spend coins-1 : " + amount);
                    break;

                case 2:
                    pot_coin[1] += amount;
                    /*pot_coin2_save = pot_coin2;
                    PlayerPrefs.SetInt("Pot_Coins_2", pot_coin2_save);*/
                    pot_text[1].text = pot_coin[1].ToString();
                    ApiManager.Instance.PublicPostData("2", amount);
                    Debug.Log("Enter to spend coins-2 : " + amount);
                    break;

                case 3:
                    pot_coin[2] += amount;
                    /*pot_coin3_save = pot_coin3;
                    PlayerPrefs.SetInt("Pot_Coins_3", pot_coin3_save);*/
                    pot_text[2].text = pot_coin[2].ToString();
                    ApiManager.Instance.PublicPostData("3", amount);
                    Debug.Log("Enter to spend coins-3 : " + amount);
                    break;

                case 4:
                    pot_coin[3] += amount;
                    pot_text[3].text = pot_coin[3].ToString();
                    ApiManager.Instance.PublicPostData("4", amount);
                    Debug.Log("Enter to spend coins-4 : " + amount);
                    break;
                case 5:
                    pot_coin[4] += amount;
                    pot_text[4].text = pot_coin[4].ToString();
                    ApiManager.Instance.PublicPostData("5",amount);
                    Debug.Log("Enter to spend coins-5 : " + amount);
                    break;

                case 6:
                    pot_coin[5] += amount;
                    pot_text[5].text = pot_coin[5].ToString();
                    ApiManager.Instance.PublicPostData("6",amount);
                    Debug.Log("Enter to spend coins-6 : " + amount);
                    break;

                case 7:
                    pot_coin[6] += amount;
                    pot_text[6].text = pot_coin[6].ToString();
                    ApiManager.Instance.PublicPostData("7", amount);
                    Debug.Log("Enter to spend coins-7 : " + amount);
                    break;

                case 8:
                    pot_coin[7] += amount;
                    pot_text[7].text = pot_coin[7].ToString();
                    ApiManager.Instance.PublicPostData("8", amount);
                    Debug.Log("Enter to spend coins-8 : " + amount);
                    break;

                case 9:
                    pot_coin[8] += amount;
                    pot_text[8].text = pot_coin[8].ToString();
                    ApiManager.Instance.PublicPostData("9", amount);
                    Debug.Log("Enter to spend coins-9 : " + amount);
                    break;

                case 10:
                    pot_coin[9] += amount;
                    pot_text[9].text = pot_coin[9].ToString();
                    ApiManager.Instance.PublicPostData("10", amount);
                    Debug.Log("Enter to spend coins-10");
                    break;

                default:
                    Debug.Log("Invalid bid_btn_states value: " + bid_btn_states);
                    break;
            }
        }
        else
        {
            Debug.Log("Insufficient coins");
        }
    }
}
