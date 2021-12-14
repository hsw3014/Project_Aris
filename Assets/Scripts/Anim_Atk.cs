using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Atk : MonoBehaviour
{
    public Animator animator;
    private PlayerControl playerScript;
    public Vector3 set;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        set = new Vector3(0.5f, 1, 0);
        transform.position = playerScript.transform.position + set;
        delay = 0.5f / 3;
        Destroy(gameObject, delay);
        //StartCoroutine(CheckAnim());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerScript.transform.position + set;
    }
}
