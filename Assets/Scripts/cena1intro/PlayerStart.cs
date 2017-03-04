using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStart : MonoBehaviour {

	public GameObject cardboard;
	public Text texto;

	private float mytime = 0;

	void Update ()
	{
		#if (!UNITY_STANDALONE_WIN && !UNITY_EDITOR)
		if(CustomInput.ControlMode == 0){ 
			CustomInput.ControlMode = 0;
			mytime = Time.time;
			return;
			
		}
		else
		{
		vai();
		}
		#else
		vai();
		#endif

	}

	void vai ()
	{
		if(GameObject.Find ("titulo")!= null)GameObject.Find ("titulo").SetActive (false);
		cardboard.SetActive (true);

		texto.text = "Coloque o celular no oculos VR ";

		if (Time.time > mytime + 6) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		}
	}
		

	
	

}
