using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour
{
    public float Destroy_Delay;
    private PlayerControl playerScript;
    private Vector3 AttackRange = new Vector3(2.5f, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        Destroy_Delay = 0.5f;
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
