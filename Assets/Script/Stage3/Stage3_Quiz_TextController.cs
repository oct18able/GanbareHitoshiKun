using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3_Quiz_TextController : MonoBehaviour
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


    //会話文をどれだけ読み進めたかのカウンター
    private int textCounter = 0;

    //背景キャンバスの格納
    public GameObject beforeCanvas; //泉
    public GameObject nextCanvas;   //遺跡の外、家

    //背景画像の格納
    public GameObject forestImg;    //遺跡の外
    public GameObject houseImg; //家

    //BGMの格納
    public GameObject beforeBGM;    //遺跡の中でのBGM
    public GameObject afterBGM;     //遺跡の外でのBMG(森、家)

    //タイトルボタン
    public GameObject toTitleButton;

    //二人の画像を格納
    public GameObject hitoshi_normal_img;
    public GameObject timu_normal_img;
    public GameObject hitoshi_dirty_img;
    public GameObject timu_dirty_img;
    public GameObject timu_shiny_img;

    //二人の画像を表示させるタイミングを行列で格納(普通ver、汚れたver)
    public int[] hitoshi_normal;
    public int[] timu_normal;
    public int[] hitoshi_dirty;
    public int[] timu_dirty;

    //森と家の画像切り替えカウンター
    public int[] backpicture_conter;

    //タイトルボタンを表示するカウンター
    public int titlebutton_counter;



    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    //開始時の処理
    void Start()
    {
        ChangeBGM();    //BGMの変更
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

        //背景キャンバスの切り替え
        ChangeCanvas(textCounter);

        //タイトルへ移動できるボタンの表示
        AppearToTitleButton(textCounter);

        //二人の画像の表示(普通ver、汚れたver)
        Display_Hitoshi_Normal_Img(hitoshi_normal, textCounter);
        Display_Timu_Normal_Img(timu_normal, textCounter);
        Display_Hitoshi_Dirty_Img(hitoshi_dirty, textCounter);
        Display_Timu_Dirty_Img(timu_dirty, textCounter);

        //会話文の進行をカウントする
        textCounter++;

        //森と家の画像切り替え
        ChangeImg(textCounter);

    }

   
    //背景キャンバスの切り替え関数
    void ChangeCanvas(int changeNumber)
    {
        if (changeNumber == backpicture_conter[0])
        {
            beforeCanvas.SetActive(false);
            nextCanvas.SetActive(true);
        }
    }

    //森と家の画像切り替え関数
    void ChangeImg(int changeNumber)
    {
        if (changeNumber == backpicture_conter[1])
        {
            forestImg.SetActive(false);
            houseImg.SetActive(true);
        }


    }

    //BGMの変更関数
    void ChangeBGM()
    {
        beforeBGM.SetActive(false); //クイズ中のBGMを消す
        afterBGM.SetActive(true);   //脱出してからのBGMを流す
    }


    
    //タイトル画面へ移動できるボタンのほう時間数
    void AppearToTitleButton(int changeNumber)
    {
        if (changeNumber == titlebutton_counter)
        {
            toTitleButton.SetActive(true);
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
                //hitoshi_normal_img.SetActive(true);
                timu_normal_img.SetActive(true);
                break;
            }
            else
            {
                timu_normal_img.SetActive(false);
            }
        }
    }


    //ヒトシの画像(汚れ)を表示する
    void Display_Hitoshi_Dirty_Img(int[] array, int changeNumber)
    {


        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == changeNumber)
            {
                hitoshi_dirty_img.SetActive(true);
                break;
            }
            else
            {
                hitoshi_dirty_img.SetActive(false);
            }
        }
    }

    //ティムの画像(汚れ)を表示する
    void Display_Timu_Dirty_Img(int[] array, int changeNumber)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == changeNumber)
            {
                timu_dirty_img.SetActive(true);
                break;
            }
            else
            {
                timu_dirty_img.SetActive(false);
            }
        }
    }



}
