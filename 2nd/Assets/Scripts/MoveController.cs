using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MoveController : MonoBehaviour//�ٲܽ� ctrl+R+R
{

    //manager ,�񵿱������� ȣ���� ������ ����
    //controller ,updater ��� ���������� ȣ���� ���� �ʴ��� Ÿ ����� �ҷ��� ����ϴ� ��찡 ����
    [Header("�÷��̾� �̵� �� ����")]
    Rigidbody2D rigid;//null
    CapsuleCollider2D coll;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity =0;//�������� �������� ��

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] bool showGroundCheck;
    [SerializeField] float groundCheckLenght;//�� ��ư� ���ӿ��� �󸶸�ŭ�� ���̰� �������� ���������,�˼� ����
    [SerializeField] Color groundColorCheck;

    [SerializeField] bool isGround;//�ν����Ϳ��� �÷��̾ �÷��� Ÿ�Ͽ� ���� �ߴ��� Ȯ�ο�
    bool isJump;



    private void OnDrawGizmos()
    {
        if (showGroundCheck == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLenght), groundColorCheck);//Color.red

        }

        //Debug.DrawLine();����׷� üũ�뵵�� �� ī�޶󿡼� ���� �׷��ټ� ����
        //Gizmos.DrawSphere() ����׺��� �� ���� �ð�ȿ�� ����
        //Handles.DrawWireArc
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();//�ҷ�����,ȣ��
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        checkGround();

        moving();
        jump();
        checkGravity();
        doAnim();
    }
    /// <summary>
    /// �ش� �Լ��� ���� ��Ҵ��� Ȯ���ϴ� �ڵ�
    /// </summary>
    private void checkGround() 
    {
        isGround = false;
        if (verticalVelocity > 0)//����ó��
        {
            return; 
        }
        //if (gameObject.CompareTag("Player") == true) //�±״� string���� ����� �ñ׸� ����
        
        //float.PositiveInfinity
        //Layer int�� ��礷�� ���̾ ����
        //Layer�� int�� ���������� Ȱ���ϴ�int�� �ٸ�
        //Wall Layer,Ground Layer
        RaycastHit2D hit =
        Physics2D.Raycast(transform.position,Vector2.down,
        groundCheckLenght,LayerMask.GetMask("Ground"));
        // new Vector2(0,-1)//GetMask(�ȿ� 2�� �̻��� ������ �ֱ�; ����)

        if (hit)
        {
            isGround = true;
        }
        

    }
    /// <summary>
    /// Ư�� Ű�� ����ؼ� �����̴� ���
    /// </summary>
    private void moving() 
    {
        //�¿�Ű�� ������ �¿�� �����δ�
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//a Left a key -1 / d, Raight a key 1 �ƹ��͵� �Է� ���ϸ� 0
        moveDir.y = rigid.velocity.y;
        #region moveDir.y �޸�
        //unity����������Ŭ ���� ������ y�� �ʱ�ȭ �Ǽ�
        //moveDir.y = rigid.velocity.y; �̷��� �ؾ� ���ӵ��� �ٴ´�
        #endregion 
        //���ð��� ���鶧�� ������Ʈ �ڵ忡 ���� �����̵� �ϰ� ����
        //�𸮿� ���ؼ� �̵�
        rigid.velocity = moveDir;//moveDir.y�� ���� 0�̸� ���� �ʱ�ȭ �ؼ� õõ�� ������
        #region �޸�
        //velocity�� rigid�� ���õȱ���̶� Time.deltaTime�� �ʿ� ����
        //rigid == Rigidbody2D���� AddForce���� �پ��� ����� �ִ�
        #endregion
        #region �߷����� �޸�
        //Physics2D.gravity = moveDir;
        #endregion
    }
    /// <summary>
    /// �����ϴ� ���
    /// </summary>
    private void jump() 
    {
        #region GetKey �Է� Ÿ�̹� Ȯ��
        //if (Input.GetKeyDown(KeyCode.Space)) { Debug.Log("GetKeyDown"); }
        //if (Input.GetKeyUp(KeyCode.Space)) { Debug.Log("GetKeyUp"); }
        //if (Input.GetKey(KeyCode.Space)) { Debug.Log("GetKey"); }
        #endregion
        #region �ٸ� ����
        //if (isGround==true && isJump == false && Input.GetKeyDown(KeyCode.Space)) 
        //{

        //    rigid.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);//(x=1 ,y=0)
        //    //ForceMode2D.Impulse = ���۽����� ���� �ۿ�
        //    //rigid.AddForce(new Vector2(10,0));//(x=10 ,y=0)//������ �̴���
        //    isJump = true;//����ó��
        #region 2�� ���� ����
        //    //Vector2 forse = rigid.velocity;
        //    //forse.y = 0;
        //    //rigid.velocity = forse;
        #endregion
        //}
        #endregion
        if (isGround == false) 
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)==true) 
        {
            isJump = true; 
        }
    }
    /// <summary>
    /// Rigidbody2D�� �߷��� 0�����ϰ� �ش� �Լ��� �߷��� ������
    /// </summary>
    private void checkGravity() 
    {
        if (isGround == false) //���߿� ���ִ� ����
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;//-9.81<- ������Ʈ ���� ����
            //������ �������ν� �߷��� ���ӵ��� �����Ѵ�
            if (verticalVelocity < -10f)
            {
                verticalVelocity = -10f;
            }
        }
        else if (isJump == true)
        {
            isJump = false;
            verticalVelocity = jumpForce;

        }
        else if (isGround == true) 
        {
             verticalVelocity = 0;//����ó�� �ʿ�
        }
        rigid.velocity = new Vector2(rigid.velocity.x, verticalVelocity);
    }
    /// <summary>
    /// �ִϸ��̼� ���� �� �ο�
    /// </summary>
    private void doAnim() 
    {
        anim.SetInteger("Horizontal",(int)moveDir.x);
        anim.SetBool("isGrund", isGround);
    }
}