using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MoveController : MonoBehaviour//바꿀시 ctrl+R+R
{

    //manager ,비동기적으로 호출이 왔을때 대응
    //controller ,updater 사용 동기적으로 호출이 오지 않더라도 타 기능을 불러서 사용하는 경우가 많음
    [Header("플레이어 이동 및 점프")]
    Rigidbody2D rigid;//null
    CapsuleCollider2D coll;
    Animator anim;
    Vector3 moveDir;
    float verticalVelocity =0;//수직으로 떨어지는 힘

    [SerializeField] float jumpForce;
    [SerializeField] float moveSpeed;

    [SerializeField] bool showGroundCheck;
    [SerializeField] float groundCheckLenght;//이 길아가 게임에서 얼마만큼의 길이가 육안으러 고기전에느,알수 없음
    [SerializeField] Color groundColorCheck;

    [SerializeField] bool isGround;//인스팩터에서 플레이어가 플랫폼 타일에 착지 했는지 확인용
    bool isJump;



    private void OnDrawGizmos()
    {
        if (showGroundCheck == true)
        {
            Debug.DrawLine(transform.position, transform.position - new Vector3(0, groundCheckLenght), groundColorCheck);//Color.red

        }

        //Debug.DrawLine();디버그로 체크용도로 씬 카메라에서 선을 그려줄수 있음
        //Gizmos.DrawSphere() 디버그보다 더 많은 시각효과 제공
        //Handles.DrawWireArc
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();//불러오기,호출
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
        //Layer int로 대사ㅇ의 레이어를 구분
        //Layer의 int와 공통적으로 활용하는int와 다름
        //Wall Layer,Ground Layer
        RaycastHit2D hit =
        Physics2D.Raycast(transform.position,Vector2.down,
        groundCheckLenght,LayerMask.GetMask("Ground"));
        // new Vector2(0,-1)//GetMask(안에 2개 이상의 데이터 넣기; 가능)

        if (hit)
        {
            isGround = true;
        }
        

    }
    /// <summary>
    /// 특정 키를 사용해서 움직이는 기능
    /// </summary>
    private void moving() 
    {
        //좌우키를 누르는 좌우로 움직인다
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;//a Left a key -1 / d, Raight a key 1 아무것도 입력 안하면 0
        moveDir.y = rigid.velocity.y;
        #region moveDir.y 메모
        //unity라이프사이클 순서 때문에 y가 초기화 되서
        //moveDir.y = rigid.velocity.y; 이렇게 해야 가속도가 붙는다
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
    /// Rigidbody2D에 중력을 0으로하고 해당 함수로 중력을 재현함
    /// </summary>
    private void checkGravity() 
    {
        if (isGround == false) //공중에 떠있는 상태
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
        anim.SetInteger("Horizontal",(int)moveDir.x);
        anim.SetBool("isGrund", isGround);
    }
}