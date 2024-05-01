using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    int deck1, deck2, deck3, deck4, deck5, deck6,deck7,deck8,deck9,deck10;
    int save_deck1, save_deck2, save_deck3, save_deck4, save_deck5, save_deck6, save_deck7, save_deck8, save_deck9, save_deck10;
    public int bid_btn_states;
    public int coins = 0;
    public TextMeshProUGUI coin_text;
    public TextMeshProUGUI[] pot_text;
    public int pot_coin1, pot_coin2, pot_coin3, pot_coin4, pot_coin5, pot_coin6, pot_coin7, pot_coin8, pot_coin9, pot_coin10;
    public int pot_coin1_save, pot_coin2_save, pot_coin3_save, pot_coin4_save, pot_coin5_save, pot_coin6_save, pot_coin7_save, pot_coin8_save, pot_coin9_save, pot_coin10_save;
    public int spend_amount;
    public bool savedCoins = false;
    public Image newGame;
    void Start()
    {
       
        coin_text.text = ApiManager.Instance.coins.ToString();
        pot_coin1 = 0;
        pot_coin2 = 0;
        pot_coin3 = 0;
        pot_coin4 = 0;
        pot_coin5 = 0;
        pot_coin6 = 0;
        pot_coin7 = 0;
        pot_coin8 = 0;
        pot_coin9 = 0;
        pot_coin10 = 0;
        pot_text[0].text = "" + pot_coin1;
        pot_text[1].text = "" + pot_coin2;
        pot_text[2].text = "" + pot_coin3;
        pot_text[3].text = "" + pot_coin4;
        pot_text[4].text = "" + pot_coin5;
        pot_text[5].text = "" + pot_coin6;
        pot_text[6].text = "" + pot_coin7;
        pot_text[7].text = "" + pot_coin8;
        pot_text[8].text = "" + pot_coin9;
        pot_text[9].text = "" + pot_coin10;



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
            deck1 = 1;
            //save_deck1 = 1;
            //PlayerPrefs.SetInt("save_deck1", save_deck1);

        }
        if (bid_btn_states == 2)
        {
            deck2 = 1;
            //save_deck2 = 1;
            //PlayerPrefs.SetInt("save_deck2", save_deck2);

        }
        if (bid_btn_states == 3)
        {
            deck3 = 1;
            //save_deck3 = 1;
            //PlayerPrefs.SetInt("save_deck3", save_deck3);

        }
        if(bid_btn_states == 4)
        {
            deck4 = 1;
        }
        if (bid_btn_states == 5)
        {
            deck5 = 1;
        }
        if (bid_btn_states == 6)
        {
            deck6 = 1;
        }
        if (bid_btn_states == 7)
        {
            deck7 = 1;
        }
        if (bid_btn_states == 8)
        {
            deck8 = 1;
        }
        if (bid_btn_states == 9)
        {
            deck9 = 1;
        }
        if(bid_btn_states == 10)
        {
            deck10 = 1;
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
                    pot_coin1 += amount;
                    /*pot_coin1_save = pot_coin1;
                    PlayerPrefs.SetInt("Pot_Coins_1", pot_coin1_save);*/
                    pot_text[0].text = pot_coin1.ToString();
                    ApiManager.Instance.PublicPostData("1", amount);
                    Debug.Log("Enter to spend coins-1");
                    break;

                case 2:
                    pot_coin2 += amount;
                    /*pot_coin2_save = pot_coin2;
                    PlayerPrefs.SetInt("Pot_Coins_2", pot_coin2_save);*/
                    pot_text[1].text = pot_coin2.ToString();
                    ApiManager.Instance.PublicPostData("2", amount);
                    Debug.Log("Enter to spend coins-2");
                    break;

                case 3:
                    pot_coin3 += amount;
                    /*pot_coin3_save = pot_coin3;
                    PlayerPrefs.SetInt("Pot_Coins_3", pot_coin3_save);*/
                    pot_text[2].text = pot_coin3.ToString();
                    ApiManager.Instance.PublicPostData("3", amount);
                    Debug.Log("Enter to spend coins-3");
                    break;

                case 4:
                    pot_coin4 += amount;
                    pot_text[3].text = pot_coin4.ToString();
                    ApiManager.Instance.PublicPostData("4", amount);
                    Debug.Log("Enter to spend coins-4");
                    break;
                case 5:
                    pot_coin5 += amount;
                    pot_text[4].text = pot_coin5.ToString();
                    ApiManager.Instance.PublicPostData("5",amount);
                    Debug.Log("Enter to spend coins-5");
                    break;

                case 6:
                    pot_coin6 += amount;
                    pot_text[5].text = pot_coin6.ToString();
                    ApiManager.Instance.PublicPostData("6",amount);
                    Debug.Log("Enter to spend coins-6");
                    break;

                case 7:
                    pot_coin7 += amount;
                    pot_text[6].text = pot_coin7.ToString();
                    ApiManager.Instance.PublicPostData("7", amount);
                    Debug.Log("Enter to spend coins-7");
                    break;

                case 8:
                    pot_coin8 += amount;
                    pot_text[7].text = pot_coin8.ToString();
                    ApiManager.Instance.PublicPostData("8", amount);
                    Debug.Log("Enter to spend coins-8");
                    break;

                case 9:
                    pot_coin9 += amount;
                    pot_text[8].text = pot_coin9.ToString();
                    ApiManager.Instance.PublicPostData("9", amount);
                    Debug.Log("Enter to spend coins-9");
                    break;

                case 10:
                    pot_coin10 += amount;
                    pot_text[9].text = pot_coin10.ToString();
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
