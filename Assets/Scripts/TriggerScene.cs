using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerScene : MonoBehaviour {

	private bool jobDone = false;
	public string sceneToLoad = "";
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other)
	{
		if (!jobDone && other.tag == "Player") {
			jobDone = true;
			SceneManager.LoadScene(sceneToLoad);

		}
	}
}
