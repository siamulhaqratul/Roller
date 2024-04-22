using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    int deck1, deck2, deck3;
    int save_deck1, save_deck2, save_deck3;
    public int bid_btn_states;
    public int coins = 0;
    public TextMeshProUGUI coin_text;
    public TextMeshProUGUI[] pot_text;
    public int pot_coin1, pot_coin2, pot_coin3;
    public int pot_coin1_save, pot_coin2_save, pot_coin3_save;
    public int spend_amount;
    public bool savedCoins = false;
    public Image newGame;
    void Start()
    {
       
        coin_text.text = ApiManager.Instance.coins.ToString();
        pot_coin1 = 0;
        pot_coin2 = 0;
        pot_coin3 = 0;
        pot_text[0].text = "" + pot_coin1;
        pot_text[1].text = "" + pot_coin2;
        pot_text[2].text = "" + pot_coin3;


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

        coins = ApiManager.Instance.coins;
        if (deck1 == 1 && deck2 == 1)
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
        }

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
        //PlayerPrefs.Save();
       
    }

    public void SpendCoins(int amount)
    {
        if (coins >= amount)
        {
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
