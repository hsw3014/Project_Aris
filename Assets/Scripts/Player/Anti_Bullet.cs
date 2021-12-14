using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anti_Bullet : MonoBehaviour
{
    private PlayerControl playerControlScript;
    private Rigidbody myRb;
    private float angle;
    private float moveSpeed;
    private Vector3 myEnemyPos;

    private float RightBound;
    private Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        angle = getAngle();
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);
        moveSpeed = 15.0f;
        RightBound = 40;
        Direction = (myEnemyPos - transform.position).normalized;  //상대방에게
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerControlScript.isPaused)
        {
            /*transform.position = Vector3.MoveTowards(transform.position,
                myEnemyPos,
                Time.deltaTime * moveSpeed);*/
            myRb.velocity = Direction * moveSpeed;
        }

        if(transform.position.x >= RightBound)  //화면밖 나가면 삭제
        {
            Destroy(gameObject);
        }
    }

    float getAngle()
    {
        float dist_x = transform.position.x - myEnemyPos.x;
        float dist_y = transform.position.y - myEnemyPos.y;

        float radian = Mathf.Atan2(dist_y, dist_x);
        float degree = radian * 180 / Mathf.PI;

        return degree;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Monster") 
            && other.CompareTag("Flying"))
        {
            Destroy(gameObject);
        }
    }

    public void getVector(Vector3 v)
    {
        myEnemyPos = v;
    }
}
