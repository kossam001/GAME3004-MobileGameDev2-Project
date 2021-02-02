using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                InteractableObject clickedObject = hit.collider.gameObject.GetComponent<InteractableObject>();
                if (clickedObject != null)
                {
                    clickedObject.Use();
                }
            }
        }
    }
}
