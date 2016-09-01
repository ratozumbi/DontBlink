using UnityEngine;
using System.Collections;

public class trigger : MonoBehaviour {

	public GameObject inimigo;
	private bool jobDone = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (!jobDone && other.tag == "Player") {
			inimigo.AddComponent<Monstro> ();
			jobDone = true;
		}
	}
}
