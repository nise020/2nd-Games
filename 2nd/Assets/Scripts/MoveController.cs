using System.Collections;
using System.Collections.Generic;
using TMPro;//TMP_text
using UnityEngine;
using UnityEngine.UI;//image

public class MoveController : MonoBehaviour//�ٲܽ� ctrl+R+R
{

    //manager ,�񵿱������� ȣ���� ������ ����
    //controller ,updater ��� ���������� ȣ���� ���� �ʴ��� Ÿ ����� �ҷ��� ����ϴ� ��찡 ����
    [Header("�÷��̾� �̵� �� ����")]
    Rigidbody2D rigid;//null
    CapsuleCollider2D coll;
    BoxCollider2D box2d;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity = 0;//�������� �������� ��

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] bool showGroundCheck;
    [SerializeField] float groundCheckLenght;//�� ���̰� ���ӿ��� �󸶸�ŭ�� ���̰� �������� ���������,�˼� ����
    [SerializeField] Color groundColorCheck;

    [SerializeField] bool isGround;//�ν����Ϳ��� �÷��̾ �÷��� Ÿ�Ͽ� ���� �ߴ��� Ȯ�ο�
    bool isJump;

    Camera camMain;
    [Header("������")]
    [SerializeField] bool touchWall;
    bool isWallJump;
    [SerializeField] float wallJumpTime = 0.3f;
    float wallJumpTimer = 0.0f;//Ÿ�̸�

    [Header("���")]
    [SerializeField] private float dashTime = 0.3f;
    [SerializeField] private float dashSpeed = 20.0f;
    float dashTimer = 0.0f;//Ÿ�̸�
    TrailRenderer dashEffpact;//�������Ʈ,null
    [SerializeField] private float dashCollTime = 2f;
    float dashCollTimer = 0.0f;

    //�۷ι� ��Ÿ��
    [SerializeField] KeyCode dashKey;//enum �����Ͷ� �ν����Ϳ��� Ű�� ��� ����

    [Header("��� Ui")]
    [SerializeField] GameObject objDashCoolTime;
    [SerializeField] Image imgFill;
    [SerializeField] TMP_Text TextCoolTime;
    private void OnDrawGizmos()
    {
        

        if (showGroundCheck == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLenght), groundColorCheck);//Color.red

        }

        //Debug.DrawLine();������� üũ�뵵�� �� ī�޶󿡼� ���� �׷��ټ� ����
        //Gizmos.DrawSphere() ����׺��� �� ���� �ð�ȿ�� ����
        //Handles.DrawWireArc
    }
    #region ����
    //private void OnTriggerEnter2D(Collider2D collision)//������ �ݶ��̴��� ������,���� �����Ų�� ��
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
    //    {
    //        touchWall = true;
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer ==LayerMask.NameToLayer("Wall")) 
    //    {
    //        touchWall = false;
    //    }
    //}
    #endregion

    public void TriggerEnter(HitBox.ehitBoxType _type, Collider2D collision)
    {
        //HitBox = Class ������
        if (_type == HitBox.ehitBoxType.WallCheck)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                //�ش� �̸��� Layer�� ��Ҵ��� üũ
                touchWall = true;

            }
        }
    }

    public void TriggerExit(HitBox.ehitBoxType _type, Collider2D collision)
    {
        if (_type == HitBox.ehitBoxType.WallCheck)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                //�ش� �̸��� Layer�� ��Ҵ��� üũ
                touchWall = false;

            }
        }
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();//�ҷ�����,ȣ��
        box2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        dashEffpact = GetComponent<TrailRenderer>();
        dashEffpact.enabled = false;//����
        initUi();
    }

    void Start()
    {
        camMain = Camera.main;
    }

    void Update()
    {
        checkTimers();

        checkGround();

        dash();

        moving();
        doAnim();
        jump();

        checkGravity();

        checkAnim();
    }

    private void dash() //�߰����� �޸� �ʿ�
    {
        if (dashTimer == 0.0f && dashCollTimer == 0.0f && 
            Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.F)) 
        {
            //Input.GetKeyDown(dashKey);//�̷������� ����
            dashTimer = dashTime;
            dashCollTimer = dashTime;
            verticalVelocity = 0;
            dashEffpact.enabled = true;

            rigid.velocity = new Vector2(transform.localScale.x > 0 ? -dashSpeed : dashSpeed,0.0f);
            #region �ٸ� ����
            //if (transform.localScale.x > 0f) //����
            //{
            //    rigid.velocity = new Vector2(-dashSpeed, verticalVelocity);
            //}
            //else 
            //{
            //    rigid.velocity = new Vector2(dashSpeed, verticalVelocity);
            //}

            //rigid.velocity = transform.localScale.x>0 ? 
            //      new Vector2(-dashSpeed,verticalVelocity)
            //    : new Vector2(dashSpeed, verticalVelocity);
            #endregion
        }

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
        //Layer int�� ����� ���̾ ����
        //Layer�� int�� ���������� Ȱ���ϴ�int�� �ٸ�
        //Wall Layer,Ground Layer
        RaycastHit2D hit =
        //Physics2D.Raycast(transform.position,Vector2.down,
        //groundCheckLenght,LayerMask.GetMask("Ground"));
        //// new Vector2(0,-1)//GetMask(�ȿ� 2�� �̻��� ������ �ֱ�; ����)

        Physics2D.BoxCast(box2d.bounds.center,box2d.bounds.size,
        0f,Vector2.down, groundCheckLenght, LayerMask.GetMask("Ground"));

        //bounds

        if (hit)
        {
            isGround = true;
        }


    }

    private void checkTimers()
    {
        if (wallJumpTimer > 0.0f)
        {
            wallJumpTimer -= Time.deltaTime;
            if (wallJumpTimer < 0.0f)
            {
                wallJumpTimer = 0.0f;
            }
        }

        if (dashTimer > 0.0f)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer < 0.0f)
            {
                dashTimer = 0.0f;
                dashEffpact.enabled = false;
                dashEffpact.Clear();//������ ����
            }
        }

        if (dashCollTimer > 0.0f)
        {
            if (objDashCoolTime.activeSelf == false) 
            {
                objDashCoolTime.SetActive(true);
            }


            dashCollTimer -= Time.deltaTime;
            if (dashCollTimer < 0.0f)
            {
                dashCollTimer = 0.0f;
                objDashCoolTime.SetActive(false);

            }
            //2(Ÿ�̸�)/2(�ִ� Ÿ�̸�) = 1/0.5/0
            //dashCollTime = 2��, ��ų�� ���� 0. ���� 1�� �Ǿ�� ��
            imgFill.fillAmount = 1 - dashCollTimer / dashCollTime;
            TextCoolTime.text = dashCollTimer.ToString("F1");
            //F0 = 1 , F1 =1.0 , F2 1.00
        }
    }

    /// <summary>
    /// Ư�� Ű�� ����ؼ� �����̴� ���
    /// </summary>
    private void moving() 
    {
        if (wallJumpTimer > 0.0f || dashTimer > 0.0f)//�ؿ� �ڵ� ���� ���ϰ� �ϴ� ���
        {
            return;
        }
        //�¿�Ű�� ������ �¿�� �����δ�

        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        //a Left a key -1 / d, Raight a key 1
        //�ƹ��͵� �Է����� ������ 0

        moveDir.y = rigid.velocity.y;
        #region moveDir.y �޸�
        //unity����������Ŭ ���� ������ y�� �ʱ�ȭ �Ǽ�
        //moveDir.y = rigid.velocity.y; �̷��� ������ Y���� ��� ������ ���ӵ��� ���� �Ҽ� �մ�
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

    private void checkAnim()
    {
        #region scale�� �̿��� ������
        //    Vector3 scale = transform.localScale;
        //    if (moveDir.x < 0f && scale.x!=1.0f)//���� 0f�� ������ <- moving() �Լ�����
        //    {
        //        scale.x = 1.0f;
        //        transform.localScale = scale;
        //        Debug.Log("");
        //    }
        //    else if (moveDir.x > 0f && scale.x != -1.0f)//������
        //    {
        //        scale.x = -1.0f;
        //        transform.localScale = scale;
        //    }
        //scale.x != -1.0f<- ����ó��
        #endregion
        //transform.localPosition:�θ��� ���� ������ ��ġ�� �����ö�
        //transform.Position:���� ������ ��ġ�� �����ö�
        Vector2 mouseWorldPos = camMain.ScreenToWorldPoint(Input.mousePosition);//Scene������ �ƴ� Game(carse ����)
        Vector2 playerPos = transform.position;
        Vector2 fixedPos = mouseWorldPos - playerPos;

        Vector3 playerScale =transform.localScale;
        if (fixedPos.x > 0 && playerScale.x != -1.0f) 
        {
            playerScale.x = -1.0f;
        }
        else if (fixedPos.x < 0 && playerScale.x != 1.0f)
        {
            playerScale.x = 1.0f;
        }
        transform.localScale=playerScale;
        //Debug.Log(mouseWorldPos);
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
        if (isGround == false)//���߿� ���ִ� ���¶��,                    
        {
            //���� �پ��ְ� ���� ���� �÷��̾ ����Ű�� ������ �ִµ� ������ ������
            if (touchWall == true && moveDir.x != 0f && Input.GetKeyDown(KeyCode.Space)) 
            {
                isWallJump = true;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space)==true) 
        {
            isJump = true; 
        }

        //Imageit.fill
    }
    /// <summary>
    /// Rigidbody2D�� �߷��� 0�����ϰ� �ش� �Լ��� �߷��� ������
    /// </summary>
    private void checkGravity() 
    {
        if (dashTimer > 0.0f)
        {
            return;
        }
        else if (isWallJump == true) 
        {
            isWallJump = false;

            Vector2 dir = rigid.velocity;
            dir.x *= -1f;//�ݴ����//-1�� �����μ� x���� �ݴ�� �ο�
            rigid.velocity = dir;

            verticalVelocity = jumpForce * 0.5f;
            //�����ð� ������ �Է��Ҽ� ����� ���� �߷� �� x���� ���� ����
            //�ԷºҰ�,Ÿ�̸Ӹ� �۵����Ѿ���
            wallJumpTimer = wallJumpTime;
        }
        else if (isGround == false) //���߿� ���ִ� ����
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
        anim.SetInteger("Horizontal",(int)moveDir.x);//int ����ȯ �� Horizontal�� ��ġ �ο�
        anim.SetBool("isGrund", isGround);//boll���¿��� o/x���·� �ο�
    }

    private void initUi() 
    {
        objDashCoolTime.SetActive(false);
        imgFill.fillAmount = 0;
        TextCoolTime.text = "";
    }

    
}