using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage2_Story_TextController : MonoBehaviour
{
    //21行目まで、テキスト制御の変数
    public string[] scenarios;
    [SerializeField] Text uiText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string currentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int currentLine = 0;
    private int lastUpdateCharacter = -1;

    //カメラの格納
    public GameObject[] Camera;

    //会話文をどれだけ読み進めたかのカウンター
    private int textCounter = 0;

    //ドアの閉まるSEの格納変数
    public GameObject se_doorclose;

    //二人の画像を格納
    public GameObject hitoshi_normal_img;
    public GameObject timu_normal_img;
    public GameObject hitoshi_dirty_img;
    public GameObject timu_dirty_img;
    public GameObject timu_shiny_img;

    //二人の画像を表示させるタイミングを行列で格納
    public int[] hitoshi_normal;
    public int[] timu_normal;

    //暗転の際のカウンター
    public int blackScene;

    //ドアの閉まるSEのカウンター
    public int doorClose_SE;

    //場面の切り替え変数
    public int sceneChange_Number;



    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    //開始時の処理
    void Start()
    {
        SetNextLine();
    }

    //フレームごとの処理
    void Update()
    {
        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
            if (currentLine < scenarios.Length && Input.GetMouseButtonDown(0))
            {
                SetNextLine();
            }
        }
        else
        {
            // 完了してないなら文字をすべて表示する
            if (Input.GetMouseButtonDown(0))
            {
                timeUntilDisplay = 0;
            }
        }

        int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter)
        {
            uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }

    //次の行を表示する
    void SetNextLine()
    {
        currentText = scenarios[currentLine];
        timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;
        timeElapsed = Time.time;
        currentLine++;
        lastUpdateCharacter = -1;

        CameraChange(textCounter);
        SE_DoorClose(textCounter);

        //二人の画像の表示
        Display_Hitoshi_Normal_Img(hitoshi_normal, textCounter);
        Display_Timu_Normal_Img(timu_normal, textCounter);
        
        //会話文の進行をカウントする
        textCounter++;

        //会話終了ならば次の場面へ移動
        SceneChange(textCounter);

    }

    //場所移動の際、一度暗転する
    void CameraChange(int changeNumber)
    {
        if (textCounter == blackScene)
        {
            Camera[0].SetActive(false);
            Camera[2].SetActive(true);  //カメラ2は暗転(黒)
            Invoke("BlackCameraChange", 1.0f);
        }


    }

    //暗転から部屋へのカメラ移動の関数
    void BlackCameraChange()
    {
        Camera[2].SetActive(false); //カメラ2は暗転(黒)
        Camera[1].SetActive(true);
    }

    //場面の変化関数
    void SceneChange(int changeNumber)
    {
        if (changeNumber == sceneChange_Number)
        {
            SceneManager.LoadScene("Stage2_Question");
        }
    }

    

    //ドアの閉まるSEを流す関数
    void SE_DoorClose(int changeNumber)
    {
        if (changeNumber == doorClose_SE)
        {
            se_doorclose.SetActive(true);
        }
    }


    //ヒトシの画像(普通)を表示する
    void Display_Hitoshi_Normal_Img(int[] array, int changeNumber)
    {

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == changeNumber)
            {
                hitoshi_normal_img.SetActive(true);
                break;
            }
            else
            {
                hitoshi_normal_img.SetActive(false);
            }
        }

    }


    //ティムの画像(普通)を表示する
    void Display_Timu_Normal_Img(int[] array, int changeNumber)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == changeNumber)
            {
                timu_normal_img.SetActive(true);
                break;
            }
            else
            {
                timu_normal_img.SetActive(false);
            }
        }

    }





}
