using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Trap : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(40, 0, 0);
    private Vector3 SwitchPos = new Vector3(40, 2, 0);
    private float startDelay = 0.0f;
    private float makeXpos;

    public GameObject SwitchPrefab;
    public GameObject obstaclePrefab;
    public GameObject OneHitPrefab;

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
        Instantiate(SwitchPrefab, SwitchPos, SwitchPrefab.transform.rotation);
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < 8; i++)
        {
            Instantiate(obstaclePrefab, new Vector3(makeXpos + i + 0.8f, 1, 0), obstaclePrefab.transform.rotation);
        }
        yield return new WaitForSeconds(1.6f);
        Instantiate(OneHitPrefab, new Vector3(makeXpos, 1, 0), OneHitPrefab.transform.rotation);
        Instantiate(OneHitPrefab, new Vector3(makeXpos + 5, 1, 0), OneHitPrefab.transform.rotation);
        Instantiate(OneHitPrefab, new Vector3(makeXpos + 10, 1, 0), OneHitPrefab.transform.rotation);
        Destroy(gameObject);
    }
}
