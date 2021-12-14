using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Flying : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(40, 0, 0);
    private float startDelay = 0.0f;
    private float makeXpos;

    public GameObject AirPrefab;

    private PlayerControl PlayerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        makeXpos = 40.0f;
        Invoke("SpawnObstacle", startDelay);
        PlayerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (!PlayerControlScript.isGameOver)
        {
            StartCoroutine(Wave_Air());
        }
    }

    IEnumerator Wave_Air()
    {
        Instantiate(AirPrefab, new Vector3(makeXpos, 2, 0), AirPrefab.transform.rotation);
        Instantiate(AirPrefab, new Vector3(makeXpos + 3, 10, 0), AirPrefab.transform.rotation);
        Instantiate(AirPrefab, new Vector3(makeXpos + 6, 20, 0), AirPrefab.transform.rotation);
        Destroy(gameObject);
        yield return null;
    }
}

