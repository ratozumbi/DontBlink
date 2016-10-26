using UnityEngine;
using System.Collections;

public class SobePortao : MonoBehaviour {

	public bool ativar = false;
	private bool once = true;
	// Update is called once per frame
	void Update () {
	
		if (ativar) {
			if (once) {
				once = false;
				GetComponent<AudioSource>().PlayOneShot (GetComponent<AudioSource>().clip);
			}

			transform.Translate(new Vector3(0,1,0) *Time.smoothDeltaTime);
			if (transform.position.y > 1f)
				Destroy (this.gameObject);
		}
	}
}
