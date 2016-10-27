using UnityEngine;
using System.Collections;

public class MataODiabo : MonoBehaviour {

	public bool podeMatar = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if ((col.gameObject.tag == "MonstroEscuro" || col.gameObject.name == "inimigoEstatua") && podeMatar) {
			col.gameObject.GetComponent<MonstroEscuro>().MataComFogo();
		} 


	}

}
