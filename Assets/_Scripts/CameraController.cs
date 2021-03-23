using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    public JoystickOption joystickOption;
    public Joystick leftJoystick;
    public Joystick rightJoystick;
    private Joystick camJoystick;

    private bool doMovement = true;

    [Header("Movement Speeds")]
    public float movingSpeed = 10.0f;
    public float perspectiveZoomSpeed = 0.2f;
    public float orthoZoomSpeed = 0.2f;

    //public float borderThickness = 10.0f;

    //public float scrollSpeed = 5.0f;
    //public float sensitivity = 100.0f;

    //public float minY = 10.0f;
    //public float maxY = 80.0f;

    [Header("Boundaries")]
    public float minX = -65.0f;
    public float maxX = 0.0f;

    public float minZ = -70.0f;
    public float maxZ = -10.0f;

    public float fieldOfViewMin = 0.1f;
    public float fieldOfViewMax = 90.0f;

    private Camera mainCam;

    private void Start()
    {
        mainCam = GetComponent<Camera>();
        camJoystick = (joystickOption.joystick == JoyStick.LEFT ) ? leftJoystick : rightJoystick;
        camJoystick.gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.M))
        {
            doMovement = !doMovement;
        }

        if (!doMovement) return;



        /*------------------------------------------- KEYBOARD INPUT ---------------------------------------------*/

        //if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - borderThickness)
        //{
        //    transform.Translate(new Vector3(1.0f, 0.0f, 1.0f) * movingSpeed * Time.deltaTime, Space.World);
        //}

        //if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= borderThickness)
        //{
        //    transform.Translate(new Vector3(-1.0f, 0.0f, -1.0f) * movingSpeed * Time.deltaTime, Space.World);
        //}

        //if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= borderThickness)
        //{
        //    transform.Translate(new Vector3(-1.0f, 0.0f, 1.0f) * movingSpeed * Time.deltaTime, Space.World);
        //}

        //if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - borderThickness)
        //{
        //    transform.Translate(new Vector3(1.0f, 0.0f, -1.0f) * movingSpeed * Time.deltaTime, Space.World);
        //} 


        //float scroll = Input.GetAxis("Mouse ScrollWheel");

        //if(scroll != 0)
        //{
        //    Vector3 pos = transform.position;

        //    pos.y -= scroll * sensitivity * scrollSpeed * Time.deltaTime;
        //    pos.x += scroll * sensitivity * scrollSpeed * Time.deltaTime;
        //    pos.z += scroll * sensitivity * scrollSpeed * Time.deltaTime;

        //    pos.y = Mathf.Clamp(pos.y, minY, maxY);
        //    pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //    pos.z = Mathf.Clamp(pos.z, minZ, maxZ);


        //    transform.position = pos;
        //}

        /*-----------------------------------------------------------------------------------------------------*/


        /*------------------------------------------- TOUCH INPUT ---------------------------------------------*/

        if (camJoystick.Vertical > 0)
        {
            transform.Translate(new Vector3(1.0f, 0.0f, 1.0f) * movingSpeed * Time.deltaTime, Space.World);
        }

        if (camJoystick.Vertical < 0)
        {
            transform.Translate(new Vector3(-1.0f, 0.0f, -1.0f) * movingSpeed * Time.deltaTime, Space.World);
        }

        if (camJoystick.Horizontal < 0)
        {
            transform.Translate(new Vector3(-1.0f, 0.0f, 1.0f) * movingSpeed * Time.deltaTime, Space.World);
        }

        if (camJoystick.Horizontal > 0)
        {
            transform.Translate(new Vector3(1.0f, 0.0f, -1.0f) * movingSpeed * Time.deltaTime, Space.World);
        }

        float posX = Mathf.Clamp(transform.position.x, minX, maxX);
        float posZ = Mathf.Clamp(transform.position.z, minZ, maxZ);
        transform.position = new Vector3(posX, transform.position.y, posZ);

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudediff = prevTouchDeltaMag - touchDeltaMag;

            if (mainCam.orthographic)
            {
                mainCam.orthographicSize += deltaMagnitudediff * orthoZoomSpeed;
                mainCam.orthographicSize = Mathf.Max(mainCam.orthographicSize, 0.1f);
            }
            else
            {
                mainCam.fieldOfView += deltaMagnitudediff * perspectiveZoomSpeed;
                mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView, fieldOfViewMin, fieldOfViewMax);
            }    
        }

        /*-----------------------------------------------------------------------------------------------------*/
    }
}
