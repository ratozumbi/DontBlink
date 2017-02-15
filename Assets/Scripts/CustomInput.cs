using UnityEngine;
using System.Collections;

public class CustomInput : MonoBehaviour {

	private static int mode = 0;

	public static string vertical = "";
	public static string horizontal = "";
	public static float invertY = 1;
	public static float invertX = 1;

	public static int ControlMode {
		get {
			return mode;
		}
		set {
			mode = value;

			switch (mode) {
			case 0:
				autoSetMode ();
				break;
			case 1:
				vertical = "Vertical";
				horizontal = "Horizontal";
				break;
			case 2:
				vertical = "Horizontal";
				horizontal = "Vertical";
				break;
			case 3:
				vertical = "Vertical";
				horizontal = "Horizontal";
				invertX = -1;
				break;
			case 4:
				vertical = "Horizontal";
				horizontal = "Vertical";
				invertY = -1;
				break;
			}
		}
	}

	public static bool gatilhoJoystick(){
		return Input.GetKeyDown (KeyCode.JoystickButton0) |
			Input.GetKeyDown (KeyCode.JoystickButton1) |
			Input.GetKeyDown (KeyCode.JoystickButton2) |
			Input.GetKeyDown (KeyCode.JoystickButton3) |
			Input.GetKeyDown (KeyCode.JoystickButton4)|
			Input.GetKeyDown (KeyCode.JoystickButton5)|
			Input.GetKeyDown (KeyCode.JoystickButton6)|
			Input.GetKeyDown (KeyCode.JoystickButton7)|
			Input.GetKeyDown (KeyCode.JoystickButton8);
	}

	public static bool autoSetMode(){
		if (vertical == "") {
			if (Input.GetAxis ("Vertical") > 0.5f && (Input.GetAxis ("Horizontal") < 0.5f && Input.GetAxis ("Horizontal") > -0.5f)) {
				vertical = "Vertical";
				horizontal = "Horizontal";
				ControlMode = 1;
			} else if (Input.GetAxis ("Horizontal") > 0.5f && (Input.GetAxis ("Vertical") < 0.5f && Input.GetAxis ("Vertical") > -0.5f)) {
				vertical = "Horizontal";
				horizontal = "Vertical";
				ControlMode = 2;
			} else if (Input.GetAxis ("Vertical") < -0.5f && (Input.GetAxis ("Horizontal") < 0.5f && Input.GetAxis ("Horizontal") > -0.5f)) {
				vertical = "Vertical";
				horizontal = "Horizontal";
				invertX= -1;
				ControlMode = 3;
			} else if (Input.GetAxis ("Horizontal") < -0.5f && (Input.GetAxis ("Vertical") < 0.5f && Input.GetAxis ("Vertical") > -0.5f)) {
				vertical = "Horizontal";
				horizontal = "Vertical";
				invertY = -1;
				ControlMode = 4;
			}
			return false;
		} else {
			return true;
		}
	}
		
}
