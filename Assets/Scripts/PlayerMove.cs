using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	private CharacterController controller;

	public float speed = 3;
	public float jumpSpeed = 8;
	private Vector3 gravidade = Vector3.zero;

	private CardboardHead head = null;

	void Update ()
	{

		Vector3 direction = new Vector3(head.transform.forward.x, 0, head.transform.forward.z).normalized * speed * Time.deltaTime * Input.GetAxis("Vertical") ;
		Quaternion rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
		controller.Move(rotation * direction);

		direction = new Vector3(head.transform.right.x, 0, head.transform.right.z).normalized * speed * Time.deltaTime * Input.GetAxis("Horizontal") ;
		rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
		controller.Move(rotation * direction);

		//gravidade

		if (controller.isGrounded) {
			print ("g");
//			gravidade.y = -9.8f;
//			if (Input.GetKeyDown (KeyCode.Space)) { // unless it jumps:
//				gravidade.y = 18;
//			}
		}
		else {
			print ("n");

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
