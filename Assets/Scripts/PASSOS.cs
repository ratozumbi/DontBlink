using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))] // SCRIPT CRIADO POR Marcos Schultz
[RequireComponent(typeof(CharacterController))] // VISITE WWW.SCHULTZGAMES.COM e WEMAKEAGAME.COM.BR
public class PASSOS : MonoBehaviour {
	public AudioClip Madeira,Grama,Terra,Cimento,Metal,Agua,Pulo,SomPadrao;
	private CharacterController controller;
	private bool Pulou,Esperando,EstaNaAgua;
	private float TempoDeEspera,tempoCorridaENormal = 1;
	public float TempoMadeira = 0.6f,TempoGrama = 0.6f,TempoTerra = 0.6f,TempoCimento = 0.6f,TempoMetal = 0.6f,TempoAgua = 0.6f,TempoPulo = 0.6f,TempoPadrao = 0.6f,Aceleracao = 1.3f;
	//variaveis de movimento da camera
	public GameObject CameraDoPlayer;
	public float intensidadeDoMovimento;
	private Vector3 PosicaoInicialDaCamera;
	private float movimentoDaCamera;
	private bool comecarContagem;
	public bool AtivarMovimento;
	void Start (){
		comecarContagem = false;
		PosicaoInicialDaCamera = CameraDoPlayer.transform.localPosition;
		controller = GetComponent<CharacterController> ();
	}
	void Update (){
		RaycastHit hit;
		if (Pulou == false) {
			if (Physics.Raycast (transform.position, Vector3.down, out hit)) {
				if (hit.collider.gameObject.CompareTag ("MADEIRA")) {
					GetComponent<AudioSource>().clip = Madeira;
				} else if (hit.collider.gameObject.CompareTag ("GRAMA")) {
					GetComponent<AudioSource>().clip = Grama;
				} else if (hit.collider.gameObject.CompareTag ("TERRA")) {
					GetComponent<AudioSource>().clip = Terra;
				} else if (hit.collider.gameObject.CompareTag ("CIMENTO")) {
					GetComponent<AudioSource>().clip = Cimento;
				} else if (hit.collider.gameObject.CompareTag ("METAL")) {
					GetComponent<AudioSource>().clip = Metal;
				} else if (EstaNaAgua == true) {
					GetComponent<AudioSource>().clip = Agua;
				} else {
					GetComponent<AudioSource>().clip = SomPadrao;
				}
			}
			if (controller.isGrounded && controller.velocity.magnitude > 0.2f) {
				if (!GetComponent<AudioSource>().isPlaying) {
					TocarSons ();
					if (comecarContagem == false) {
						movimentoDaCamera += Time.deltaTime;
					}
					if (comecarContagem == true) {
						movimentoDaCamera -= Time.deltaTime;
					}
				}
			}
			if (!controller.isGrounded || controller.velocity.magnitude <= 0.19f) {
				GetComponent<AudioSource>().Stop ();
				CameraDoPlayer.transform.localPosition = Vector3.Lerp (CameraDoPlayer.transform.localPosition, PosicaoInicialDaCamera + PosicaoInicialDaCamera * 0.25f* intensidadeDoMovimento, 10 * Time.deltaTime);  
			}
		}
		if (movimentoDaCamera >= TempoDeEspera) {
			comecarContagem = true;
		}
		if (movimentoDaCamera <= 0) {
			comecarContagem = false;
		}
		if (AtivarMovimento == true) {
			CameraDoPlayer.transform.localPosition = Vector3.Lerp (CameraDoPlayer.transform.localPosition, PosicaoInicialDaCamera + PosicaoInicialDaCamera * movimentoDaCamera * intensidadeDoMovimento, 10 * Time.deltaTime);                                                           
		}
		if (Input.GetKeyDown (KeyCode.Space) && Pulou == false) {
			Pulou = true;
			GetComponent<AudioSource>().clip = Pulo;
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().Play ();
			} else if (GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().Stop ();
				GetComponent<AudioSource>().Play ();
			}
		}
		if (Esperando == true) { 
			TempoDeEspera -= Time.deltaTime;
		}
		if (TempoDeEspera <= 0) {
			Esperando = false;
		}
		if (Input.GetKey (KeyCode.LeftShift)) {
			tempoCorridaENormal = 1 / Aceleracao;
		} else {
			tempoCorridaENormal = 1;
		}
	}
	void OnControllerColliderHit (ControllerColliderHit hit){
		Pulou = false;
	}
	void OnTriggerEnter(Collider Other){
		if(Other.gameObject.CompareTag ("AGUA")){
			EstaNaAgua = true;
		}
	}
	void OnTriggerExit(Collider Other){
		if(Other.gameObject.CompareTag ("AGUA")){
			EstaNaAgua = false;
		}
	}
	void TocarSons (){
		if (Esperando == false) {
			GetComponent<AudioSource>().Stop ();
			if (GetComponent<AudioSource>().clip == Madeira) {
				TempoDeEspera = TempoMadeira * tempoCorridaENormal;
				Esperando = true;
				GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
			}
			if (GetComponent<AudioSource>().clip == Grama) {
				TempoDeEspera = TempoGrama * tempoCorridaENormal;
				Esperando = true;
				GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
			}
			if (GetComponent<AudioSource>().clip == Terra) {
				TempoDeEspera = TempoTerra * tempoCorridaENormal;
				Esperando = true;
				GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
			}
			if (GetComponent<AudioSource>().clip == Cimento) {
				TempoDeEspera = TempoCimento * tempoCorridaENormal;
				Esperando = true;
				GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
			}
			if (GetComponent<AudioSource>().clip == Metal) {
				TempoDeEspera = TempoMetal * tempoCorridaENormal;
				Esperando = true;
				GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
			}
			if (GetComponent<AudioSource>().clip == Agua) {
				TempoDeEspera = TempoAgua * tempoCorridaENormal;
				Esperando = true;
				GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
			}
			if (GetComponent<AudioSource>().clip == Pulo) {
				TempoDeEspera = TempoPulo * tempoCorridaENormal;
				Esperando = true;
				GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
			}
			if (GetComponent<AudioSource>().clip == SomPadrao) {
				TempoDeEspera = TempoPadrao * tempoCorridaENormal;
				Esperando = true;
				GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
			}
		}
	}
}