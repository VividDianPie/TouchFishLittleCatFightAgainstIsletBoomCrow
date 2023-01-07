using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using UnityEngine;
using UnityEngine.Networking;

namespace Utils
{
    //文件功能类
    class FileUtils
    {
        //Application.dataPath  游戏打包号的资源所在的文件夹，有些平台不允许写
        //Application.persistentDataPath   只读
        //Application.streamingAssetsPath  不允许写只读
        //通过WWW / WebRequst 
        //PlayerPrefs / ScriptableObject
        //PlayerPrefs.SetInt("Height", 190);
        //PlayerPrefs.HasKey("Height");
        //PlayerPrefs.GetInt("Height");
        //Application.temporaryCachePath 应用的零时文件目录（手机上卸载数据删除）


        //判断一个文件夹是否存在
        public static bool IsDirExist(string path)
        {
            return Directory.Exists(path);
        }


        //创建文件夹
        public static bool CreateDir(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
            }
            catch (Exception exp)
            {
                return false;
            }
            return true;
        }


        //删除一个文件夹
        public static bool DeleteDir(string path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception exp)
            {
                return false;
            }

            return true;
        }


        //判断一个文件是否存在
        public static bool IsFileExist(string path)
        {
            return File.Exists(path);
        }


        //删除一个文件
        public static bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception exp)
            {
                return false;
            }
            return true;
        }


        //从文件中读取
        public static int ReadFromFile(string path, out byte[] buf)
        {
            buf = null;
            try
            {
                int ret = -1;
                FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read);
                buf = new byte[fs.Length];
                ret = fs.Read(buf, 0, (int)fs.Length);
                fs.Close();
                return ret;
            }
            catch (Exception e)
            {
                return -1;
            }
            return -1;
        }


        //写入文件
        public static int WriteToFile(string path, byte[] buf)
        {
            if (buf != null)
            {
                try
                {
                    FileStream fs = File.Open(path, FileMode.Create, FileAccess.Write);
                    fs.Write(buf, 0, buf.Length);
                    fs.Close();
                }
                catch (Exception e)
                {
                    return -1;
                }
            }
            return -1;
        }


        public static Uri GetStreamingAssets(string path)
        {
            /*
     string filePath = "";

#if UNITY_STANDALONE
     filePath = "file:///" + Application.streamingAssetsPath + "/" + path;

#elif UNITY_ANDROID
     filePath = Application.streamingAssetsPath + "/" +  path;

#elif UNITY_IOS
     filePath = "file://" + Application.streamingAssetsPath + "/" + path;

#elif UNITY_WEBGL
            filePath = "file://" + Application.streamingAssetsPath + "/" + path;

#endif
     return filePath;

            return filePath;
            */
            return new Uri(Path.Combine(Application.streamingAssetsPath, path));
        }
    }
}
