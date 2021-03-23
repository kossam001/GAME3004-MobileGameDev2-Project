using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SwitchHandler : MonoBehaviour
{
    private float localPosX;
    private int switchState;
    public GameObject switchBtn;
    private RawImage switchImage;

    public JoystickOption joystickOption;

    private void Start()
    {
        switchImage = switchBtn.gameObject.GetComponent<RawImage>();

        switchState = (int)joystickOption.joystick;

        switchBtn.transform.localPosition = (switchState == (int)JoyStick.LEFT)
            ? new Vector3(-40.0f, switchBtn.transform.localPosition.y, switchBtn.transform.localPosition.z)
            : new Vector3(40.0f, switchBtn.transform.localPosition.y, switchBtn.transform.localPosition.z);

        switchImage.color = (switchState == (int)JoyStick.LEFT) ? Color.green : Color.red;
    }

    public void OnSwitchButtonClicked()
    {
        switchBtn.transform.DOLocalMoveX(-switchBtn.transform.localPosition.x, 0.2f);
        switchState = Math.Sign(-switchBtn.transform.localPosition.x);
        //Debug.Log(switchState);

        joystickOption.joystick = (switchState == (int)JoyStick.LEFT) ? JoyStick.LEFT : JoyStick.RIGHT;

        switchImage.color = (switchState == (int)JoyStick.LEFT) ? Color.green : Color.red;
    }
}
