using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//드래그가 가능한 인벤토리 아이템

public class Item : MonoBehaviour, IBeginDragHandler,IDragHandler,IEndDragHandler
{

    Transform canvas;//드래그 할때 슬롯UI 뒤로 그려지는 것을 방지하기 위해 잠깐 이용할 캔버스
    Transform beforeParant;//혹시 잘못된 위치에 드롭하게 되면 돌아오게 만들 위치
    //레이캐스트 타겟
    CanvasGroup canvasGroup;//자식들을 통합 관리하는 컴포넌트

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Start()
    {
        canvas = InventoryManager.Instance.CanvarsInvenTory;
    }

    /// <summary>
    /// idx 넘버를 전달 받으면 해당 아니템을 json으로 부터 검색하여 찾고
    /// 해당 아이템 데이터에서 필요한 정보만을 가져와서 해당 스크립트에 채워줌
    /// </summary>
    /// <param name="_idx">아이템 인덱스 넘버</param>
    public void SetItem(string _idx) 
    {

    }
    /// <summary>
    /// 드래그 시작 기능 -> IBeginDragHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        beforeParant = transform.parent;

        transform.SetParent(canvas);

        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position =eventData.position;//드래그중
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(transform.parent ==canvas) 
        {
            transform.position =beforeParant.position;
            transform.SetParent(beforeParant);
            //잘못된 위치면 이전 부모 위치로 복귀
        }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        //인벤토라 밖으로 나갔을때 삭제 하는 기능은 이 코드가 들어가 있는 iTem의 부모인 Slot이 아니라
        //더 위에 부모인 Inventory로 연결 필요
    }
}
