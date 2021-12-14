using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private float now_x;
    private float now_y;
    private float now_z;
    private float value;
    private Vector3 myPos;

    private AudioSource myAudio;

    public GameObject boss_bullet;
    public GameObject summon_bounce;

    public AudioClip damaged_audio;

    public float yPos;
    public float speed;
    public float Rushspeed;
    public float rad;

    public bool phasing;
    public bool isStuned = false;
    public bool failRush = false;
    public int before;
    public int state;

    public int hitPoint;

    private PlayerControl playerControlScript;
    private Rigidbody myRb;

    // Start is called before the first frame update
    void Start()
    {
        myPos = transform.position;
        hitPoint = 2000;
        myRb = GetComponent<Rigidbody>();
        myAudio = GetComponent<AudioSource>();
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        value = 0;
        speed = 1.2f;
        Rushspeed = 1.6f;
        rad = 0.001f;
        state = 0;  //1 = rush, 2 = shootsmall, 3 = shootbig, 4 = danger, 5 = stun
        before = state;
        phasing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControlScript.isPaused)  //일시정지 아닐때만 활동
        {
            if(state == 0 && !phasing)  //idle
            {
                phasing = true;
                StartCoroutine(Idle());
            }
            else if(state == 1 && !phasing)  //rush
            {
                Debug.Log("Phase 1 start");
                phasing = true;
                StartCoroutine(Rush());
            }
            else if(state == 2 && !phasing) //shoot small
            {
                Debug.Log("Phase 2 start");
                phasing = true;
                StartCoroutine(Shooting());
            }
            else if(state == 3 && !phasing) //shoot big
            {
                Debug.Log("Phase 3 start");
                phasing = true;
                StartCoroutine(Summon());
            }
            else if(state == 4 && hitPoint <= 500) //danger
            {

            }
            else if(state == 5 && !isStuned) //stun
            {
                Debug.Log("Stun");
                isStuned = true;
                phasing = true;
                StartCoroutine(Stun());
            }
            else
            {
                FlyState();
            }
        }

        if(hitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Judge_Guard") && !failRush)
        {
            failRush = true;    //가드 성공
            playerControlScript.isInvincible = true;
            StartCoroutine(recoverFailRush());
            if(playerControlScript.isParried)   //패링성공시 5
            {
                state = 5;
            }
        }

        if(other.CompareTag("Judge_Bullet"))    //반사탄막 피격 
        {
            Destroy(other.gameObject);
            hitPoint -= (int)playerControlScript.antibulletPower;
            myAudio.PlayOneShot(damaged_audio, 1.0f);
        }
    }

    void FlyState()
    {
        now_x = transform.position.x;
        now_y = transform.position.y;
        now_z = transform.position.z;
        value += Time.deltaTime * speed;
        yPos = rad * Mathf.Sin(value);
        transform.position = new Vector3(now_x, now_y + yPos, now_z);
    }

    IEnumerator recoverFailRush()
    {
        yield return new WaitForSeconds(2.0f);
        failRush = false;
        playerControlScript.isInvincible = false;
    }

    IEnumerator Idle()
    {
        ChangeState();
        yield return new WaitForSeconds(2.0f);
        phasing = false;   //페이즈 종료
    }

    IEnumerator Rush()
    {
        //전조현상 아직없음
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Rush Start");
        for (int i = 0; i < 10; i++) //박치기
        {
            myRb.velocity += Vector3.left * Rushspeed * (i+1);
            if (state == 5) break;    //패링당함
            yield return new WaitForSeconds(0.1f);
        }

        if (state == 5)
        {
            myRb.velocity = Vector3.zero;
            yield break;    //패링당함, 돌진패턴 취소
        }

        yield return new WaitForSeconds(2.5f);  //잠시대기
        Vector3 temp = myPos;
        temp.y -= 10;
        transform.position = temp; //아래 생성 후 상승
        myRb.velocity = Vector3.zero;

        for(int i=0; i<100; i++)
        {
            temp.y += 0.1f;
            transform.position = temp;
            yield return new WaitForSeconds(0.01f);
        }

        ChangeState();
        yield return new WaitForSeconds(1.5f);  //잠시대기
        phasing = false;    //페이즈 종료
    }

    IEnumerator Shooting()
    {
        //전조현상 아직없음
        yield return new WaitForSeconds(1.5f);
        Vector3 temp = myPos;
        for(int i=1; i<=9; i++) //랜덤한곳에서 3발씩 발사(중복존재)
        {
            temp = myPos;
            int x = Random.Range(-4, 5);
            int y = Random.Range(-4, 5);
            temp.x += x;
            temp.y += y;
            var bul = Instantiate(boss_bullet, temp, boss_bullet.transform.rotation);
            bul.GetComponent<Flying_Bullet>().getVector(myPos);
            if (i % 3 == 0) yield return new WaitForSeconds(1.0f);
        }

        ChangeState();
        yield return new WaitForSeconds(4.0f);
        phasing = false;
    }

    IEnumerator Summon()
    {
        //전조현상 아직없음
        yield return new WaitForSeconds(1.0f);
        for(int i=0; i<3; i++)
        {
            Instantiate(summon_bounce, myPos, summon_bounce.transform.rotation);
            yield return new WaitForSeconds(0.5f);
        }

        ChangeState();
        yield return new WaitForSeconds(3.5f);
        phasing = false;
    }

    IEnumerator Stun()
    {
        Vector3 temp = transform.position;
        for (int i = 0; i < 100; i++)   //밀려나는 효과
        {
            temp.x += 0.2f;
            if (temp.x >= myPos.x) temp.x = myPos.x;
            transform.position = temp;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(3.0f);
        ChangeState();
        phasing = false;
        isStuned = false;
        transform.position = myPos;
    }

    void ChangeState()
    {
        state = Random.Range(1, 4); //1~3패턴 랜덤반복
        while(before == state)  //중복패턴 방지
        {
            state = Random.Range(1, 4);
        }
        before = state;
    }
}
