using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMonster : MonoBehaviour
{
    public int monster_hp;
    public float moveSpeed;
    private PlayerControl playerScript;
    public Rigidbody enemyRb;
    public Vector3 lookDirection;

    public int leng;

    // Start is called before the first frame update
    void Start()
    {
        monster_hp = 100;
        moveSpeed = 8.0f;
        lookDirection = (Vector3.left).normalized;
        enemyRb = GetComponent<Rigidbody>();
        playerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerScript.isPaused)
        {
            if (monster_hp <= 0)
            {
                Destroy(gameObject);
            }
            enemyRb.AddForce(lookDirection * moveSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            monster_hp = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Judge_Attack"))
        {
            enemyRb.velocity = Vector3.right * playerScript.hitPower;
            monster_hp -= 40;
        }
    }

}
