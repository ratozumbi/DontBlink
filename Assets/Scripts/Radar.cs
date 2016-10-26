using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Radar : MonoBehaviour {
	public AudioClip Alerta;

	void Start () {
	
	}

	void Update () {
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "inimigoEstatua") {
			/*SceneManager.LoadScene(SceneManager.GetActiveScene().name);*/

			GetComponent<AudioSource> ().PlayOneShot (Alerta);
		}
	}
}



