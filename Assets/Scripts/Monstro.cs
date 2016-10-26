﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Monstro : MonoBehaviour {
	
	public bool iluminadoForce = false;
	[HideInInspector]
	public bool iluminado = false;
	private bool podeRodar = false;

	private bool possivelVer = false;
	private GameObject player;

	private Camera camL;
	private Camera camR;

	private NavMeshAgent myAgent;
	private Renderer myRenderer;

	public AudioClip PertoDoPlayer, MorteDoPlayer;

	private float CronometroNextLevel;
	public GameObject Player;

	void Start() {
		camL = GameObject.FindGameObjectWithTag ("camL").GetComponent<Camera> ();
		camR = GameObject.FindGameObjectWithTag ("camR").GetComponent<Camera> ();
		myAgent = GetComponent<NavMeshAgent>();
		myRenderer = GetComponent<Renderer> ();
		player = GameObject.FindWithTag ("Player");
	}


	void Update () {

		Vector3 viewPort = camL.WorldToViewportPoint(myRenderer.bounds.center);
		Ray ray = camL.ViewportPointToRay(viewPort);
		RaycastHit hit;
		if (myRenderer.bounds.IntersectRay (ray)) {
			if (Physics.Raycast (ray, out hit))
			{
				if (hit.transform.tag == "Monstro" || hit.transform.tag == "Vidro") {
					if (myRenderer.bounds.IntersectRay (ray)) {
						possivelVer = true;
					}
				} 
			}		
		}
			
		viewPort = camR.WorldToViewportPoint(myRenderer.bounds.center);
		ray = camR.ViewportPointToRay (viewPort);
		hit = new RaycastHit();
		if (myRenderer.bounds.IntersectRay (ray)) {
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.tag == "Monstro" || hit.transform.tag == "Vidro") {
					if (myRenderer.bounds.IntersectRay (ray)) {
						possivelVer = true;
					}
				} 
			} 
		}

		//calcula a rotacao da estatua
		Vector3 lookPos = player.GetComponent<Transform>().position - transform.position;
		Quaternion viradinhaMalandra = new Quaternion (0, -1, 0, 1);//isso esta aqui pq o look at acha que a lateral da estatua eh pra frente, dai compenso dando uam girada.
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation (lookPos) * viradinhaMalandra;

		if (iluminado && myRenderer.isVisible && possivelVer) {//caso willRender e !possivelVer : vai ser possivel ver a sombra do bixo mexendo. Não sei se é uma feature ou bug.
			myAgent.SetDestination (transform.position);
			myAgent.Stop();
			podeRodar = true;
			gameObject.GetComponent<AudioSource>().Play();
		} else {
			myAgent.Resume();
			if(podeRodar)
				transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 1);
			myAgent.SetDestination (player.transform.position);
		}
		possivelVer = false;
		iluminado = iluminadoForce;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "luz") {
			iluminado = true;
		}
		if (col.gameObject.tag == "Player") {
			//if (!(iluminado || myRenderer.isVisible ||   possivelVer))
			StartCoroutine(ReiniciaLevel());
		}
	}

	IEnumerator ReiniciaLevel(){
		CharacterController cc = Player.GetComponent<CharacterController> ();
		cc.enabled = false;

		GetComponent<AudioSource>().PlayOneShot(MorteDoPlayer);
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		yield break;
	}

}
