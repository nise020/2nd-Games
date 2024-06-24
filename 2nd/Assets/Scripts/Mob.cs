using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 moveDir=new Vector2(1f,0f);
    [SerializeField] float moveSpeed =3;
    #region
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    #endregion
    BoxCollider2D groundCheckColl;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //자신의 Rigidbody2D를 찾는다
        groundCheckColl = GetComponentInChildren<BoxCollider2D>();
        //자식(GroundCheck)의 BoxCollider2D를 찾는다
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    private void move() 
    {
        //IsTouchingLayers가 비어 있으면 모든 레이어 체크
        if (groundCheckColl.IsTouchingLayers(LayerMask.GetMask("Ground")) == false)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            //value 데이터 라서 새롭게 정의 해줘야 한다

            moveDir.x *= -1;
        }
        rigid.velocity = new Vector2(moveDir.x * moveSpeed, rigid.velocity.y);
        //Debug.Log( groundCheckColl.IsTouchingLayers(LayerMask.GetMask("Ground")));//비어 있으면 모든 레이어 체크

    }
}
