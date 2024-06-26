using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//IPointerEnterHandler 같은것들
using UnityEngine.UI;

//아이템을 드래그 하거나 드래그해서 놓으면 이 스크립트가 드래그 
//아이템을 파악하여 자식으로 분류해줌

public class InventorySlot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IDropHandler
//IPointerDownHandler, IPointerUpHandler <- 마우스가 해당영역에 들어가거나 나갈때 기능
{
    //EventSystem가 하이라키에 있어야 기능함(Canvers 생성시)


    Image imgSlot;
    RectTransform rect;

    private void Awake()
    {
        imgSlot = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 마우스가 인벤토리 영역 들어갈때
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)//이런 함수를 사용하면 최적화에 좋다
    {
        //마우스가 인벤토리 영역 들어갈때
        imgSlot.color = Color.red;
    }
    /// <summary>
    /// 마우스가 인벤토리 영역 나갈때
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //마우스가 인벤토리 영역 나갈때
        imgSlot.color = Color.white;
    }
    /// <summary>
    /// 이벤트 시스템으로 인해 그래그 되는 대상이 이 스크립트 위에서
    /// 드롭되게 되면 해당 드롭 오브젝트를 나의 자식으로 변경합니다
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null) 
        {
            eventData.pointerDrag.transform.SetParent(transform);//부모로 변경
            eventData.pointerDrag.transform.position = rect.position;//해당위치로 이동
        }
    }
}
