using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class CHAVE : MonoBehaviour{
	public int IDdaChave;
	public float DistanciaDaChave = 3;
	public AudioClip somChave;
	private bool PegouChave;
	private GameObject Jogador;
	void Start (){
		PegouChave = false;
		Jogador = GameObject.FindWithTag ("Player");
	}
	void Update () {
		if (Vector3.Distance (transform.position, Jogador.transform.position) < DistanciaDaChave) {
			if ( (CustomInput.gatilhoJoystick() ||Input.GetKeyDown ("e")) && PegouChave == false) {
				PORTA.ListaDeIDs.Add (IDdaChave);
				PegouChave = true;
				GetComponent<MeshRenderer> ().enabled = false;
				GetComponent<AudioSource> ().PlayOneShot (somChave);
				Destroy (gameObject,2.0f);

				if (GameObject.Find ("carta").GetComponent<carta> ().mostrarCarta == false) {
					GameObject.FindWithTag ("Monstro").GetComponent<Monstro> ().anda = false;
					GameObject.FindWithTag ("Monstro").GetComponent<Monstro> ().enabled = false;
				} else {
					GameObject.FindWithTag ("Monstro").GetComponent<Monstro> ().enabled = true;
				}

			}
		}
	}
}