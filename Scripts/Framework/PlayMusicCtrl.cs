using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicCtrl
{
    float musicVolume = 0.5f;
    public int musicCtrl = 0;
    //µ¥ÀýÄ£Ê½
    static PlayMusicCtrl sInstance;
    public static PlayMusicCtrl Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new PlayMusicCtrl();
            }
            return sInstance;
        }
    }




    public void ChangeMusic(int musicCtrl)
    {
        switch (musicCtrl)
        {
            case 0: SoundMgr.Instance.PlayMusic("Sound/Music/Moonrise"); break;
            case 1: SoundMgr.Instance.PlayMusic("Sound/Music/ÃÎÏëÀÖÍÁÖ®ÒÛ"); break;
            case 2: SoundMgr.Instance.PlayMusic("Sound/Music/³ÂÖÂÒÝ,HOYO-MiX - Hanachirusato »ªÉ¢Ö®Ôµ"); break;
            case 3: SoundMgr.Instance.PlayMusic("Sound/Music/The Starlit Past ÐÇÖªÏþµÄ¾ÉÃÎ"); break;
            case 4: SoundMgr.Instance.PlayMusic("Sound/Music/Clouds"); break;
            case 5: SoundMgr.Instance.PlayMusic("Sound/Music/HOYO-MiX - Undersea Encounters ¾¨²¨ö¾ÀË"); break;
            case 6: SoundMgr.Instance.PlayMusic("Sound/Music/³ÂÖÂÒÝ,HOYO-MiX - Fiery Pursuit ÇýµÐÖð±±"); break;
        }
    }

    public void Run()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetMusicCtrlPlusPlusAndPlay();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetMusicCtrlSubtractAndPlay();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.UpArrow))
        {
            musicVolume += 0.002f;
            SoundMgr.Instance.MusicVolume = musicVolume;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            musicVolume -= 0.002f;
            SoundMgr.Instance.MusicVolume = musicVolume;
        }

         
    }

    public void  SetMusicCtrlPlusPlusAndPlay()
    {
        musicCtrl += 1;
        if (musicCtrl > 6)
        {
            musicCtrl = 0;
        }
        ChangeMusic(musicCtrl);
    }


    public void SetMusicCtrlSubtractAndPlay()
    {
        musicCtrl -= 1;
        if (musicCtrl < 0)
        {
            musicCtrl = 6;
        }
        ChangeMusic(musicCtrl);
    }

}
