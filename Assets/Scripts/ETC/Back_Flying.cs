using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back_Flying : MonoBehaviour
{
    private Rigidbody myRb;
    private SpriteRenderer mySp;

    private float jumpPower;
    private float moveSpeed;
    private bool moveStop = false;
    private bool secMov = false;
    private Vector3 save;
    private Vector3 diag;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        save = transform.position;
        diag = new Vector3(-15, -5, 7);
        myRb = GetComponent<Rigidbody>();
        mySp = GetComponent<SpriteRenderer>();
        jumpPower = 20.0f;
        moveSpeed = 5.0f;

        color = mySp.color; //투명도 조절
        color.a = 0.5f;
        mySp.color = color;

        StartCoroutine(moveauto());
    }

    // Update is called once per frame
    void Update()
    {
        if(!moveStop)
        {
            //transform.position = Vector3.MoveTowards(transform.position, diag, Time.deltaTime * moveSpeed);
            transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground") && !moveStop && !secMov)
        {
            myRb.velocity = Vector3.up * jumpPower;
            //myRb.AddForce(Vector3.up * jumpPower);
        }
    }

    IEnumerator moveauto()
    {
        while (true)
        {
            myRb.velocity = Vector3.zero;
            transform.position = save;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            moveStop = false;
            secMov = false;

            yield return new WaitForSeconds(3.0f);
            moveStop = true;
            yield return new WaitForSeconds(2.0f);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            yield return new WaitForSeconds(0.6f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(0.6f);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            yield return new WaitForSeconds(0.6f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(1.0f);

            secMov = true;
            moveStop = false;
            myRb.velocity = Vector3.up * jumpPower * 1.2f;
            yield return new WaitForSeconds(1.2f);
            myRb.velocity = Vector3.up * jumpPower * 1.2f;
            yield return new WaitForSeconds(1.2f);
            myRb.velocity = Vector3.up * jumpPower * 1.2f;
            yield return new WaitForSeconds(10.0f);
        }
    }
}
