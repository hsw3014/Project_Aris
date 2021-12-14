using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Me : MonoBehaviour
{
    public float Destroy_Delay = 0.15f;
    private PlayerControl playerScript;
    private Vector3 AttackRange = new Vector3(2.5f, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        transform.position = playerScript.transform.position + AttackRange;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerScript.transform.position + AttackRange;
        Destroy(gameObject, Destroy_Delay);
    }
}
