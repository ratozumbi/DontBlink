using UnityEngine;
using System.Collections;

public class winFPS : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;

	private CharacterController controller;

	public float speed = 5;

	float rotationY = 0F;

	private CharacterController cc = new CharacterController();
	void Update ()
	{
		if (axes == RotationAxes.MouseXAndY) {
			float rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
		} else if (axes == RotationAxes.MouseX) {
			transform.Rotate (0, Input.GetAxis ("Mouse X") * sensitivityX, 0);
		} else {
			rotationY += Input.GetAxis ("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3 (-rotationY, transform.localEulerAngles.y, 0);
		}
			
		Vector3 v3 = transform.forward * Input.GetAxis("Vertical") * speed;
		controller.Move (v3 * Time.deltaTime);	
		v3 = transform.right * Input.GetAxis("Horizontal") * speed;
		controller.Move (v3 * Time.deltaTime);


	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		Rigidbody rgdb = this.GetComponent<Rigidbody>();
		if (rgdb)
			rgdb.freezeRotation = true;


		controller = this.GetComponent<CharacterController>();

	}
	
	

}
