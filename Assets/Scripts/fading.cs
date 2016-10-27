using UnityEngine;
using System.Collections;

public class fading : MonoBehaviour {
	public Texture2D fadeOutTexture;
	float fadeSpeed = 0.4f;
	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadDir = -1;

	void OnGUI(){
		alpha += fadDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	public float BeginFade (int direction){
		fadDir = direction;
		return(fadeSpeed);
	}

	void OnLevelWasLoaded(){
		BeginFade(-1);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
