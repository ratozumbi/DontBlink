using UnityEngine;
using System.Collections;

public class CustomInput : MonoBehaviour {

	public static bool gatilhoJoystick(){
		return Input.GetKeyDown (KeyCode.JoystickButton0) |
			Input.GetKeyDown (KeyCode.JoystickButton1) |
			Input.GetKeyDown (KeyCode.JoystickButton2) |
			Input.GetKeyDown (KeyCode.JoystickButton3) |
			Input.GetKeyDown (KeyCode.JoystickButton4)|
			Input.GetKeyDown (KeyCode.JoystickButton5);
	}
}
