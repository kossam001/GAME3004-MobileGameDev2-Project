using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;

    public float movingSpeed = 30.0f;
    public float borderThickness = 10.0f;

    public float scrollSpeed = 5.0f;
    public float sensitivity = 1000.0f;

    public float minY = 10.0f;
    public float maxY = 80.0f;

    public float minX = -62.0f;
    public float maxX = 8.0f;

    public float minZ = -67.0f;
    public float maxZ = 3.0f;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown (KeyCode.M))
        {
            doMovement = !doMovement;
        }

        if (!doMovement) return;


        if(Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - borderThickness)
        {
            transform.Translate(new Vector3(1.0f, 0.0f, 1.0f) * movingSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= borderThickness)
        {
            transform.Translate(new Vector3(-1.0f, 0.0f, -1.0f) * movingSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= borderThickness)
        {
            transform.Translate(new Vector3(-1.0f, 0.0f, 1.0f) * movingSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - borderThickness)
        {
            transform.Translate(new Vector3(1.0f, 0.0f, -1.0f) * movingSpeed * Time.deltaTime, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if(scroll != 0)
        {
            Vector3 pos = transform.position;

            pos.y -= scroll * sensitivity * scrollSpeed * Time.deltaTime;
            pos.x += scroll * sensitivity * scrollSpeed * Time.deltaTime;
            pos.z += scroll * sensitivity * scrollSpeed * Time.deltaTime;

            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.z = Mathf.Clamp(pos.z, minZ, maxZ);


            transform.position = pos;
        }

        
    }
}
