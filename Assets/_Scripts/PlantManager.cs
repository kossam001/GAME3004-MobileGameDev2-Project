using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlantManager : MonoBehaviour
{
    public static bool planting = false;
    public Camera camera;
    public GameObject plantPrefab;
    public float gridSizeX = 2.5f;
    public float gridSizeY = 2.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (planting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                LayerMask mask = LayerMask.GetMask("PlantClick");

                if (Physics.Raycast(ray, out hit, 1000, mask))
                {
                    Transform objectHit = hit.transform;

                    float pointX = (int) (hit.point.x / gridSizeX);
                    pointX = (pointX ) * gridSizeX;
                    //pointX += gridSizeX / 2;
                    float pointZ = (int) (hit.point.z / gridSizeY);
                    pointZ = (pointZ) * gridSizeY;
                    pointZ += gridSizeY / 2;


                    Vector3 spawnPoint = new Vector3(pointX, hit.point.y, pointZ);

                    Instantiate(plantPrefab, spawnPoint, Quaternion.identity);
                }
            }
        }
    }

    public static void PlantingOn()
    {
        planting = true;

    }

    public static void PlantingOff()
    {
        planting = false;
    }
}
