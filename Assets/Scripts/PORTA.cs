using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class PORTA : MonoBehaviour {
	public bool EstaTrancada,PrecisaDeChave,MudaDeCena = true;
	private AudioClip PortaNormal,PortaTrancada,SomDeChave;
	public float distanciaParaAbrir = 3;
	public Font Fonte;
	public float VelocidadeDeGiro = 60;
	public int IDdaPorta;
	public static List<int> ListaDeIDs = new List<int>(); // LISTA DE CHAVES QUE O PLAYER CONTEM
	private bool MovimentarPorta, EstaAberta,PodeAbrir,AvisoTrancada,temAChave;
	private float CronometroDoAviso,CronometroMovimento,CronometroNextLevel;
	private float RotacaoFechada,RotacaoAberta;
	private GameObject Jogador;
	void Start (){
		EstaAberta = false;
		AvisoTrancada = false;
		temAChave = false;
		RotacaoFechada = transform.eulerAngles.y;
		RotacaoAberta = transform.eulerAngles.y + 90;
		if (RotacaoAberta > 360) {
			RotacaoAberta = transform.eulerAngles.y + 90 -360;
		}
		Jogador = GameObject.FindWithTag ("Player");
		if (PrecisaDeChave == true) {
			EstaTrancada = true;
		}
		PortaNormal = (AudioClip)Resources.Load ("porta_abrindo");
		PortaTrancada = (AudioClip)Resources.Load ("porta_trancada");
		SomDeChave = (AudioClip)Resources.Load("porta_destrancando");
	}
	void Update (){
		// CHECAHDO SE ESTA PERTO OU NAO
		if (Vector3.Distance (transform.position, Jogador.transform.position) <= distanciaParaAbrir) {
			PodeAbrir = true;
		} else if (Vector3.Distance (transform.position, Jogador.transform.position) > distanciaParaAbrir) {
			PodeAbrir = false;
		}
		//CHECANDO SE ESTA TRANCADA OU NAO... SE NAO ESTIVER, PODE ABRIR
		if (EstaTrancada == false) {
			if((CustomInput.gatilhoJoystick() ||Input.GetKeyDown ("e")) && MovimentarPorta == true && PodeAbrir == true){
				CronometroMovimento = 0;
				EstaAberta = !EstaAberta;
				GetComponent<AudioSource>().Stop ();
				GetComponent<AudioSource>().PlayOneShot(PortaNormal);
			}
			else if((CustomInput.gatilhoJoystick() ||Input.GetKeyDown ("e")) && PodeAbrir == true && MovimentarPorta == false){
				GetComponent<AudioSource>().PlayOneShot(PortaNormal);
				MovimentarPorta = true;
			}
		}
		// SE A PORTA ESTIVER TRANCADA 
		if ((CustomInput.gatilhoJoystick() ||Input.GetKeyDown ("e")) && PodeAbrir == true && EstaTrancada == true) {
			//CHECA SE O PALYER TEM A CHAVE OU NAO
			for(int x = 0; x < ListaDeIDs.Count; x++){
				if(IDdaPorta == ListaDeIDs[x]){
					temAChave = true;
				}else{
					temAChave = false;
				}
			}
			// SE O PALYER TEM A CHAVE
			if(temAChave == true && PrecisaDeChave == true){
				EstaTrancada = false;
				if(!GetComponent<AudioSource>().isPlaying){
					GetComponent<AudioSource>().PlayOneShot(SomDeChave);
				}
			}
			// SE O PALYER NAO TEM A CHAVE
			else {
				AvisoTrancada = true;
				if(!GetComponent<AudioSource>().isPlaying){
					GetComponent<AudioSource>().PlayOneShot(PortaTrancada);
				}
			}
		}
		// CRONOMETRO DO AVISO DA PORTA TRANCADA
		if (AvisoTrancada == true) {
			CronometroDoAviso += Time.deltaTime;
		}
		if (CronometroDoAviso >= 3) {
			AvisoTrancada = false;
			CronometroDoAviso = 0;
		}
		// CRONOMETRO DO MOVIMENTO DA PORTA
		if (MovimentarPorta == true) {
			CronometroMovimento += Time.deltaTime;
		}
		if(CronometroMovimento >= 2 + 75/VelocidadeDeGiro){
			MovimentarPorta = false;
			CronometroMovimento = 0;
			EstaAberta = !EstaAberta;
		}
	}
	void FixedUpdate (){
		// MOVIMENTO DE ABRIR A PORTA
		if (MovimentarPorta == true && EstaAberta == false) {
			Vector3 rotacaoFinal = new Vector3(0,RotacaoAberta,0);
			transform.eulerAngles = Vector3.Lerp (transform.eulerAngles,rotacaoFinal,Time.deltaTime*(VelocidadeDeGiro/50));

			CronometroNextLevel += Time.deltaTime;

			if (CronometroNextLevel >= 2) {
				CronometroNextLevel = 0;

				int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
				if ((SceneManager.sceneCountInBuildSettings > nextSceneIndex) && MudaDeCena)
				{
					SceneManager.LoadScene(nextSceneIndex);
				}

			}
		}
		// MOVIMENTO DE FECHAR A PORTA
		else if (MovimentarPorta == true && EstaAberta == true) {
			Vector3 rotacaoFinal = new Vector3(0,RotacaoFechada,0);
			transform.eulerAngles = Vector3.Lerp (transform.eulerAngles,rotacaoFinal,Time.deltaTime*(VelocidadeDeGiro/50));
		}
	}
	void OnGUI (){
		// AVISO SOBRE PORTA TRANCADA
		GUI.skin.font = Fonte;
		GUI.skin.label.fontSize = Screen.height / 20;
		if (AvisoTrancada == true) {
			if(PrecisaDeChave == true){
				GUI.Label(new Rect(Screen.width/2-Screen.width/5,Screen.height/2-Screen.height/16,Screen.width/2.5f,Screen.height/8),""); 
			}
			else if(PrecisaDeChave == false){
				GUI.Label(new Rect(Screen.width/2-Screen.width/5,Screen.height/2-Screen.height/16,Screen.width/2.5f,Screen.height/8),""); 
			}
		}
	}
}