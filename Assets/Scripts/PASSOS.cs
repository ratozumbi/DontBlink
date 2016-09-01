using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))] 
[RequireComponent(typeof(CharacterController))] 
public class PASSOS : MonoBehaviour {
	public AudioClip Madeira;
	private CharacterController controller;
	private bool Pulou,Esperando,EstaNaAgua;
	private float TempoDeEspera,tempoCorridaENormal = 1;
	public float TempoMadeira = 0.6f;

	void Start (){
		controller = GetComponent<CharacterController> ();
	}

	void Update (){
		GetComponent<AudioSource>().clip = Madeira;
		GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
		}
	}