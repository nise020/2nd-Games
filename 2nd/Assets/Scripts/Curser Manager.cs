using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurserManager : MonoBehaviour
{
    [Header("Ŀ�� �̹���")]
    [SerializeField,Tooltip("0�� <color=red>����Ʈ</color>," +
        "1�� <color=red>Ŭ��</color> ")] List<Texture2D> cursors;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) 
        {
            Cursor.SetCursor(cursors[1],new Vector2(cursors[1].width * 0.5f ,
                cursors[1].height * 0.5f),CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursors[0], new Vector2(cursors[1].width * 0.5f,
               cursors[1].height * 0.5f), CursorMode.Auto);
        }

        
    }
}
