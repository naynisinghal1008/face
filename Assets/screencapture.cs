using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using System.IO;

public class screencapture : MonoBehaviour
{
    // Start is called before the first frame update
    private bool storagepermission=false;
    void Start()
    {
        if(!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        else
        {
            storagepermission=true;
        }
    }

    IEnumerator screenshot()
    {
        GameObject go = GameObject.Find("Canvas");

        go.SetActive(false);

        yield return new WaitForEndOfFrame();

        Texture2D cover = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);

        cover.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);

        cover.Apply();

        byte[] save = cover.EncodeToPNG();

        string ph = "harsh" + System.DateTime.Now.ToString("YYMMDDHHmmss")+".png";

        NativeGallery.Permission clickphoto = NativeGallery.SaveImageToGallery(cover,"filename","gallery",(success,path)=>
        {
            if(success)
            Debug.Log("FileSaved"+path);

            else
            Debug.Log("File Not Saved");
        });

        go.SetActive(true);
    }

    public void OnButtonClick()
    {
        if(storagepermission)
        {
            StartCoroutine(screenshot());
        }
    }
}
