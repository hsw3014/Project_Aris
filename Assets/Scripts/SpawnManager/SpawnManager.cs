using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3 spawnPos = new Vector3(40, 0, 0);
    private float startDelay = 2.0f;
    private float repeatRate = 6.0f;

    public GameObject[] SpawnList;
    public int PatternRandNumber;
    public int beforeRandNumber;

    private PlayerControl PlayerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        PatternRandNumber = Random.Range(0, SpawnList.Length);
        beforeRandNumber = PatternRandNumber;

        InvokeRepeating("Spawn_Manager", startDelay, repeatRate);
        PlayerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn_Manager()
    {
        if(!PlayerControlScript.isGameOver)
        {
            Instantiate(SpawnList[PatternRandNumber], spawnPos, SpawnList[PatternRandNumber].transform.rotation);
            PatternRandNumber++;
            if (PatternRandNumber >= SpawnList.Length)
                PatternRandNumber = 0;
            /*PatternRandNumber = Random.Range(0, SpawnList.Length);
            while(beforeRandNumber == PatternRandNumber)
            {
                PatternRandNumber = Random.Range(0, SpawnList.Length);
            }
            beforeRandNumber = PatternRandNumber;*/
        }
    }
}
