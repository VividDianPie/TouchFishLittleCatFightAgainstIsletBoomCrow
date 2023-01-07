using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Config;
using Newtonsoft.Json;

public class TalkChapter
{

    //�������汾�½ڵĶԻ�����
    Dictionary<int, Talk> mTalks;

    //��ǰ�����Ǿ�Ի�
    int mCurrent;

    //�Ի����е�id
    int[] mIds;

    //����
    public string ChapterName { get; protected set;}
    

    //�����ļ���
    public TalkChapter(string chapterCfg)
    {
        //��������
        TextAsset txt = Resources.Load<TextAsset>($"Configs/Scripts/{chapterCfg}");
        if (txt == null)
        {
            throw new System.Exception("�籾��ְ�����Ų߻�С��");
        }

        //��ֵ����
        ChapterName = txt.name;

        //�����л�
        mTalks = JsonConvert.DeserializeObject<Dictionary<int, Talk>>(txt.text);

        InitTalk();
    }


    public void InitTalk()
    {
        //��ȡ�Ի�id
        mIds = new int[mTalks.Count];
        //���ֵ��е� Key ֵ���㿪ʼ������ mIds��
        mTalks.Keys.CopyTo(mIds, 0);
        //��������
        Array.Sort(mIds);
        //���ó�ʼ��
        mCurrent = 0;
    }

    //�׳���ǰ�Ի�
    public Talk Current()
    {
        if (mTalks == null)
        {
            return null;
        }

        return mTalks[mIds[mCurrent]];
    }

    //��һ��
    public bool Next()
    {
        if (mCurrent >= mIds.Length - 1)
        {
            return false;
        }

        mCurrent++;
        return true;
    }

    //���öԻ�
    public void Reset()
    {
        mCurrent = 0;
    }

    //�����Ի�
    public bool IsFinish
    {
        get
        {
            return mCurrent == mIds.Length - 1;
        }
    }

    public int GetChapterLenth()
    {
        return mTalks.Count;
    }
}
