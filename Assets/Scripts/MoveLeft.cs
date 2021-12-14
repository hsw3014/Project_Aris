using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float moveSpeed = 30.0f;
    private float leftBound = -15;

    private PlayerControl playerControlScript;  //PlayerControl 스크립트 내 변수 접근 위함

    // Start is called before the first frame update
    void Start()
    {
        playerControlScript = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerControlScript.isGameOver) //게임오버가 아닐때만 움직인다.
        transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);

        if(transform.position.x < leftBound && CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
