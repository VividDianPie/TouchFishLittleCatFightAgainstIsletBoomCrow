using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MyVec2
{
    public float x, y;
    public MyVec2()
    {
        x = y = 0f;
    }

    public MyVec2(Vector2 vec)
    {
        x = vec.x;
        y = vec.y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }
}


public class JoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    //摇杆的背景图
    public GameObject JoyStickBG;

    //中间的杆
    public GameObject Stick;

    //中间杆儿
    Vector2 mSrcPos;

    Coroutine CoroutineJoyDown;

    Coroutine CoroutineJoyDrag;

    Vector2 centreToJoyDir;

    bool f_f;
    float rotateSpeed;

    void Start()
    {
        mSrcPos = Stick.GetComponent<RectTransform>().anchoredPosition;
    }


    void Update()
    {
        int randCound = Random.Range(0, 1000);
        if (randCound == 1)
        {
            f_f = !f_f;
        }
        if (f_f)
        {
            rotateSpeed += 0.5f;
        }
        else
        {
            rotateSpeed -= 0.5f;
        }
        Stick.transform.Rotate(Vector3.forward, Time.deltaTime*rotateSpeed);



        //Check if there is  a touch
        if (Input.touchCount == 0)
        {
            EventManager.Instance.DispatchEvent(new MyEvent(EEventType.ScreenFingerZero, null));
            Stick.GetComponent<RectTransform>().anchoredPosition = mSrcPos;

        }
    }

    //鼠标按下的时候
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 temp_pos = GetRelativePos(eventData.position);
        if (temp_pos == Vector2.zero) { return; };
        Vector2 pos = temp_pos;
        Stick.GetComponent<RectTransform>().anchoredPosition = pos;

        pos = GetInput(pos);
        //按下时 开启携程 持续派发事件 抛出遥感坐标方向
        CoroutineJoyDown = StartCoroutine(CoroutineGetJoyPosJoyDown(pos));
    }




    //正在拖动的时候
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 temp_pos = GetRelativePos(eventData.position);
        if (temp_pos == Vector2.zero) { return; };
        Vector2 pos = temp_pos;
        Stick.GetComponent<RectTransform>().anchoredPosition = pos;

        pos = GetInput(pos);
        if (CoroutineJoyDrag != null)
        {
            StopCoroutine(CoroutineJoyDrag);
        }
        if (CoroutineJoyDown != null)
        {
            StopCoroutine(CoroutineJoyDown);
        }
        CoroutineJoyDrag = StartCoroutine(CoroutineGetJoyPosJoyDrag(pos));
    }




    //鼠标抬起的时候
    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 temp_pos = GetRelativePos(eventData.position);
        if (temp_pos == Vector2.zero) { return; };
        Vector2 pos = temp_pos;
        //抬起时 关闭携程 重置 数据 停止抛出 遥感坐标方向·
        if (CoroutineJoyDown != null)
        {
            StopCoroutine(CoroutineJoyDown);
        }
        //拖动时 开启携程 持续派发事件 抛出遥感坐标方向
        if (CoroutineJoyDrag != null)
        {
            StopCoroutine(CoroutineJoyDrag);

        }

        Stick.GetComponent<RectTransform>().anchoredPosition = mSrcPos;
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.JoyStickUp, new MyVec2(Vector2.zero)));
    }


    //获取屏幕坐标相对于背景的坐标
    public Vector2 GetRelativePos(Vector2 ScreenPos)
    {
        Vector2 relativePos;
        //将一个屏幕空间点转换为 RectTransform 的本地空间中位于其矩形平面上的一个位置。
        RectTransformUtility.ScreenPointToLocalPointInRectangle(JoyStickBG.GetComponent<RectTransform>(), ScreenPos, null, out relativePos);

        //计算相对的偏移
        centreToJoyDir = relativePos - mSrcPos;

        if (centreToJoyDir.magnitude > 400)
        {
            return Vector2.zero;
        }


        float len = Mathf.Clamp(centreToJoyDir.magnitude, 0, 200 / 2);

        //偏移向量
        centreToJoyDir.Normalize();
        centreToJoyDir = centreToJoyDir * len;

        relativePos = mSrcPos + centreToJoyDir;

        return relativePos;
    }


    public Vector2 GetInput(Vector2 relativePos)
    {
        // Debug.Log("relativePos.x " + relativePos.x + "  " + "relativePos.y " + relativePos.y);
        return relativePos.normalized;
    }



    IEnumerator CoroutineGetJoyPosJoyDown(Vector2 pos)
    {
        while (true)
        {
            yield return 0;
            EventManager.Instance.DispatchEvent(new MyEvent(EEventType.JoyStickDown, new MyVec2(pos)));
        }
    }

    IEnumerator CoroutineGetJoyPosJoyDrag(Vector2 pos)
    {
        while (true)
        {
            EventManager.Instance.DispatchEvent(new MyEvent(EEventType.JoyStickDrag, new MyVec2(pos)));
            yield return 0;
        }
    }

    void AllFingerUp(Vector2 pos)
    {

        return;
    }
}



