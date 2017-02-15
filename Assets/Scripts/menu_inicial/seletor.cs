using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class seletor : MonoBehaviour {
	private Camera myCameraL;
	private Camera myCameraR;
	private bool mirando = false;
	private float timeSelect;

	private Vector3 pMiraL;
	private Vector3 pMiraR;

	void Start () {
		myCameraL = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		myCameraR = GameObject.FindWithTag("camR").GetComponent<Camera>();
		timeSelect = 0;
	}
	
	// Update is called once per frame
	void Update () {

		pMiraL = myCameraL.ViewportToWorldPoint(new Vector3(0.6f, 0.6f, myCameraL.nearClipPlane));
		Ray rayMiraL = myCameraL.ScreenPointToRay (pMiraL);

		pMiraR = myCameraL.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, myCameraR.nearClipPlane));
		Ray rayMiraR = myCameraL.ScreenPointToRay (pMiraR);

		RaycastHit hit;
		if (Physics.Raycast (rayMiraL, out hit) || Physics.Raycast (rayMiraR, out hit)) {
			Debug.DrawLine (pMiraL, hit.point,Color.red);
			Debug.DrawLine (pMiraR, hit.point,Color.red);
			Debug.Log (hit.collider.name);
			mirando = true;
			timeSelect += Time.deltaTime;
			if (timeSelect > 2) {
				if (hit.collider.name == "exit") {
					Application.Quit();
				}
				if (hit.collider.name == "start") {
					SceneManager.LoadScene(1);
				}
			}

		} else {
			mirando = false;
		}

	}
		


}
