using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    public GameObject se_buttonPush;

    public void OnClick()
    {
        se_buttonPush.SetActive(true);
        Invoke("LoadScene", 0.3f);
        
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("Intro");
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
