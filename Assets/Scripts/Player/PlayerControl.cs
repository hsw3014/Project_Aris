using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody playRb;
    public AudioSource playerAudio;

    public AudioClip AttackSound;
    public AudioClip GuardSound;
    public AudioClip crashSound;

    public int hitPoint;
    public int stamina;
    public int MaxStamina;
    public int attackStamina;
    public int guardStamina;
    public float recoveryStamina;

    public float AttackShowDelay;
    public float AttackSpeed;
    public float GuardDelay;
    public float gravityModifier = 5.0f;
    public float InvincibleTime;
    public float ParringChance;

    public float SwitchForce;
    public float hitPower;
    public float antibulletPower;
    public float staminaRegenPenalty;

    public bool isPaused = false;
    public bool isGuarded = false;
    public bool isTakeDamage = false;
    public bool isInvincible = false;
    public bool CanAttack = true;
    public bool isGameOver = false; //public이라 접근 가능.
    public bool isAttackSwitch = false;
    public bool isParried = false;
    public bool isRegenStPenalty = false;

    public bool showDmgState1 = false;
    public bool showDmgState2 = false;
    public bool showAtkState = false;
    public bool showGuardState = false;

    public GameObject AttackObj;
    public GameObject AttackAnim;
    public GameObject GuardObj;

    private Boss bossScript;

    
    void Start()
    {
        playRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        hitPoint = 100;
        stamina = 100;
        MaxStamina = 100;
        attackStamina = 25;
        guardStamina = 35;
        recoveryStamina = 150;
        antibulletPower = 30;
        staminaRegenPenalty = 0.35f;

        hitPower = 18.0f;
        SwitchForce = 20.0f;
        AttackShowDelay = 0.1f;
        InvincibleTime = 0.8f;
        AttackSpeed = 0.1f;
        GuardDelay = 0.5f;
        ParringChance = 0.05f;

        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
        }

        if (!isPaused)
        {
            if (!isRegenStPenalty)  //스태미너 잠시 회복하지않음
            {
                stamina += (int)(Time.deltaTime * recoveryStamina);
                if (stamina > MaxStamina) stamina = MaxStamina;
            }
            //공격
            if (Input.GetButtonDown("Attack") && !isGameOver)
            {
                if (CanAttack)  //공격
                {
                    if (stamina > 0)    //스태미나 최소치가 존재해야 성공
                    {
                        if (showAtkState == false)
                        {
                            showAtkState = true;
                            StartCoroutine(AttackShowEvent());
                        }

                        CanAttack = false;
                        isGuarded = true;
                        stamina -= attackStamina;   //스태미나 감소
                        if (stamina < 0) stamina = 0;

                        StartCoroutine("AttackDelayEvent");

                        Instantiate(AttackObj, transform.position, transform.rotation); //피격판정 생성
                        Instantiate(AttackAnim, transform.position, transform.rotation);

                        playerAudio.PlayOneShot(AttackSound, 1.0f);

                        if (!isRegenStPenalty)  //스태미나 회복페널티 없으면 적용
                        {
                            isRegenStPenalty = true;
                            StartCoroutine(regenStamina_PenaltyEvent(0));
                        }
                    }
                }
            }

            if(Input.GetButtonDown("Guard") && !isGuarded)
            {
                if (stamina > 25)    //최소 스태미너 필요
                {
                    CanAttack = false;
                    isGuarded = true;
                    stamina -= guardStamina;
                    if (stamina < 0) stamina = 0;

                    Instantiate(GuardObj, transform.position, transform.rotation);

                    playerAudio.PlayOneShot(GuardSound, 1.0f);

                    StartCoroutine(Parring());
                    StartCoroutine("GuardDelayEvent");

                    if (!isRegenStPenalty)  //스태미나 회복페널티 없으면 적용
                    {
                        isRegenStPenalty = true;
                        StartCoroutine(regenStamina_PenaltyEvent(0.15f));
                    }
                }
            }
        }
    }
       
    //충돌발생
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))   //적과 충돌
        {
            if (!isInvincible)  //무적상태가 아니면 다음진행
            {
                playRb.velocity = Vector3.up * 7.5f;
                CanAttack = false;
                isGuarded = true;
                isTakeDamage = true;
                isInvincible = true;
                StartCoroutine("InvincibleEvent");
                StartCoroutine("DamageEvent");
                playerAudio.PlayOneShot(crashSound, 1.0f);
            }

            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            if (!isInvincible)  //무적상태가 아니면 다음진행
            {
                playRb.velocity = Vector3.up * 7.5f;
                CanAttack = false;
                isGuarded = true;
                isTakeDamage = true;
                isInvincible = true;
                StartCoroutine("InvincibleEvent");
                StartCoroutine("DamageEvent");
                playerAudio.PlayOneShot(crashSound, 1.0f);

                if(other.CompareTag("Boss_1"))
                {
                    hitPoint -= 40;
                    Debug.Log("Hit by Boss");
                }
            }
        }
    }

    IEnumerator AttackShowEvent()   //공격 스프라이트 재생시간 이벤트
    {
        yield return new WaitForSeconds(AttackShowDelay);
        showAtkState = false;
    }

    IEnumerator AttackDelayEvent()  //공격 딜레이.
    {
        yield return new WaitForSeconds(AttackSpeed);
        CanAttack = true;
        isGuarded = false;
    }

    IEnumerator GuardDelayEvent()   //가드 딜레이.
    {
        yield return new WaitForSeconds(GuardDelay);
        CanAttack = true;
        isGuarded = false;
    }

    IEnumerator DamageEvent()   //피격 이벤트
    {
        yield return new WaitForSeconds(InvincibleTime);
        isTakeDamage = false;
        CanAttack = true;
        isGuarded = false;
    }

    IEnumerator regenStamina_PenaltyEvent(float plus) //스태미너 페널티
    {
        yield return new WaitForSeconds(staminaRegenPenalty + plus);
        isRegenStPenalty = false;
    }

    IEnumerator InvincibleEvent()   //무적시간동안 스프라이트 번쩍임
    {
        int countTime = 0;

        while(countTime < 10)
        {
            if(countTime % 2 == 0)
            {
                showDmgState1 = true;
                showDmgState2 = false;
            }
            else
            {
                showDmgState1 = false;
                showDmgState2 = true;
            }
            yield return new WaitForSeconds(0.2f);
            countTime++;
        }

        isInvincible = false;

        yield return null;
    }

    IEnumerator Parring()
    {
        isParried = true;
        yield return new WaitForSeconds(ParringChance);
        isParried = false;
    }
}
