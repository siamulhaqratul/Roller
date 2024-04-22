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
            if (CoinManager.Instance.pot_coin1 == 0 && CoinManager.Instance.pot_coin2 == 0 && CoinManager.Instance.pot_coin1 == 0)
                ApiManager.Instance.PublicRepeatBet();
        }
        else
        {
            Debug.Log("Toggle is OFF");
            PlayerPrefs.SetInt("Toogle value", 0);
        }
    }
}
