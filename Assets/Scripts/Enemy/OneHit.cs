using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHit : MonoBehaviour
{
    private PlayerControl playerScript;
    public AudioClip DeathSound;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Judge_Attack") || other.CompareTag("Player"))
        {
            playerScript.playerAudio.PlayOneShot(DeathSound, 1.0f);
            Destroy(gameObject);
        }
    }
}
