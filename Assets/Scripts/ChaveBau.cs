using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class ChaveBau : MonoBehaviour {
	public int IDdaChave;
	public float DistanciaDaChave = 3;
	public AudioClip somChave;
	private bool BauAberto;
	private GameObject Jogador;

	void Start (){
		BauAberto = false;
		Jogador = GameObject.FindWithTag ("Player");
	}
	void Update () {
		if (Vector3.Distance (transform.position, Jogador.transform.position) < DistanciaDaChave) {
			if (Input.GetKeyDown ("e") && BauAberto) {
				GetComponent<MeshRenderer> ().enabled = false;
				GetComponent<AudioSource> ().PlayOneShot (somChave);
				Destroy (gameObject, 2.0f);
			} else if (Input.GetKeyDown ("e") && BauAberto == false){
				BauAberto = true;	
			}
		}
	}
}