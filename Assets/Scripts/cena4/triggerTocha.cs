using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class triggerTocha : MonoBehaviour {

	private bool jobDone = false;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		
		#if(OFF )

		if (!jobDone && other.tag == "Player") {
			jobDone = true;
			GameObject tochaPlayer = GameObject.Find ("tochaPlayer");
			if (!tochaPlayer)
				Debug.Log ("nao encontrei a tocha no player");
			else
				tochaPlayer.transform.localPosition = new Vector3(0.453f,-0.069f,0.455f);
			this.gameObject.SetActive (false);
			Debug.Log ("done tocha");

		}

		#endif
	}

	void OnTriggerStay(Collider other)
	{
		if (CustomInput.gatilhoJoystick() || Input.GetKey(KeyCode.E))
		{
			GameObject tochaPlayer = GameObject.Find ("tochaPlayer");
			if (!tochaPlayer)
				Debug.Log ("nao encontrei a tocha no player");
			else
				tochaPlayer.transform.localPosition = new Vector3(0.453f,-0.069f,0.455f);
			this.gameObject.SetActive (false);
		}
	}
}
