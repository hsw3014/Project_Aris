using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageBody_Manager : MonoBehaviour
{
    private float now_x;
    private float now_y;
    private float now_z;
    private float Value;
    public float yPos;

    public float speed;
    public float rad;
    public Sprite now_sprite;
    public Sprite move_sprite;
    public Sprite attack_sprite;
    public Sprite damage_sprite;
    public Sprite guard_sprite;

    private PlayerControl playerControlScript;
    private SpriteRenderer Sprite_Control;

    // Start is called before the first frame update
    void Start()
    {
        //Player의 Script 참조
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        Sprite_Control = gameObject.GetComponent<SpriteRenderer>();
        Sprite_Control.sprite = now_sprite;

        Value = 0;
        speed = 1.2f;
        rad = 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        Sprite_Control.sprite = now_sprite;
        //공격시 공격스프라이트 재생
        if (!playerControlScript.isGameOver && playerControlScript.showAtkState)
        {
            now_sprite = attack_sprite;
            FlyState();
        }
        //피격시 피격스프라이트 재생
        else if (!playerControlScript.isGameOver && playerControlScript.isTakeDamage)
        {
            now_sprite = damage_sprite;
        }
        else if(!playerControlScript.isGameOver && playerControlScript.isGuarded)
        {
            now_sprite = guard_sprite;
        }
        else    //평상시, 부유효과
        {
            now_sprite = move_sprite;
            FlyState();
        }

        
        //피격시 무적스프라이트 재생
        if (!playerControlScript.isGameOver && playerControlScript.isInvincible)
        {
            if (playerControlScript.showDmgState1)
            {
                Sprite_Control.color = new Color32(255, 255, 255, 90);
            }
            else if (playerControlScript.showDmgState2)
            {
                Sprite_Control.color = new Color32(255, 255, 255, 180);
            }
        }
    }

    void FlyState()
    {
        now_x = playerControlScript.transform.position.x;
        now_y = playerControlScript.transform.position.y + 0.7f;
        now_z = playerControlScript.transform.position.z;
        Value += Time.deltaTime * speed;
        yPos = rad * Mathf.Sin(Value);
        transform.position = new Vector3(now_x, now_y + yPos, now_z);
    }
}
