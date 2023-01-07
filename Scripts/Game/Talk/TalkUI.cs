using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Config;


public class TalkUI : MonoBehaviour
{
    //设置需要操作的控件
    public Image LeftHead;
    public Image RightHead;
    public Text showTalk;


    //对话数据
    TalkChapter mTalk;

    //对话显示需要的缓冲的字符串
    string mTalkStr;


    void Awake()
    {
        mTalk = null;
        mTalkStr = null;
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.GameStop, null));
    }
    void OnDestroy()
    {
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.GameRun, null));
    }

    void Start()
    {
        //test
        SetChapter("Chapter0");
        //test
    }


    void Update()
    {

    }


    public void SetChapter(string cn)
    {
        mTalk = new TalkChapter(cn);

        //播放第一句对话
        Refresh();
    }


    public void Refresh()
    {
        Talk talk = mTalk.Current();
        if (talk != null)
        {
            //刷新对话文字
            showTalk.text = talk.Text;
            if (mTalkStr == null)
            {
                int i = talk.Text.IndexOf(":");
                mTalkStr = talk.Text.Substring(0, i + 1);

                //启动定时器
                UnScaleTimerManger.Instance.Repeated("TalkSpeedTimerCall", 0.2f, TalkSpeedTimerCall);
            }

            showTalk.text = mTalkStr;

            //刷新对话头像
            Sprite sp = null;
            if (talk.UseSide == 1)
            {
                LeftHead.gameObject.SetActive(true);
                RightHead.gameObject.SetActive(false);
                showTalk.alignment = TextAnchor.UpperLeft;
                sp = Resources.Load<Sprite>("Pics/" + talk.LeftHead);
                LeftHead.sprite = sp;
            }
            else if (talk.UseSide == 2)
            {
                LeftHead.gameObject.SetActive(false);
                RightHead.gameObject.SetActive(true);
                showTalk.alignment = TextAnchor.UpperRight;

                sp = Resources.Load<Sprite>("Pics/" + talk.RightHead);
                RightHead.sprite = sp;
            }
        }
    }


    public void OnBgClick()
    {
        Talk talk = mTalk.Current();
        if (mTalkStr.Length < talk.Text.Length)
        {
            mTalkStr = talk.Text;
            showTalk.text = mTalkStr;
            UnScaleTimerManger.Instance.DeleteTimer("TalkSpeedTimerCall");
        }
        else
        {
            mTalkStr = null;
            if (mTalk.Next() == false)
            {
                UnScaleTimerManger.Instance.DeleteTimer("TalkSpeedTimerCall");
                GameManager.Instance.RemoveUI(gameObject);
                return;
            }
            Refresh();
        }
    }


    public void TalkSpeedTimerCall()
    {
        Talk talk = mTalk.Current();
        if (mTalkStr != null && mTalkStr.Length < talk.Text.Length)
        {
            mTalkStr = talk.Text.Substring(0, mTalkStr.Length + 1);
            showTalk.text = mTalkStr;
        }
        else
        {
            UnScaleTimerManger.Instance.DeleteTimer("TalkSpeedTimerCall");
        }
    }
}
