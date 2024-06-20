using System.Collections;
using System.Collections.Generic;
using TMPro;//TMP_text
using UnityEngine;
using UnityEngine.UI;//image

public class MoveController : MonoBehaviour//바꿀시 ctrl+R+R
{

    //manager ,비동기적으로 호출이 왔을때 대응
    //controller ,updater 사용 동기적으로 호출이 오지 않더라도 타 기능을 불러서 사용하는 경우가 많음
    [Header("플레이어 이동 및 점프")]
    Rigidbody2D rigid;//null
    CapsuleCollider2D coll;
    BoxCollider2D box2d;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity = 0;//수직으로 떨어지는 힘

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] bool showGroundCheck;
    [SerializeField] float groundCheckLenght;//이 길이가 게임에서 얼마만큼의 길이가 육안으러 고기전에느,알수 없음
    [SerializeField] Color groundColorCheck;

    [SerializeField] bool isGround;//인스팩터에서 플레이어가 플랫폼 타일에 착지 했는지 확인용
    bool isJump;

    Camera camMain;
    [Header("벽점프")]
    [SerializeField] bool touchWall;
    bool isWallJump;
    [SerializeField] float wallJumpTime = 0.3f;
    float wallJumpTimer = 0.0f;//타이머

    [Header("대시")]
    [SerializeField] private float dashTime = 0.3f;
    [SerializeField] private float dashSpeed = 20.0f;
    float dashTimer = 0.0f;//타이머
    TrailRenderer dashEffpact;//대시이펙트,null
    [SerializeField] private float dashCollTime = 2f;
    float dashCollTimer = 0.0f;

    //글로벌 쿨타임
    [SerializeField] KeyCode dashKey;//enum 데이터라서 인스팩터에서 키를 등록 가능

    [Header("대시 Ui")]
    [SerializeField] GameObject objDashCoolTime;
    [SerializeField] Image imgFill;
    [SerializeField] TMP_Text TextCoolTime;
    private void OnDrawGizmos()
    {
        

        if (showGroundCheck == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLenght), groundColorCheck);//Color.red

        }

        //Debug.DrawLine();디버그의 체크용도로 씬 카메라에서 선을 그려줄수 있음
        //Gizmos.DrawSphere() 디버그보다 더 많은 시각효과 제공
        //Handles.DrawWireArc
    }
    #region 예시
    //private void OnTriggerEnter2D(Collider2D collision)//상대방의 콜라이더를 가져온,누가 실행시킨지 모름
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
        //HitBox = Class 데이터
        if (_type == HitBox.ehitBoxType.WallCheck)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                //해당 이름의 Layer에 닿았는지 체크
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
                //해당 이름의 Layer에 닿았는지 체크
                touchWall = false;

            }
        }
    }
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();//불러오기,호출
        box2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        dashEffpact = GetComponent<TrailRenderer>();
        dashEffpact.enabled = false;//끄기
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

    private void dash() //추가적인 메모 필요
    {
        if (dashTimer == 0.0f && dashCollTimer == 0.0f && 
            Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.F)) 
        {
            //Input.GetKeyDown(dashKey);//이런식으로 가능
            dashTimer = dashTime;
            dashCollTimer = dashTime;
            verticalVelocity = 0;
            dashEffpact.enabled = true;

            rigid.velocity = new Vector2(transform.localScale.x > 0 ? -dashSpeed : dashSpeed,0.0f);
            #region 다른 형태
            //if (transform.localScale.x > 0f) //왼쪽
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
    /// 해당 함수로 땅에 닿았는지 확인하는 코드
    /// </summary>
    private void checkGround() 
    {
        isGround = false;
        if (verticalVelocity > 0)//예외처리
        {
            return; 
        }
        //if (gameObject.CompareTag("Player") == true) //태그는 string으로 대상의 택그를 구분

        //float.PositiveInfinity
        //Layer int로 대상의 레이어를 구분
        //Layer의 int와 공통적으로 활용하는int와 다름
        //Wall Layer,Ground Layer
        RaycastHit2D hit =
        //Physics2D.Raycast(transform.position,Vector2.down,
        //groundCheckLenght,LayerMask.GetMask("Ground"));
        //// new Vector2(0,-1)//GetMask(안에 2개 이상의 데이터 넣기; 가능)

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
                dashEffpact.Clear();//데이터 삭제
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
            //2(타이머)/2(최대 타이머) = 1/0.5/0
            //dashCollTime = 2초, 스킬을 쓰면 0. 점점 1이 되어가야 함
            imgFill.fillAmount = 1 - dashCollTimer / dashCollTime;
            TextCoolTime.text = dashCollTimer.ToString("F1");
            //F0 = 1 , F1 =1.0 , F2 1.00
        }
    }

    /// <summary>
    /// 특정 키를 사용해서 움직이는 기능
    /// </summary>
    private void moving() 
    {
        if (wallJumpTimer > 0.0f || dashTimer > 0.0f)//밑에 코드 동작 못하게 하는 기능
        {
            return;
        }
        //좌우키를 누르는 좌우로 움직인다

        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        //a Left a key -1 / d, Raight a key 1
        //아무것도 입력하지 않으면 0

        moveDir.y = rigid.velocity.y;
        #region moveDir.y 메모
        //unity라이프사이클 순서 때문에 y가 초기화 되서
        //moveDir.y = rigid.velocity.y; 이렇게 적으면 Y값이 계속 더해져 가속도를 구현 할수 잇다
        #endregion 
        //슈팅게임 만들때는 오브젝트 코드에 의해 순간이동 하게 만듬
        //쿨리에 의해서 이동

        rigid.velocity = moveDir;//moveDir.y의 값이 0이면 값을 초기화 해서 천천히 내려감
        #region 메모
        //velocity는 rigid에 관련된기능이라 Time.deltaTime이 필요 없다
        //rigid == Rigidbody2D에는 AddForce같은 다양한 기능이 있다
        #endregion
        #region 중력조절 메모
        //Physics2D.gravity = moveDir;
        #endregion
    }

    private void checkAnim()
    {
        #region scale을 이용한 뒤집기
        //    Vector3 scale = transform.localScale;
        //    if (moveDir.x < 0f && scale.x!=1.0f)//왼쪽 0f인 이유는 <- moving() 함수참조
        //    {
        //        scale.x = 1.0f;
        //        transform.localScale = scale;
        //        Debug.Log("");
        //    }
        //    else if (moveDir.x > 0f && scale.x != -1.0f)//오른쪽
        //    {
        //        scale.x = -1.0f;
        //        transform.localScale = scale;
        //    }
        //scale.x != -1.0f<- 예외처리
        #endregion
        //transform.localPosition:부모의 월드 기준의 위치를 가져올때
        //transform.Position:월드 기준의 위치를 가져올때
        Vector2 mouseWorldPos = camMain.ScreenToWorldPoint(Input.mousePosition);//Scene기준이 아닌 Game(carse 기준)
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
    /// 점프하는 기능
    /// </summary>
    private void jump() 
    {
        #region GetKey 입력 타이밍 확인
        //if (Input.GetKeyDown(KeyCode.Space)) { Debug.Log("GetKeyDown"); }
        //if (Input.GetKeyUp(KeyCode.Space)) { Debug.Log("GetKeyUp"); }
        //if (Input.GetKey(KeyCode.Space)) { Debug.Log("GetKey"); }
        #endregion
        #region 다른 예시
        //if (isGround==true && isJump == false && Input.GetKeyDown(KeyCode.Space)) 
        //{

        //    rigid.AddForce(new Vector2(0,jumpForce),ForceMode2D.Impulse);//(x=1 ,y=0)
        //    //ForceMode2D.Impulse = 갑작스러운 힘의 작용
        //    //rigid.AddForce(new Vector2(10,0));//(x=10 ,y=0)//지긋이 미는힘
        //    isJump = true;//예외처리
        #region 2단 점프 예시
        //    //Vector2 forse = rigid.velocity;
        //    //forse.y = 0;
        //    //rigid.velocity = forse;
        #endregion
        //}
        #endregion
        if (isGround == false)//공중에 떠있는 상태라면,                    
        {
            //벽에 붙어있고 벽을 향해 플레이어가 방향키를 누르고 있는데 점프를 누르면
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
    /// Rigidbody2D에 중력을 0으로하고 해당 함수로 중력을 재현함
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
            dir.x *= -1f;//반대방향//-1로 함으로서 x값을 반대로 부여
            rigid.velocity = dir;

            verticalVelocity = jumpForce * 0.5f;
            //일정시간 유저가 입력할수 없어야 벽을 발로 참 x값을 볼수 있음
            //입력불가,타이머를 작동시켜야함
            wallJumpTimer = wallJumpTime;
        }
        else if (isGround == false) //공중에 떠있는 상태
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;//-9.81<- 프로젝트 설정 기준
            //음수를 더함으로써 중력의 가속도를 구현한다
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
             verticalVelocity = 0;//예외처리 필요
        }
        rigid.velocity = new Vector2(rigid.velocity.x, verticalVelocity);
    }
    /// <summary>
    /// 애니메이션 구동 값 부여
    /// </summary>
    private void doAnim() 
    {
        anim.SetInteger("Horizontal",(int)moveDir.x);//int 형변환 후 Horizontal에 수치 부여
        anim.SetBool("isGrund", isGround);//boll형태여서 o/x형태로 부여
    }

    private void initUi() 
    {
        objDashCoolTime.SetActive(false);
        imgFill.fillAmount = 0;
        TextCoolTime.text = "";
    }

    
}