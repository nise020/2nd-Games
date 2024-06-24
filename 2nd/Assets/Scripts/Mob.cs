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
        //�ڽ��� Rigidbody2D�� ã�´�
        groundCheckColl = GetComponentInChildren<BoxCollider2D>();
        //�ڽ�(GroundCheck)�� BoxCollider2D�� ã�´�
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
        //IsTouchingLayers�� ��� ������ ��� ���̾� üũ
        if (groundCheckColl.IsTouchingLayers(LayerMask.GetMask("Ground")) == false)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            //value ������ �� ���Ӱ� ���� ����� �Ѵ�

            moveDir.x *= -1;
        }
        rigid.velocity = new Vector2(moveDir.x * moveSpeed, rigid.velocity.y);
        //Debug.Log( groundCheckColl.IsTouchingLayers(LayerMask.GetMask("Ground")));//��� ������ ��� ���̾� üũ

    }
}
