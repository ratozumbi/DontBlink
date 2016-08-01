using UnityEngine;
using System.Collections;

public class luzPoint : MonoBehaviour {

	private Light myLight;
	// Use this for initialization
	void Start () {
	
		myLight = gameObject.GetComponent<Light> ();

	}
	
	// Update is called once per frame
	void Update () {

		Collider[] allInimigos; 
		allInimigos = Physics.OverlapSphere (transform.position, myLight.range/*, LayerMask.NameToLayer ("LayerInimigo")*/);
	
		foreach (Collider i in allInimigos) {
			if (i.transform.tag == "Monstro") {
				Monstro monstro = i.gameObject.GetComponent<Monstro> ();
				monstro.iluminado = true;
			}
		}

	
	}
}
