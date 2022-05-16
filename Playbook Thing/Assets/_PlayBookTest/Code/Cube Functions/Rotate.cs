using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    
    public GameObject selectedObject;

    public GameObject xRotate; //X gimbal

    public GameObject yRotate; //Y gimbal

    public GameObject zRotate; //Z gimbal

    public  GameObject cubeRotate;

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
                    if (hit.collider.CompareTag("DragRotateX") || hit.collider.CompareTag("DragRotateY") || hit.collider.CompareTag("DragRotateZ"))
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

        if (selectedObject != null)
        {

            
            float rotX = Input.GetAxis("Mouse X")* 10f * Mathf.Deg2Rad; //I hate math

            float rotY = Input.GetAxis("Mouse Y") * 10f * Mathf.Deg2Rad;

            rotX = (Mathf.Clamp(rotX, -360f, 360));

            rotY = (Mathf.Clamp(rotY, -360f, 360));
            Clamps();


            if (selectedObject.tag == "DragRotateZ")
            {
                xRotate.transform.Rotate(Vector3.forward, -rotX);
                cubeRotate.transform.rotation = xRotate.transform.rotation;

            }
            if (selectedObject.tag == "DragRotateX")
            {
                yRotate.transform.Rotate(Vector3.right, rotX);
                cubeRotate.transform.rotation = yRotate.transform.rotation;

            }

            if (selectedObject.tag == "DragRotateY")
            {
                zRotate.transform.Rotate(Vector3.up, -rotY);
                cubeRotate.transform.rotation = zRotate.transform.rotation;

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
        Vector3 xpos = xRotate.transform.localPosition;
        xpos.x = Mathf.Clamp(xRotate.transform.localPosition.x, 0, 0);

        xpos.y = Mathf.Clamp(xRotate.transform.localPosition.y, 0, 0);

        xpos.z = Mathf.Clamp(xRotate.transform.localPosition.z, 0, 0);

        xRotate.transform.localPosition = xpos;


        Vector3 ypos = yRotate.transform.localPosition;

        ypos.x = Mathf.Clamp(yRotate.transform.localPosition.x, 0, 0);

        ypos.y = Mathf.Clamp(yRotate.transform.localPosition.y, 0, 0);

        ypos.z = Mathf.Clamp(yRotate.transform.localPosition.z, 0, 0);

        yRotate.transform.localPosition = ypos;


        Vector3 zpos = zRotate.transform.localPosition;

        zpos.x = Mathf.Clamp(zRotate.transform.localPosition.x, 0, 0);

        zpos.y = Mathf.Clamp(zRotate.transform.localPosition.y, 0, 0);

        zpos.z = Mathf.Clamp(zRotate.transform.localPosition.z, 0, 0);


        zRotate.transform.localPosition = zpos;


    }

}
