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

	private UnityEngine.AI.NavMeshAgent myAgent;
	private Renderer myRenderer;
	void Start() {
		camL = GameObject.FindGameObjectWithTag ("camL").GetComponent<Camera> ();
		camR = GameObject.FindGameObjectWithTag ("camR").GetComponent<Camera> ();
		myAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
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
		listAudio[5] = (AudioClip)Resources.Load("morte");

	}

	public void MataComFogo(){
		morrendo = true;
		myAudioSource.PlayOneShot (listAudio [4]);
		contadorEstatico = 8;
		myLight.intensity = 8;
		GameObject fire = Instantiate (Resources.Load ("Prefabs/fx_fire_g", typeof(GameObject)), transform,true) as GameObject; 
		fire.transform.localPosition = new Vector3 (0, -0.23f, 0);
		fire.transform.localScale = new Vector3 (3, 3, 3);

	}

	void EndGame(){
		Destroy (this.gameObject);
		Application.Quit ();

	}

	void Update () {
		if (morrendo) {
			Para ();
			contadorEstatico -= Time.deltaTime;
			if (contadorEstatico <= 0) {
				EndGame ();
			}
			meuGargula.transform.Translate(new Vector3(0,0,-1) *Time.smoothDeltaTime/3f);
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
				if (triggerDistance < 3)
					MataPlayer ();
				contadorEstatico = timerEstatico;
				int rand = Random.Range (0, 5);
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
		StartCoroutine(ReiniciaLevel());
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);	
	}

	IEnumerator ReiniciaLevel(){
		CharacterController cc = player.GetComponent<CharacterController> ();
		cc.enabled = false;

		float fadetime = GameObject.Find ("_GM").GetComponent<fading> ().BeginFade (1);;

		GetComponent<AudioSource>().PlayOneShot(listAudio[5]);
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		yield break;
	}


}
