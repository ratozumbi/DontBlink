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
		#if (!UNITY_STANDALONE_WIN && !UNITY_EDITOR)
		if(CustomInput.ControlMode == 0){ 
			CustomInput.ControlMode = 0;
			return;
		}
		Vector3 direction = new Vector3(head.transform.forward.x, 0, head.transform.forward.z).normalized * speed * Time.deltaTime * Input.GetAxis(CustomInput.vertical) * CustomInput.invertY;
		Quaternion rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
		controller.Move(rotation * direction);

		direction = new Vector3(head.transform.right.x, 0, head.transform.right.z).normalized * speed * Time.deltaTime * Input.GetAxis(CustomInput.horizontal) * CustomInput.invertX;
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
		gravidade = Physics.gravity;

		controller.Move(gravidade * Time.deltaTime);
		#endif

	}
		
	void Start ()
	{
		head = GameObject.FindObjectOfType<CardboardHead>();//procura o primeiro, mas so deve existir 1 mesmo
		controller = this.GetComponent<CharacterController>();
	
	}
	
	

}
