using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	private CharacterController controller;

	public float speed = 3;
	public float jumpSpeed = 8;
	private Vector3 gravidade = Vector3.zero;

	private CardboardHead head = null;

	private string vertical = "";
	private string horizontal = "";
	private float invertY = 1;
	private float invertX = 1;

	void Update ()
	{
		//enquanto o player nao apertar para frente, não se mexe
		//esses ifs serve pra mapear o controle caso esteja invertido
		if (vertical == "") {
			if (Input.GetAxis ("Vertical") > 0.5f && (Input.GetAxis ("Horizontal") < 0.5f && Input.GetAxis ("Horizontal") > -0.5f)) {
				vertical = "Vertical";
				horizontal = "Horizontal";
			} else if (Input.GetAxis ("Horizontal") > 0.5f && (Input.GetAxis ("Vertical") < 0.5f && Input.GetAxis ("Vertical") > -0.5f)) {
				vertical = "Horizontal";
				horizontal = "Vertical";
			} else if (Input.GetAxis ("Vertical") < -0.5f && (Input.GetAxis ("Horizontal") < 0.5f && Input.GetAxis ("Horizontal") > -0.5f)) {
				vertical = "Vertical";
				horizontal = "Horizontal";
				invertX = -1;
			} else if (Input.GetAxis ("Horizontal") < -0.5f && (Input.GetAxis ("Vertical") < 0.5f && Input.GetAxis ("Vertical") > -0.5f)) {
				vertical = "Horizontal";
				horizontal = "Vertical";
				invertY = -1;
			}
			return;
		}

		Vector3 direction = new Vector3(head.transform.forward.x, 0, head.transform.forward.z).normalized * speed * Time.deltaTime * Input.GetAxis(vertical) * invertY;
		Quaternion rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
		controller.Move(rotation * direction);

		direction = new Vector3(head.transform.right.x, 0, head.transform.right.z).normalized * speed * Time.deltaTime * Input.GetAxis(horizontal) * invertX;
		rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
		controller.Move(rotation * direction);
		//tx.text = invertY +" \n "+ invertX; 
		//gravidade

		if (controller.isGrounded) {
			//print ("g");
//			gravidade.y = -9.8f;
//			if (Input.GetKeyDown (KeyCode.Space)) { // unless it jumps:
//				gravidade.y = 18;
//			}
		}
		else {
			//print ("n");

		}
		gravidade = gravidade + Physics.gravity;
		controller.Move(gravidade * Time.deltaTime);


	}
		
	void Start ()
	{
		head = GameObject.FindObjectOfType<CardboardHead>();//procura o primeiro, mas so deve existir 1 mesmo
		controller = this.GetComponent<CharacterController>();
	
	}
	
	

}
