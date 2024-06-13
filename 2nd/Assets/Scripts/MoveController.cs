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
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        checkGround();

        moving();
    }

    private void checkGround() 
    {

        //if (gameObject.CompareTag("Player") == true) //�±״� string���� ����� �ñ׸� ����
        
        //float.PositiveInfinity
        //Layer int�� ��礷�� ���̾ ����
        //Layer�� int�� ���������� Ȱ���ϴ�int�� �ٸ�
        //Wall Layer,Ground Layer
        RaycastHit2D hit =

        
        Physics2D.Raycast(transform.position,Vector2.down, groundCheckLenght,LayerMask.GetMask("Ground"));// new Vector2(0,-1)//GetMask(�ȿ� 2�� �̻��� ������ �ֱ�; ����)

        if (hit){isGround = true;}
        else {isGround = false;}

    }
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
        #region �߷����� �޸�
        //Physics2D.gravity = moveDir;
        #endregion
    }
}
