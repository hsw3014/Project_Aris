using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying_Bullet : MonoBehaviour
{
    private Rigidbody myRb;
    private PlayerControl playerControlScript;

    public GameObject anti_bullet;
    public float lookRight;
    public float moveSpeed;
    public float angle;
    private float leftBound = -15;
    private Vector3 myParentPos;

    // Start is called before the first frame update
    void Start()
    {
        //lookRight = 90.0f;
        //transform.rotation = Quaternion.Euler(0, 0, lookRight);
        moveSpeed = 10.0f;
        myRb = GetComponent<Rigidbody>();
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControlScript.isPaused)
        {
            angle = getAngle();
            transform.rotation = Quaternion.Euler(0, 0, angle);

            transform.position = Vector3.MoveTowards(transform.position,
                playerControlScript.transform.position,
                Time.deltaTime * moveSpeed);
        }

        if(transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }

    //각도구하기
    float getAngle()
    {
        float dist_x = transform.position.x - playerControlScript.transform.position.x;
        float dist_y = transform.position.y - playerControlScript.transform.position.y;

        float radian = Mathf.Atan2(dist_y, dist_x);
        float degree = radian * 180 / Mathf.PI;

        return degree;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))  //피격시 체력감소
        {
            if (!playerControlScript.isInvincible)
            {
                playerControlScript.hitPoint -= 20;
            }
            Destroy(gameObject);
        }

        if(other.CompareTag("Judge_Guard")) //가드시 적에게 반사
        {
            var Anti = Instantiate(anti_bullet, 
                transform.position, 
                transform.rotation);
            Anti.GetComponent<Anti_Bullet>().getVector(myParentPos);
            Destroy(gameObject);
        }
    }

    public void getVector(Vector3 v)
    {
        myParentPos = v;
    }
}
