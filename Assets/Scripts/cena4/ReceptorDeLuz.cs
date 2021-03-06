﻿using UnityEngine;
using System.Collections;

public class ReceptorDeLuz : MonoBehaviour {

	private Light playerLuz;
	private Light minhaLuz;

	private GameObject monstro;
	private MonstroEscuro scriptMonstro;
	private bool once = true;

	public int ativacao = 0;

	void Start () {
		GameObject tochaPlayer;
		tochaPlayer = GameObject.FindGameObjectWithTag ("TochaPlayer");
		playerLuz = tochaPlayer.GetComponent<Light> ();
		minhaLuz = GetComponent <Light> ();
	}


	public void receiveCor(){
		if (minhaLuz.color == playerLuz.color && once) {
			minhaLuz.range = 2.5f;
			gameObject.AddComponent<luzPoint>();
			once = false;

			switch (ativacao) {
			case 1:
				GameObject.Find ("Portao1").GetComponent<SobePortao> ().ativar = true;
				monstro = GameObject.FindGameObjectWithTag ("MonstroEscuro");
				monstro.AddComponent<MonstroEscuro> ();
				break;
			case 2:
				GameObject.Find ("Portao2").GetComponent<SobePortao> ().ativar = true;
				break;
			case 3:
				GameObject.Find ("Portao3").GetComponent<SobePortao> ().ativar = true;
				break;
			case 4:
				GameObject.Find ("Portao4").GetComponent<SobePortao> ().ativar = true;
				break;
			case 5:
				GameObject.FindWithTag ("Finish").GetComponent<MataODiabo> ().podeMatar = true;
				break;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{ 

		//#if (UNITY_EDITOR || UNITY_STANDALONE_WIN)

		receiveCor();
		//#endif
	}

	void OnTriggerStay(Collider other)
	{
		if (CustomInput.gatilhoJoystick())
		{
			receiveCor();
		}
	}
}
