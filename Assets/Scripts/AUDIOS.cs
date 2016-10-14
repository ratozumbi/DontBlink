using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]
public class AUDIOS : MonoBehaviour {
	private BoxCollider[] Colisores;
	public AudioClip Audio;
	void Start () {
		Colisores = gameObject.GetComponents<BoxCollider> ();
		GetComponent<AudioSource>().clip = Audio;
	}
	void OnTriggerEnter (){
		GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
		foreach (BoxCollider BoxColl in Colisores) {
			BoxColl.enabled = false;
		}
		Destroy (gameObject, GetComponent<AudioSource>().clip.length);
	}
}