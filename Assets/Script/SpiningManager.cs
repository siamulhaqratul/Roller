using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SpiningManager : MonoBehaviour {

	public static SpiningManager Instance;

	int randVal;
	private float timeInterval;
	private bool isCoroutine;
	private int finalAngle;

	public TextMeshProUGUI winText;
	public int section;
	float totalAngle;
	public string[] PrizeName;
	public GameObject winningPanel;
	public Image[] winningElements;

    PhotonView pView;

    private void Awake()
    {
		Instance = this;
        pView = GetComponent<PhotonView>();
    }

  
    private void Start () {
		isCoroutine = true;
		totalAngle = 360 / 4;
	}

	
	private void Update () {

		
	}

	public void StartSpin() 
	{
        StartCoroutine(Spin());

    }

	private IEnumerator Spin(){

		isCoroutine = false;
		randVal = Random.Range (200, 300);
		AudioManager.instance.Play("Count");
		timeInterval = 0.0001f*Time.deltaTime*2;

		for (int i = 0; i < randVal; i++) {

			transform.Rotate (0, 0, (totalAngle)); 


			//To slow Down Wheel
			if (i > Mathf.RoundToInt (randVal * 0.2f))
				timeInterval = 0.5f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.5f))
				timeInterval = 1f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.7f))
				timeInterval = 1.5f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.8f))
				timeInterval = 2f*Time.deltaTime;
			if (i > Mathf.RoundToInt (randVal * 0.9f))
				timeInterval = 2.5f*Time.deltaTime;

			yield return new WaitForSeconds (timeInterval);

		}

		if (Mathf.RoundToInt (transform.eulerAngles.z) % totalAngle != 0) //when the indicator stop between 2 numbers,it will add aditional step 
			transform.Rotate (0, 0, totalAngle);
		
		finalAngle = Mathf.RoundToInt (transform.eulerAngles.z);//round off euler angle of wheel value

		Debug.Log("spin" + finalAngle);

		//Prize check
		for (int i = 0; i < section; i++) {

			if (finalAngle == i * totalAngle) 
			{
                if (PhotonNetwork.IsMasterClient)
                {
                    pView.RPC("WinningPanelActive", RpcTarget.AllBuffered, i);

                }


            }

        }

	
		isCoroutine = true;
	}

	[PunRPC]
	public void WinningPanelActive(int i) 
	{
        StartCoroutine("ActivateWiningPanel", i);
    }

	public IEnumerator ActivateWiningPanel(int i) 
	{
        AudioManager.instance.Stop("Count");
        yield return new WaitForSeconds(1);

        if (PhotonNetwork.IsMasterClient)
        {
            pView.RPC("SceneLoad", RpcTarget.AllBuffered);
        }

		int reward = 0;
		int seat = 0;
		if(i == 0 || i == 3 || i == 5)
		{
			seat = 1;
			reward = 2;
		}
		else if(i == 2 || i == 6)
		{
            seat = 3;
            reward = 2;
        }
		else if(i == 1 || i == 4 ||  i == 7)
		{
			seat = 2;
			reward = 8;
		}
        ApiManager.Instance.PublicResultApi(seat, reward);
        winText.text = PrizeName[i];
        winningPanel.SetActive(true);
        winningElements[i].gameObject.SetActive(true);
		AudioManager.instance.Play("Over");
        yield return new WaitForSeconds(2);
        winningPanel.SetActive(false);
        CoinManager.Instance.newGame.gameObject.SetActive(true);
    }


    [PunRPC]
	public void SceneLoad() 
	{
        StartCoroutine("Loadscene");
    }

	public IEnumerator Loadscene() 
	{
		yield return new WaitForSeconds(5);
        CoinManager.Instance.newGame.gameObject.SetActive(false);
		Debug.Log("New Game Started");
        SceneManager.LoadScene(1);
	
	}
}
