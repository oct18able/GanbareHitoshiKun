using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_CallHint : MonoBehaviour
{
    
    //ヒントを格納
    public GameObject hint;

    //クリック時にヒントの表示
    public void OnClickOpenHint()
    {
        hint.SetActive(true);
    }
        
}
