using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TureCamera_Pc: MonoBehaviour
{
    bool isDownLook;



    //相机轴偏移值
    public float OffsetY;
    public float OffsetZ;
    public GameObject mainCameFllowTag;

    //鼠标锁定
    public bool lockCursor;
    private int ScreenCenterX;
    private int ScreenCenterY;
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SetCursorPos(int x, int y);

    void Start()
    {
        //摄像机初始照向主角
        CalcCameraPos();
        Cursor.visible = true;
        ScreenCenterX = Screen.width / 2;
        ScreenCenterY = Screen.height / 2;

        isDownLook = false;
    }

    void Update()
    {
        ////检测当前鼠标坐标之下是否有 Ui 手指之下有Ui时 不进行 旋转屏幕操作
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return; 
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    //获取鼠标光标坐标 手指触摸
        //    Debug.Log(Input.mousePosition);
        //}

        ////检测手指触摸之下是否有Ui
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    //Check if finger is over a Ui element
        //    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //    {
        //        return;
        //    }
        //}

        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isDownLook = !isDownLook;

        }
        if (isDownLook == true)
        {
            OffsetY = Mathf.Lerp(OffsetY, 9.0f, 0.06f);
        }
    }

    void LateUpdate()
    {
        //检测当前鼠标坐标之下是否有 Ui 手指之下有Ui时 不进行 旋转屏幕操作
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}

        if (lockCursor == true)
        {
            SetCursorPos(ScreenCenterX, ScreenCenterY);
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.visible = !Cursor.visible;
            lockCursor = !Cursor.visible;
        }
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };

       


        //获取鼠标的前后移动调整上下值
        if (isDownLook == false)
        {
            float mouseAxisY = Input.GetAxis("Mouse Y");
            if (mouseAxisY > 0)
            {
                OffsetY -= 0.05f;
            }
            else if (mouseAxisY < 0)
            {
                OffsetY += 0.05f;
            }
            if (isDownLook == false)
            {
                OffsetY = Mathf.Clamp(OffsetY, -0.8f, 1.5f);
            }
        }




        float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScrollWheel > 0)
        {
            OffsetZ += 0.05f;
        }
        else if (mouseScrollWheel < 0)
        {
            OffsetZ -= 0.05f;
        }
        OffsetZ = Mathf.Clamp(OffsetZ, -10f, -0.8f);
        //计算摄像机的位置
        CalcCameraPos();
    }



    //计算摄像机的位置
    void CalcCameraPos()
    {
        Vector3 pos = new Vector3();
        //先计算摄像机的Z
        Vector3 heroBackD = -mainCameFllowTag.transform.forward;
        heroBackD = heroBackD * OffsetZ;
        //确定摄像机的Z
        pos = mainCameFllowTag.transform.position + heroBackD;
        //确定摄像机的Y
        pos.y += OffsetY;
        //设置给摄像机
        this.transform.position = pos;
        //让摄像机看向英雄
        this.transform.LookAt(mainCameFllowTag.transform);
    }
}

