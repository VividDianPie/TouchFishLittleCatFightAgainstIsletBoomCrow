using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr
{

    static SoundMgr sInstance = null;
    public static SoundMgr Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new SoundMgr();
            }
            return sInstance;
        }
    }

    //��ȡ��Ϸ�� �������� �� ��Ч AudioSource
    public AudioSource mBkg;
    public AudioSource mEffect;

    SoundMgr()
    {
        mBkg = null;
        mEffect = null;
    }


    public void Set(GameObject effObj = null)
    {
        //UI���ڵ���AudioSource���ű�������
        if (mBkg == null)
        {
            mBkg = GameManager.Instance.UIRoot.GetComponent<AudioSource>();
        }

        //��Ч��Ӧ���ڹؿ�����
        if (effObj != null)
        {
            mEffect = effObj.AddComponent<AudioSource>();
        }
        else
        {
            mEffect = GameManager.Instance.GameRoot.AddComponent<AudioSource>();
        }

        mBkg.volume = GameCfgMgr.Instance.MusicOn ? 0.8f : 0f;
        mEffect.volume = GameCfgMgr.Instance.EffectOn ? 1.0f : 0f;
    }


    //��������
    public bool PlayMusic(string musPath)
    {
        AudioClip ac = Resources.Load<AudioClip>(musPath);
        if (ac == null)
        {
            return false;
        }

        mBkg.Stop();
        mBkg.clip = ac;
        mBkg.Play();

        return true;
    }


    //��ͣ��������
    public void Pause()
    {
        mBkg.Pause();
    }


    //�ָ�����
    public void Resume()
    {
        mBkg.UnPause();
    }

    //����
    public float MusicVolume
    {
        get
        {
            return mBkg.volume;
        }
        set
        {
            mBkg.volume = value;
        }
    }

    public bool IsPlayMusic()
    {
        return mBkg.isPlaying;
    }


    public bool PlayEffect(string musPath)
    {
        AudioClip audioClip = Resources.Load<AudioClip>(musPath);
        if (audioClip == null)
        {
            return false;
        }
        mEffect.PlayOneShot(audioClip);
        return true;
    }


    public float EffectVolume
    {
        get
        {
            return mEffect.volume;
        }
        set
        {
            mEffect.volume = value;
        }
    }


    public void RandomPlayXianglingAAttackSound()
    {
        int inPlayRandom = Random.Range(0, 2);
        if (inPlayRandom == 0)
        {
            int inSelectRandom = Random.Range(0, 5);
            if (inSelectRandom == 0)
            {
                PlayEffect("Sound/Effects/XiangLing/����_��");//  ����_��  ����_��0   ����_��   ����_��     ����_��
            }
            else if (inSelectRandom == 1)
            {
                PlayEffect("Sound/Effects/XiangLing/����_��0");//  ����_��  ����_��0   ����_��   ����_��     ����_��
            }
            else if (inSelectRandom == 2) 
            {
              //  PlayEffect("Sound/Effects/XiangLing/����_��");//  ����_��  ����_��0   ����_��   ����_��     ����_��
            }
            else if (inSelectRandom == 3) 
            {
                PlayEffect("Sound/Effects/XiangLing/����_��");//  ����_��  ����_��0   ����_��   ����_��     ����_��
            }
            else if (inSelectRandom == 4) 
            {
                PlayEffect("Sound/Effects/XiangLing/����_��");//  ����_��  ����_��0   ����_��   ����_��     ����_��
            }
        }
    }


}
