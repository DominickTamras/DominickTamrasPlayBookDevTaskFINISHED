using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    private float mOffset;

    public GameObject selectedObject;

    public GameObject xTranslate; //X gimbal

    public GameObject yTranslate; //Y gimbal

    public GameObject zTranslate; //Z gimbal

    public GameObject cubeTranslate;

    public Transform savedScale;

   
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("DragTranslate") || hit.collider.CompareTag("DragTranslateY") || hit.collider.CompareTag("DragTranslateZ")) // ADD TAGS HERE OR SMTHG IT WORKS NOW YOU JUST NEED TO ASSIGN TAGS
                    {
                        selectedObject = hit.collider.gameObject;
                    }

                 
                   
                }

            }

            else //Deslect
            { 
                
                selectedObject = null;
            }

        }

        if (selectedObject != null) //When selected do action.
        {

            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPos.x, worldPos.y, worldPos.z);
            Clamps();
            if(selectedObject.tag == "DragTranslate") //Translates via transform direction + offset
            {
                cubeTranslate.transform.position = xTranslate.transform.position + xTranslate.transform.TransformDirection(new Vector3( -2, 0, 0));

            }
            if (selectedObject.tag == "DragTranslateY")
            {
                cubeTranslate.transform.position = yTranslate.transform.position + yTranslate.transform.TransformDirection(new Vector3(0, -2, 0));

            }
            if (selectedObject.tag == "DragTranslateZ")
            {
                cubeTranslate.transform.position = zTranslate.transform.position + zTranslate.transform.TransformDirection(new Vector3(0, 0, -2));

            }


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
        Vector3 xpos = xTranslate.transform.localPosition;

        xpos.y = Mathf.Clamp(xTranslate.transform.localPosition.y, 0, 0);

        xpos.z = Mathf.Clamp(xTranslate.transform.localPosition.z, 0, 0);

        xTranslate.transform.localPosition = xpos;


        Vector3 ypos = yTranslate.transform.localPosition;

        ypos.x = Mathf.Clamp(yTranslate.transform.localPosition.x, 0, 0);

        ypos.z = Mathf.Clamp(yTranslate.transform.localPosition.z, 0, 0);

        yTranslate.transform.localPosition = ypos;


        Vector3 zpos = zTranslate.transform.localPosition;

        zpos.x = Mathf.Clamp(zTranslate.transform.localPosition.x, 0, 0);

        zpos.y = Mathf.Clamp(zTranslate.transform.localPosition.y, 0, 0);

        zTranslate.transform.localPosition = zpos;


    }
}
