using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Air : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(40, 0, 0);
    private Vector3 SwitchPos = new Vector3(40, 2, 0);
    private float startDelay = 0.0f;
    private float makeXpos;

    public GameObject SwitchPrefab;
    public GameObject obstaclePrefab;
    public GameObject AirSwitchPrefab;

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
            StartCoroutine(Wave_AirSwitch());
        }
        Destroy(gameObject, 5.0f);
    }

    IEnumerator Wave_Trap()
    {
        Instantiate(SwitchPrefab, SwitchPos, SwitchPrefab.transform.rotation);
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 45; i++)
        {
            Instantiate(obstaclePrefab, new Vector3(makeXpos + i + 0.8f, 1, 0), obstaclePrefab.transform.rotation);
        }
    }

    IEnumerator Wave_AirSwitch()
    {
        yield return new WaitForSeconds(1.3f);
        Instantiate(AirSwitchPrefab, new Vector3(makeXpos, 6, 0), AirSwitchPrefab.transform.rotation);
        Instantiate(AirSwitchPrefab, new Vector3(makeXpos + 12, 6, 0), AirSwitchPrefab.transform.rotation);
        Instantiate(AirSwitchPrefab, new Vector3(makeXpos + 24, 6, 0), AirSwitchPrefab.transform.rotation);
    }
}
