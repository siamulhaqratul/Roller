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


    private void Start()
    {
        isCoroutine = true;
        totalAngle = 360 / 4;
    }

    private void Update()
    {

    }

    public void StartSpin()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        isCoroutine = false;
        // Phase 1: Initial Spin
        float initialSpinDuration = 4f; // Duration of initial spinning in seconds
        float initialRotationSpeed = 360f / initialSpinDuration; // Rotation speed for initial spin
        AudioManager.instance.Play("Count");

        float elapsedTime = 0f;
        while (elapsedTime < initialSpinDuration)
        {
            transform.Rotate(0, 0, initialRotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Phase 2: Acceleration
        float accelerationDelay = 0f; // Delay before acceleration in seconds
        yield return new WaitForSeconds(accelerationDelay);

        float accelerationDuration = 5f; // Duration of acceleration in seconds
        float finalRotationSpeed = 720f / accelerationDuration; // Final rotation speed for acceleration

        elapsedTime = 0f;
        while (elapsedTime < accelerationDuration)
        {
            transform.Rotate(0, 0, finalRotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Phase 3: Stop
        float stopDuration = 3f; // Duration before stopping in seconds
        
        float sectionval = Random.Range(0,15);
        // Ensure the wheel stops at a valid angle
        transform.rotation = Quaternion.Euler(0, 0, Mathf.RoundToInt(transform.eulerAngles.z + (sectionval*22.5f)));
      
        //finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
        Debug.Log("spin" + finalAngle);

        yield return new WaitForSeconds(stopDuration);

        // Check the final angle to determine the prize
        for (int i = 0; i < section; i++)
        {
            if (finalAngle == i * (360 / section))
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    pView.RPC("WinningPanelActive", RpcTarget.AllBuffered, i);
                }
            }
        }
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
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
