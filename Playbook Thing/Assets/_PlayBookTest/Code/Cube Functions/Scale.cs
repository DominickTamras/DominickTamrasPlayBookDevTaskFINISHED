using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{


    public GameObject selectedObject;

    public GameObject xScale; //X gimbal

    public GameObject yScale; //Y gimbal

    public GameObject zScale; //Z gimbal

    public Transform cubeScale;

    public Transform savedScale;

 




    private void Awake()
    {
        
        
        Vector3 xposOffSet = xScale.transform.localPosition;

        xposOffSet.x = cubeScale.localScale.x + 0.5f;

        xScale.transform.localPosition = xposOffSet;

        Vector3 yposOffSet = yScale.transform.localPosition;

        yposOffSet.y = cubeScale.localScale.y + 0.5f;

        yScale.transform.localPosition = yposOffSet;

        Vector3 zposOffSet = zScale.transform.localPosition;

        zposOffSet.z = cubeScale.localScale.z + 0.5f;

        zScale.transform.localPosition = zposOffSet;

        Clamps();



    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(selectedObject == null)
            { 
                RaycastHit hit = CastRay();
                
                if(hit.collider != null)
                {
                    if(!hit.collider.CompareTag("Drag"))
                    {
                        return;
                    }
                    selectedObject = hit.collider.gameObject;
                   
                }
               
            }

            else //Deslect
            {
                
                selectedObject = null;
            }

        }

        if(selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPos.x, worldPos.y, worldPos.z);
            Clamps();
            cubeScale.localScale = new Vector3(xScale.transform.localPosition.x - 0.5f, yScale.transform.localPosition.y - 0.5f, zScale.transform.localPosition.z - 0.5f);
            



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

  

    void Clamps() //Clamps gimbal
    {
         //Pos clamp
            Vector3 xpos = xScale.transform.localPosition;

            xpos.y = Mathf.Clamp(xScale.transform.localPosition.y, 0, 0);

            xpos.z = Mathf.Clamp(xScale.transform.localPosition.z, 0,0);

            xScale.transform.localPosition = xpos;


            Vector3 ypos = yScale.transform.localPosition;

            ypos.x = Mathf.Clamp(yScale.transform.localPosition.x, 0, 0);

            ypos.z = Mathf.Clamp(yScale.transform.localPosition.z, 0,0);

            yScale.transform.localPosition = ypos;


            Vector3 zpos = zScale.transform.localPosition;

            zpos.x = Mathf.Clamp(zScale.transform.localPosition.x,0,0);

            zpos.y = Mathf.Clamp(zScale.transform.localPosition.y, 0, 0);

            zScale.transform.localPosition = zpos;


    }



}

