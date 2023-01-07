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

    //ªÒ»°”Œœ∑÷– ±≥æ∞“Ù¿÷ ∫Õ “Ù–ß AudioSource
    public AudioSource mBkg;
    public AudioSource mEffect;

    SoundMgr()
    {
        mBkg = null;
        mEffect = null;
    }


    public void Set(GameObject effObj = null)
    {
        //UI∏˙Ω⁄µ„…œAudioSource≤•∑≈±≥æ∞“Ù¿÷
        if (mBkg == null)
        {
            mBkg = GameManager.Instance.UIRoot.GetComponent<AudioSource>();
        }

        //“Ù–ßµƒ”¶∏√‘⁄πÿø®¿Ô√Ê
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


    //≤•∑≈“Ù¿÷
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


    //‘›Õ£≤•∑≈“Ù¿÷
    public void Pause()
    {
        mBkg.Pause();
    }


    //ª÷∏¥≤•∑≈
    public void Resume()
    {
        mBkg.UnPause();
    }

    //“Ù¡ø
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
                PlayEffect("Sound/Effects/XiangLing/œ„¡‚_π˛");//  œ„¡‚_π˛  œ„¡‚_π˛0   œ„¡‚_∫ﬂ   œ„¡‚_∫»     œ„¡‚_∫Ÿ
            }
            else if (inSelectRandom == 1)
            {
                PlayEffect("Sound/Effects/XiangLing/œ„¡‚_π˛0");//  œ„¡‚_π˛  œ„¡‚_π˛0   œ„¡‚_∫ﬂ   œ„¡‚_∫»     œ„¡‚_∫Ÿ
            }
            else if (inSelectRandom == 2) 
            {
              //  PlayEffect("Sound/Effects/XiangLing/œ„¡‚_∫ﬂ");//  œ„¡‚_π˛  œ„¡‚_π˛0   œ„¡‚_∫ﬂ   œ„¡‚_∫»     œ„¡‚_∫Ÿ
            }
            else if (inSelectRandom == 3) 
            {
                PlayEffect("Sound/Effects/XiangLing/œ„¡‚_∫»");//  œ„¡‚_π˛  œ„¡‚_π˛0   œ„¡‚_∫ﬂ   œ„¡‚_∫»     œ„¡‚_∫Ÿ
            }
            else if (inSelectRandom == 4) 
            {
                PlayEffect("Sound/Effects/XiangLing/œ„¡‚_∫Ÿ");//  œ„¡‚_π˛  œ„¡‚_π˛0   œ„¡‚_∫ﬂ   œ„¡‚_∫»     œ„¡‚_∫Ÿ
            }
        }
    }


}
