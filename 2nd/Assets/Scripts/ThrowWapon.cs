using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWapon : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 force;
    //각도X힘 = Vector
    bool right;
    bool isdone = false;
    
    private void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid.AddForce(force, ForceMode2D.Impulse);
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isdone =true;
        //wapon 생성시 콜라이더 끼리 부딪여 palyer가 뒤로 밀려서
        //방지하기 위해 설정에서 접촉 못하게 Layer를 껏음
        //Edit - ProjectSeting - Physics2D - LayerCollision
    }
    // Update is called once per frame
    void Update()
    {
        if (isdone == true) { return; }
        transform.Rotate(new Vector3(0,0,
            right == true ? -360f :360)*Time.deltaTime);
        
    }

    public void setForce(Vector2 _force, bool _isRight) 
    {
        force = _force;//Vector2
        right = _isRight;//bool

    }
}
