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

    //获取游戏中 背景音乐 和 音效 AudioSource
    public AudioSource mBkg;
    public AudioSource mEffect;

    SoundMgr()
    {
        mBkg = null;
        mEffect = null;
    }


    public void Set(GameObject effObj = null)
    {
        //UI跟节点上AudioSource播放背景音乐
        if (mBkg == null)
        {
            mBkg = GameManager.Instance.UIRoot.GetComponent<AudioSource>();
        }

        //音效的应该在关卡里面
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


    //播放音乐
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


    //暂停播放音乐
    public void Pause()
    {
        mBkg.Pause();
    }


    //恢复播放
    public void Resume()
    {
        mBkg.UnPause();
    }

    //音量
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
                PlayEffect("Sound/Effects/XiangLing/香菱_哈");//  香菱_哈  香菱_哈0   香菱_哼   香菱_喝     香菱_嘿
            }
            else if (inSelectRandom == 1)
            {
                PlayEffect("Sound/Effects/XiangLing/香菱_哈0");//  香菱_哈  香菱_哈0   香菱_哼   香菱_喝     香菱_嘿
            }
            else if (inSelectRandom == 2) 
            {
              //  PlayEffect("Sound/Effects/XiangLing/香菱_哼");//  香菱_哈  香菱_哈0   香菱_哼   香菱_喝     香菱_嘿
            }
            else if (inSelectRandom == 3) 
            {
                PlayEffect("Sound/Effects/XiangLing/香菱_喝");//  香菱_哈  香菱_哈0   香菱_哼   香菱_喝     香菱_嘿
            }
            else if (inSelectRandom == 4) 
            {
                PlayEffect("Sound/Effects/XiangLing/香菱_嘿");//  香菱_哈  香菱_哈0   香菱_哼   香菱_喝     香菱_嘿
            }
        }
    }


}
