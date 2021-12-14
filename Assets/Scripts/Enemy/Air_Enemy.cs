using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air_Enemy : MonoBehaviour
{
    public float rotateSpeed = 3000.0f;
    public float moveSpeed = 15.0f;

    private Rigidbody enemyRb;
    private PlayerControl playerControlScript;

    public GameObject bullet;
    public float dist;
    public bool SecondPattern = false;
    public bool AccessToPlayer = true;
    public bool FinalPattern = false;
    public bool CanAttack = true;

    private float now_x;
    private float now_y;
    private float now_z;
    private float Value;
    private float start_y;
    public float yPos;

    public int hitPoint;
    public float speed;
    public float rad;
    public float waitAttackTime;
    public float AttackDelayVar;
    public float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        hitPoint = 150;
        start_y = transform.position.y;
        enemyRb = GetComponent<Rigidbody>();
        enemyRb.freezeRotation = true;
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        dist = Vector3.Distance(playerControlScript.transform.position, transform.position);
        speed = 1.2f;
        rad = 0.005f;

        waitAttackTime = 1.0f;
        attackSpeed = 0.2f;
        AttackDelayVar = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어와 자신 거리 실시간 계산
        if (!playerControlScript.isPaused)
        {
            dist = Vector3.Distance(playerControlScript.transform.position, transform.position);

            if (FinalPattern)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    playerControlScript.transform.position,
                    Time.deltaTime * moveSpeed);
            }
            else if (dist >= 18.0f && AccessToPlayer)  //거리까진 계속 추적
            {
                // Vector3 lookDirection = (playerControlScript.transform.position - transform.position).normalized;
                //enemyRb.velocity = lookDirection * moveSpeed;
                transform.position = Vector3.MoveTowards(transform.position,
                    playerControlScript.transform.position + new Vector3(4, 0, 0),
                    Time.deltaTime * moveSpeed);
            }
            else if (!SecondPattern) //거리 도달후 잠시 멈추고 2차패턴
            {
                AccessToPlayer = false;
                FlyState();
                StartCoroutine(waitAttack());
            }
            else if (SecondPattern) //투사체 발사
            {
                FlyState();
                if (CanAttack)
                {
                    CanAttack = false;
                    StartCoroutine(AttackDelay());
                    StartCoroutine(makeBullet());
                }
            }
        }
    }

    IEnumerator waitAttack()
    {
        yield return new WaitForSeconds(waitAttackTime);
        SecondPattern = true;
    }

    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(AttackDelayVar);
        CanAttack = true;
    }

    IEnumerator makeBullet()
    {
        for(int i=0; i<3; i++)  //3번 발사
        {
            //bul은 생성된 토큰
            //GetComponent를 통해 해당 토큰 스크립트에 접근 후 함수로 변수 변경
            var bul = Instantiate(bullet, transform.position, bullet.transform.rotation);
            bul.GetComponent<Flying_Bullet>().getVector(transform.position);
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    //피격시 소멸
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

        if(other.CompareTag("Judge_Bullet"))
        {
            hitPoint -= (int)playerControlScript.antibulletPower;
            if(hitPoint <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void FlyState()
    {
        now_x = transform.position.x;
        now_y = transform.position.y;
        now_z = transform.position.z;
        Value += Time.deltaTime * speed;
        yPos = rad * Mathf.Sin(Value);
        transform.position = new Vector3(now_x, now_y + yPos, now_z);
    }
}
