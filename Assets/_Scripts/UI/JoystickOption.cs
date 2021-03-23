using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JoystickOption", menuName = "Control")]
public class JoystickOption : ScriptableObject
{
    public JoyStick joystick;
}

public enum JoyStick
{
    LEFT = -1,
    RIGHT = 1 
}