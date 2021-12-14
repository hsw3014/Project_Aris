using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamera : MonoBehaviour
{
    public Transform target;
    public float dist;
    private Transform Camera;
    private float new_y;
    private float start_y;
    private float end_y;

    // Start is called before the first frame update
    void Start()
    {
        end_y = 9;
        Camera = GetComponent<Transform>();
        start_y = Camera.position.y;
        Camera.position = new Vector3(target.position.x + 10, target.position.y + 2, target.position.z - 50);
        dist = Camera.position.y - target.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Camera.position.y - target.position.y;
        new_y = Camera.position.y - dist;
        if(new_y <= end_y && new_y >= start_y)  //평소
            Camera.position = new Vector3(Camera.position.x, new_y, Camera.position.z);
        else if(new_y <= start_y)   //하한선
            Camera.position = new Vector3(Camera.position.x, start_y, Camera.position.z);
        else    //상한선
            Camera.position = new Vector3(Camera.position.x, end_y, Camera.position.z);
    }
}
