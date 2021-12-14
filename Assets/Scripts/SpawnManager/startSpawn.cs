using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startSpawn : MonoBehaviour
{
    public GameObject spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(spawnManager, new Vector3(0, 0, 0), spawnManager.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
