using UnityEngine;
using System.Collections;

public class ProvedorDeLuz : MonoBehaviour {
 

	private Light playerLuz;
	private Light minhaLuz;

	void Start () {
		GameObject tochaPlayer;
		tochaPlayer = GameObject.FindGameObjectWithTag ("TochaPlayer");
		playerLuz = tochaPlayer.GetComponent<Light> ();
		minhaLuz = GetComponent <Light> ();
	}
	

	public void sendCor(){
		playerLuz.color = minhaLuz.color;
	}

	void OnTriggerEnter(Collider other)
	{

		#if(UNITY_EDITOR)

		sendCor();
		#endif
	}

	void OnTriggerStay(Collider other)
	{
		if (Input.GetKeyDown (KeyCode.JoystickButton0) ||
			Input.GetKeyDown (KeyCode.JoystickButton1) ||
			Input.GetKeyDown (KeyCode.JoystickButton2) ||
			Input.GetKeyDown (KeyCode.JoystickButton3) ||
			Input.GetKeyDown (KeyCode.JoystickButton4) ||
			Input.GetKeyDown (KeyCode.JoystickButton5))
		{
			sendCor();
		}
	}
}
