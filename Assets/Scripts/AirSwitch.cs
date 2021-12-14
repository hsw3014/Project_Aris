using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSwitch : MonoBehaviour
{
    private PlayerControl playerScript;

    public float jumpForce;
    public AudioClip JumpingSound;
    public AudioClip FailSound;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        jumpForce = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Judge_Attack"))
        {
            // playerScript.playRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerScript.playRb.velocity = Vector3.up * jumpForce;
            playerScript.playerAudio.PlayOneShot(JumpingSound, 1.0f);
            Destroy(gameObject);
            //playerScript.transform.position = new Vector3(0, 3, 0);
            //playerScript.playRb.velocity = Vector3.up * jumpForce;
        }

        if (other.CompareTag("Player"))
        {
            playerScript.playerAudio.PlayOneShot(FailSound, 1.0f);
            Destroy(gameObject);
        }
    }
}
