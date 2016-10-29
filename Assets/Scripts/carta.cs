using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class carta : MonoBehaviour {

	public Texture TexturaCarta;
	public float DistDaCarta=2;
	public Font fonte;
	public AudioClip audioFolha;
	private GameObject jogador;
	public GameObject papel, estatua;
	private float distancia;
	private bool mostrarCarta;


	void Start () {
		jogador = GameObject.FindWithTag ("Player");
		mostrarCarta = true;
	}

	// Update is called once per frame
	void Update () {
		distancia = Vector3.Distance (transform.position, jogador.transform.position);
		if (distancia <= DistDaCarta && (CustomInput.gatilhoJoystick() || Input.GetKeyDown ("e"))) {
			if (mostrarCarta == true) {
				GetComponent<AudioSource> ().PlayOneShot (audioFolha);

				estatua.GetComponent<Monstro> ().enabled = false;
				estatua.GetComponent<AudioSource> ().enabled = false;

				papel.SetActive (true);
				GetComponent<MeshRenderer> ().enabled = false;
				mostrarCarta = false;
				CharacterController cc = jogador.GetComponent<CharacterController> ();
				cc.enabled = false;
			} else if (mostrarCarta == false) {
				papel.SetActive (false);
				GetComponent<MeshRenderer> ().enabled = true;

				estatua.GetComponent<Monstro> ().enabled = true;
				estatua.GetComponent<AudioSource> ().enabled = true;

				mostrarCarta = true;
				CharacterController cc = jogador.GetComponent<CharacterController> ();
				cc.enabled = true;
			}
		}
	}
}











