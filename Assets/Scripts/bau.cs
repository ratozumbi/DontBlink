using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class bau : MonoBehaviour {
	public AudioClip Abrindo;
	public float distanciaParaAbrir = 3;
	private bool PodeAbrir, BauAberto;
	private GameObject Jogador;
	void Start (){
	   Jogador = GameObject.FindWithTag ("Player");
		BauAberto = false;
	}

	void Update (){
		// CHECAHDO SE ESTA PERTO OU NAO
		if (Vector3.Distance (transform.position, Jogador.transform.position) <= distanciaParaAbrir) {
			PodeAbrir = true;
		} else if (Vector3.Distance (transform.position, Jogador.transform.position) > distanciaParaAbrir) {
			PodeAbrir = false;
		}
		//CHECANDO SE ESTA TRANCADA OU NAO... SE NAO ESTIVER, PODE ABRIR

		if((CustomInput.gatilhoJoystick() ||Input.GetKeyDown ("e"))&& PodeAbrir == true && BauAberto == false){
			GetComponent<Animator>().SetBool ("Abrir",true);
			GetComponent<AudioSource>().PlayOneShot(Abrindo);
			BauAberto = true;
		}
	}
}