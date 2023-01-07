using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationUiPop : MonoBehaviour
{
    //设置 说明 Ui
    //音乐 音效大小 调整 
    //Ui 位置 水平 竖直 调整
    //游戏其他说明 
    public Slider musicSlider;
    public Slider musicEffectSlider;
    public Slider joyAxis_XSlider;
    public Slider joyAxis_YSlider;
    float buttonCtrlMoveSpeed = 8;
    void Start()
    {

    }
    void Awake()
    {
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.GameStop, null));


    }


    void OnDestroy()
    {
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.GameRun, null));

    }

    public void MusicValueSlider(float what)
    {
        SoundMgr.Instance.MusicVolume = musicSlider.value;
    }

    public void MusicEffectValueSlider(float what)
    {
        SoundMgr.Instance.EffectVolume = musicEffectSlider.value;
    }

    public void JoyAxis_XSlider(float what)
    {
        GameObject[] joys = GameObject.FindGameObjectsWithTag("Joy");
        for (int i = 0; i < joys.Length; i++)
        {
            joys[i].GetComponentInChildren<JoyStick>().transform.position =
                new Vector3(joyAxis_XSlider.value, joys[i].GetComponentInChildren<JoyStick>().transform.position.y,
                joys[i].GetComponentInChildren<JoyStick>().transform.position.z);
        }
    }

    public void JoyAxis_YSlider(float what)
    {
        GameObject[] joys = GameObject.FindGameObjectsWithTag("Joy");
        for (int i = 0; i < joys.Length; i++)
        {
            joys[i].GetComponentInChildren<JoyStick>().transform.position =
                new Vector3(joys[i].GetComponentInChildren<JoyStick>().transform.position.x, joyAxis_YSlider.value,
                joys[i].GetComponentInChildren<JoyStick>().transform.position.z);
        }
    }

    public void ButtonCtrlLeft(float what)
    {
        GameObject buttonUi = GameObject.FindGameObjectWithTag("ButtonUi");
        if (buttonUi == null)
        {
            return;
        }
        ETCButton[] allButton = buttonUi.transform.GetComponentsInChildren<ETCButton>();
        for (int i = 0; i < allButton.Length; i++)
        {
            allButton[i].transform.position += Vector3.left * buttonCtrlMoveSpeed;
        }
    }

    public void ButtonCtrlRight(float what)
    {
        GameObject buttonUi = GameObject.FindGameObjectWithTag("ButtonUi");
        if (buttonUi == null)
        {
            return;
        }
        ETCButton[] allButton = buttonUi.transform.GetComponentsInChildren<ETCButton>();
        for (int i = 0; i < allButton.Length; i++)
        {
            allButton[i].transform.position += -Vector3.left * buttonCtrlMoveSpeed;
        }
    }

    public void ButtonCtrlUp(float what)
    {
        GameObject buttonUi = GameObject.FindGameObjectWithTag("ButtonUi");
        if (buttonUi == null) 
        {
            return;
        }
        ETCButton[] allButton = buttonUi.transform.GetComponentsInChildren<ETCButton>();
        for (int i = 0; i < allButton.Length; i++)
        {
            allButton[i].transform.position += Vector3.up * buttonCtrlMoveSpeed;
        }
    }

    public void ButtonCtrlDown(float what)
    {
        GameObject buttonUi = GameObject.FindGameObjectWithTag("ButtonUi");
        if (buttonUi == null)
        {
            return;
        }
        ETCButton[] allButton = buttonUi.transform.GetComponentsInChildren<ETCButton>();
        for (int i = 0; i < allButton.Length; i++)
        {
            allButton[i].transform.position += -Vector3.up * buttonCtrlMoveSpeed;
        }
    }

    public void ButtonCloseThis(float what)
    {
        GameManager.Instance.RemoveUI(gameObject.transform.parent.gameObject);
    }


    public void ButtonPre()
    {
        PlayMusicCtrl.Instance.SetMusicCtrlSubtractAndPlay();
    }

    public void ButtonNext()
    {
        PlayMusicCtrl.Instance.SetMusicCtrlPlusPlusAndPlay();
    }


}
