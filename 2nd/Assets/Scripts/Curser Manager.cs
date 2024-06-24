using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurserManager : MonoBehaviour
{
    [Header("커서 이미지")]
    [SerializeField,Tooltip("0은 <color=red>디폴트</color>," +
        "1은 <color=red>클릭</color> ")] List<Texture2D> cursors;

    // Update is called once per frame
    void Update()
    {
        //마우스 커서 전환
        if (Input.GetKey(KeyCode.Mouse0)) 
        {
            Cursor.SetCursor(cursors[1],new Vector2(cursors[1].width * 0.5f ,
               cursors[1].height * 0.5f), CursorMode.Auto);
        }
        else//기본상태
        {
            Cursor.SetCursor(cursors[0], new Vector2(cursors[1].width * 0.5f,
               cursors[1].height * 0.5f), CursorMode.Auto);
        }

        
    }
}
