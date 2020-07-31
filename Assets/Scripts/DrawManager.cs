using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public GameObject drawPrefab;

    private GameObject trail;
    private Plane planeObj;
    private Vector3 startPos;
    
    private List<GameObject> trails = new List<GameObject>();

    void Start()
    {
        planeObj = new Plane(Camera.main.transform.forward * -1, this.transform.position);
    }

    void Update()
    {
        Camera cam = Camera.main;
        
        Vector3 temp = Input.mousePosition;
        temp.z = 10f;
        if (cam != null)
        {
            transform.position = cam.ScreenToWorldPoint(temp);

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
            {
                trail = Instantiate(drawPrefab, this.transform.position, Quaternion.identity);
                trails.Add(trail);

                Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
                float distance;
                
                if (planeObj.Raycast(mouseRay, out distance))
                    startPos = mouseRay.GetPoint(distance);
                
                
                
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
            {
                Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
                float distance;
                if (planeObj.Raycast(mouseRay, out distance))
                {
                    trail.transform.position = mouseRay.GetPoint(distance);
                }
            }
            
        }
    }

    public void Clear()
    {
        foreach (GameObject Trail in trails)
            Destroy(Trail);

        trails.Clear();
    }
}
