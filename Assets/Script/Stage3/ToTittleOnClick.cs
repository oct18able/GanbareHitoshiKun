using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTittleOnClick : MonoBehaviour
{
    //クリックするとタイトル画面へ移動
    public void OnClick()
    {
        SceneManager.LoadScene("Title");
    }


}
