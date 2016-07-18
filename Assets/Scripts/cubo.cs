using UnityEngine;
using System.Collections;

public class cubo : MonoBehaviour {

	private bool willRender = false;
	private bool possivelVer = false;
	private GameObject player;

	private Camera camL;
	private Camera camR;

	private NavMeshAgent myAgent;
	void Start() {
		camL = GameObject.FindGameObjectWithTag ("camL").GetComponent<Camera> ();
		camR = GameObject.FindGameObjectWithTag ("camR").GetComponent<Camera> ();
		myAgent = GetComponent<NavMeshAgent>();
		player = GameObject.FindWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {

		Vector3 viewPort = camL.WorldToViewportPoint(transform.position);
		print ("view L" + viewPort.ToString ());
		Ray ray = camL.ViewportPointToRay(viewPort);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.tag == "Monstro") {
				possivelVer = true;
			} 
		} 
			
		viewPort = camR.WorldToViewportPoint(transform.position);
		print ("view R" + viewPort.ToString ());
		ray = camR.ViewportPointToRay (viewPort);
		hit = new RaycastHit();
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.tag == "Monstro") {
				possivelVer = true;
			} 
		} 

		Debug.DrawLine (transform.position, player.transform.position);

		if (willRender && possivelVer) {//caso willRender e !possivelVer : vai ser possivel ver a sombra do bixo mexendo
			myAgent.SetDestination (transform.position);
			myAgent.Stop();
		} else {
			myAgent.Resume();
			myAgent.SetDestination (player.transform.position);
		}

		possivelVer = false;
		willRender = false;
	}

	void OnWillRenderObject(){
		willRender = true;
	}
}
