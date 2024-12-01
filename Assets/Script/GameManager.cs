using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPun
{
    public int timer  ;
    public TextMeshProUGUI countdownText;
    public Toggle toggleButton;
    PhotonView pView;
    public Image newGame;
    bool isRepeatBetPossible = true;

    private void Awake()
    {
        pView = GetComponent<PhotonView>();
    }
    void Start()
    {

       
        if (PhotonNetwork.IsMasterClient)
        {
            pView.RPC("TImeCounter", RpcTarget.AllBuffered);

        }
       
        AudioManager.instance.Play("Bg");
        //StartCoroutine(CountdownCoroutine());
      
    }


    [PunRPC]
    public void TImeCounter()
    {
        StartCoroutine(CountdownCoroutine());
    }


    IEnumerator CountdownCoroutine()
    {
       
        while (timer > 0) // Change condition from timer >= 0 to timer > 0
        {
            yield return new WaitForSeconds(1f);
            timer--;
            countdownText.text = timer.ToString();

            if (timer == 0)
            {
                SpiningManager.Instance.StartSpin();
            }
        }

        // Ensure the countdown text displays 0 after the loop completes
        countdownText.text = "0";
    }

    public void OnToggleValueChanged()
    {
        
        if (GameObject.Find("Toggle").GetComponent<Toggle>().isOn)
        {
            Debug.Log("Toggle is ON");
            PlayerPrefs.SetInt("Toogle value", 1);

            
            for (int i = 0; i < 10; i++)
            {
                if (CoinManager.Instance.pot_coin[i] != 0)
                {
                    isRepeatBetPossible = false;
                }
            }

            if (isRepeatBetPossible)
            {
                Debug.Log("Repeat Bet Enabled 2");
                ApiManager.Instance.PublicRepeatBet();
                isRepeatBetPossible = false;
            }
                
        }
        else
        {
            Debug.Log("Toggle is OFF");
            PlayerPrefs.SetInt("Toogle value", 0);
        }
    }
}
