using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawn : MonoBehaviour
{

    private Vector3 mOffset;

    private Vector3 mousePoint;

    private float mZCoord;

    public GameObject spawnCube;

    GameObject cubeSpawned;

    public ObjectPool<GameObject> cubePool;

    private void Start()
    {
        cubePool = new ObjectPool<GameObject>(() => //Object pool to spawn objects.
        {
            return Instantiate(spawnCube);
        }, gameObject =>
        { gameObject.SetActive(true); }, gameObject =>
        { gameObject.SetActive(false); }, gameObject =>
        { Destroy(gameObject); }, false, 10, 20);
    }

    void OnMouseDown()

    {

        cubeSpawned = cubePool.Get();

        mZCoord = Camera.main.WorldToScreenPoint(cubeSpawned.transform.position).z;



    }



    private Vector3 GetMouseAsWorldPoint()

    {

       

        mousePoint = Input.mousePosition;



      

        mousePoint.z = mZCoord;



        

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }



    void OnMouseDrag()

    {

        cubeSpawned.transform.position = GetMouseAsWorldPoint();

    }

}

