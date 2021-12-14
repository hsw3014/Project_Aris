using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Slime : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(40, 0, 0);
    private float startDelay = 0.0f;
    private float makeXpos;

    public GameObject SlimePrefab;

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
            StartCoroutine(Wave_Trap());
        }
    }

    IEnumerator Wave_Trap()
    {
        Instantiate(SlimePrefab, new Vector3(makeXpos, 2, 0), SlimePrefab.transform.rotation);
        Instantiate(SlimePrefab, new Vector3(makeXpos + 3, 2, 0), SlimePrefab.transform.rotation);
        Destroy(gameObject);
        yield return null;
    }
}
