using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_UFO : MonoBehaviour
{
    private float moveSpeed = 2;
    private Vector3 save;
    // Start is called before the first frame update
    void Start()
    {
        save = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        if(transform.position.x < -10)
        {
            transform.position = save;
        }
    }
}
