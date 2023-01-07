using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TureCamera : MonoBehaviour
{
    bool isDownLook;

    //相机轴偏移值
    public float OffsetY;
    public float OffsetZ;
    public GameObject mainCameFllowTag;

    //鼠标锁定
    private bool lockCursor;
    private int ScreenCenterX;
    private int ScreenCenterY;
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    public static extern int SetCursorPos(int x, int y);
    void Start()
    {
        //摄像机初始照向主角
        CalcCameraPos();
        Cursor.visible = true;
        lockCursor = false;
        ScreenCenterX = Screen.width / 2;
        ScreenCenterY = Screen.height / 2;

        isDownLook = false;

        //事件监测 向下视角是否按下
        EventManager.Instance.AddListener(EEventType.DownLook, IsDownLookEvent);

    }

    void Update()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };

        //大键盘 按键 1
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
        //鼠标锁定
        if (lockCursor == true)
        {
            //  SetCursorPos(ScreenCenterX, ScreenCenterY);
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.visible = !Cursor.visible;
            lockCursor = !Cursor.visible;
        }

        //检测当前屏幕之上是否有指定Ui
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };


        if (isDownLook == false)
        {
            //手指控制镜头上下移动
            //单只手指触摸时 Y 轴应该
            if (FingerSlideDir.Instance.TouchDirectionY1() != 0)
            {
                float mouseAxisY = 0; Input.GetAxis("Mouse Y");
                mouseAxisY = FingerSlideDir.Instance.TouchDirectionY1();
                if (mouseAxisY > 0)
                {
                    OffsetY -= 0.05f;
                }
                else if (mouseAxisY < 0)
                {
                    OffsetY += 0.05f;
                }
              
            }
            //两只手指触摸时 Y 轴应该
            if (FingerSlideDir.Instance.TouchDirectionY2() != 0)
            {
                float mouseAxisY = 0; Input.GetAxis("Mouse Y");
                mouseAxisY = FingerSlideDir.Instance.TouchDirectionY2();
                if (mouseAxisY > 0)
                {
                    OffsetY -= 0.05f;
                }
                else if (mouseAxisY < 0)
                {
                    OffsetY += 0.05f;
                }
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


    private void OnDestroy()
    {
        EventManager.Instance.DeleteListener(EEventType.DownLook, IsDownLookEvent);
    }



    public void IsDownLookEvent(MyEvent evt)
    {
        isDownLook = !isDownLook;

    }
}

