using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grip : MonoBehaviour
{

    public GameObject selectedObject;

    public List<GameObject> oldObjects = new List<GameObject>();


   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Cube")) // ADD TAGS HERE OR SMTHG IT WORKS NOW YOU JUST NEED TO ASSIGN TAGS
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                  
                        foreach(GameObject oldObject in oldObjects)  //Turns off scripts on old object
                        {
                            oldObject.transform.GetChild(1).gameObject.GetComponent<Rotate>().enabled = false;
                            oldObject.transform.GetChild(2).gameObject.GetComponent<Scale>().enabled = false;
                            oldObject.transform.GetChild(3).gameObject.GetComponent<Translate>().enabled = false;
                            oldObjects.Remove(oldObject);


                        }
                   


                    //savedPos = selectedObject.transform.position;
                }

            }

            else //Deslect
            {
                oldObjects.Add(selectedObject);
                
                selectedObject = null;
            }
            
         

        }

        if (selectedObject != null) //When selected do action. (Renables scripts)
        {


            selectedObject.transform.GetChild(1).gameObject.GetComponent<Rotate>().enabled = true;
            selectedObject.transform.GetChild(2).gameObject.GetComponent<Scale>().enabled = true;
            selectedObject.transform.GetChild(3).gameObject.GetComponent<Translate>().enabled = true;


        }

     


    }



    private RaycastHit CastRay() //Creates ray from mouse pos and camera clip.
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        RaycastHit hit;

        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;


    }



 
}
