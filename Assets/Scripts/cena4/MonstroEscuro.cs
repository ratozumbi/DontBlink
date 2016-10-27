using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MonstroEscuro : MonoBehaviour {

	public float timerEstatico = 5;
	private float contadorEstatico = 10;
	[HideInInspector]
	public bool iluminadoForce = true;
	[HideInInspector]
	public bool ignoreVisibleCondition = true;
	[HideInInspector]
	public bool iluminado = false;

	private bool podeRodar = false;
	private bool anda = true;


	private bool morrendo = false;
	public bool podeMorrer = false;

	private bool possivelVer = false;
	private GameObject player;
	private GameObject playerTocha;
	private GameObject meuGargula;
	private Light luzPlayer;

	private Light myLight;

	private Camera camL;
	private Camera camR;

	private float triggerDistance;

	private AudioClip[] listAudio = new AudioClip[10];
	private AudioSource myAudioSource;

	private NavMeshAgent myAgent;
	private Renderer myRenderer;
	void Start() {
		camL = GameObject.FindGameObjectWithTag ("camL").GetComponent<Camera> ();
		camR = GameObject.FindGameObjectWithTag ("camR").GetComponent<Camera> ();
		myAgent = GetComponent<NavMeshAgent>();
		myRenderer = GetComponent<Renderer> ();
		player = GameObject.FindWithTag ("Player");
		playerTocha = GameObject.FindWithTag ("TochaPlayer");
		meuGargula = GameObject.Find("gargula");
		luzPlayer = playerTocha.GetComponent<Light> ();
		triggerDistance = luzPlayer.range;
		myLight = GetComponent<Light> ();

		myAudioSource = GetComponent<AudioSource> ();
		listAudio[0] = (AudioClip)Resources.Load("risadaFina");
		listAudio[1] = (AudioClip)Resources.Load("risadaGrossa");
		listAudio [2] = (AudioClip)Resources.Load ("risadaLonga");
		listAudio[3] = (AudioClip)Resources.Load("risadaMuitoDistorcida1");
		listAudio[4] = (AudioClip)Resources.Load("risadaMuitoDistorcida2");

	}


	void Update () {
		if (morrendo) {
			meuGargula.transform.Translate(new Vector3(0,-1,0) *Time.smoothDeltaTime);
			return;
		}
		
		//testa se tem algo no meio do caminho impedidno a visao
		//rever esta logica, em um caso que o raio atinge um vidro, mas ainda tem uma parede até o player ele devia testar como FLASE. Mas hoje acontece como TRUE
		Vector3 viewPort = camL.WorldToViewportPoint(myRenderer.bounds.center);
		Ray ray = camL.ViewportPointToRay(viewPort);
		RaycastHit hit;
		if (myRenderer.bounds.IntersectRay (ray)) {
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Ignore Raycast")))
			{
				if (hit.transform.tag == "MonstroEscuro" || hit.transform.tag == "Monstro" ) {
					if (myRenderer.bounds.IntersectRay (ray)) {
						possivelVer = true;
						goto skipEye;
					}
				} 
			}		
		}
		viewPort = camR.WorldToViewportPoint(myRenderer.bounds.center);
		ray = camR.ViewportPointToRay (viewPort);
		hit = new RaycastHit();
		if (myRenderer.bounds.IntersectRay (ray)) {
			if (Physics.Raycast (ray, out hit, Mathf.Infinity, ~LayerMask.NameToLayer("Ignore Raycast")))
			{
				if (hit.transform.tag == "MonstroEscuro" || hit.transform.tag == "Monstro") {
					if (myRenderer.bounds.IntersectRay (ray)) {
						possivelVer = true;
					}
				} 
			} 
		}
		skipEye:

		//controle para dar tempo da estatua ficar parada quando pega o player
		if (contadorEstatico > 0) {
			contadorEstatico -= Time.deltaTime;
			iluminadoForce = true;
			iluminado = true;
			ignoreVisibleCondition = true;

			myLight.intensity = (contadorEstatico * 1.7f);
		} else {
			myLight.intensity = 0;
			myAudioSource.Stop ();
			//update para ver se o player esta perto
			if (Vector3.Distance (transform.position, player.transform.position) <= triggerDistance) {
				luzPlayer.range --;
				triggerDistance = luzPlayer.range;
				if (triggerDistance < 2)
					MataPlayer ();
				contadorEstatico = timerEstatico;
				int rand = Random.Range (0, listAudio.Length);
				myAudioSource.PlayOneShot(listAudio [rand],1f);
			} else 
			{
				iluminadoForce = false;
				ignoreVisibleCondition = false;												

			}
		}

		if (iluminado && (myRenderer.isVisible && possivelVer) || ignoreVisibleCondition) {
			anda = false;
		} else
			anda = true;
		
		if (anda)
			Move ();
		else
			Para ();

		anda = false;
		possivelVer = false;
		iluminado = iluminadoForce;
	}


	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.tag == "Player") {
			if (anda)
				MataPlayer ();
		} 
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "luz") {
			iluminado = true;
		} 
		if (col.gameObject.tag == "Player") {
			if (anda)
				MataPlayer ();
		} 
		if (col.gameObject.tag == "Finish" /*&& podeMorrer*/) {
			myAudioSource.PlayOneShot (listAudio [4]);
			morrendo = true;
			Destroy (this.gameObject, 5);
			GameObject fire = Instantiate (Resources.Load ("Prefabs/fx_fire_g", typeof(GameObject)), transform) as GameObject; 
		} 
		{
			Debug.Log ("monstro colider" + col.gameObject.name);
		}

	}

	void Move(){
		//calcula a rotacao da estatua
		Vector3 lookPos = player.GetComponent<Transform>().position - transform.position;
		Quaternion viradinhaMalandra = new Quaternion (0, -1, 0, 1);//isso esta aqui pq o look at acha que a lateral da estatua eh pra frente, dai compenso dando uma girada.
		lookPos.y = 0;
		Quaternion rotation = Quaternion.LookRotation (lookPos) * viradinhaMalandra;

		myAgent.Resume ();
		if (podeRodar)
			transform.rotation = Quaternion.Slerp (transform.rotation, rotation, 1);
		myAgent.SetDestination (player.transform.position);
	}

	void Para(){
		myAgent.SetDestination (transform.position);
		myAgent.Stop ();
		podeRodar = true;
	}
	void MataPlayer(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);	
	}


}
