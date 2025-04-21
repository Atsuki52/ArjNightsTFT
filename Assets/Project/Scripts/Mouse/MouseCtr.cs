using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseCtr : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;     //メインカメラからのスクリーン座標を取得するための変数
    [SerializeField] private GameObject charaStock;
    [SerializeField] private GameObject shop;

    private bool dragFlag;                          //ゲーム内でドラッグを行っているかの判定フラグ
    private bool dragUIFlag;                        //UI内でドラッグを行っているかの判定フラグ

    private bool putCharaFlag;                      //ドラッグしたものを置けるかの判定フラグ
    private Vector3 underTile;                      //マウスの真下にあるタイルの位置を保存する変数
    private GameObject dragChara;                   //ドラッグしているゲーム内オブジェクトを保存する変数
    private GameObject dragCharaUI;                 //ドラッグしているUI内オブジェクトを保存する変数
    private bool buttonTauch;
    private string buttonName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //情報のリセット
        dragFlag = false;
        dragUIFlag = false;
        dragChara = null;
        dragCharaUI = null;
    }

    // Update is called once per frame
    void Update()
    {
//--------------------------------------------------------------------
    //マウスの移動
        //カーソルの位置をとってカーソル画像を移動
        transform.position = Input.mousePosition;
//--------------------------------------------------------------------
    //キャラクタードラッグ
        //左クリック時に真下になにがいるか
        if (Input.GetMouseButtonDown(0))
        {
            //Rayを飛ばして当たり判定
            //RaycastAllの引数（PointerEventData）作成
            PointerEventData pointData = new PointerEventData(EventSystem.current);
            Vector3 pointWorld = new Vector3(0, 0, 0);

            //RaycastAllの結果格納用List
            List<RaycastResult> RayResult = new List<RaycastResult>();

            //PointerEventDataにマウスの位置をセット
            pointData.position = Input.mousePosition;
            pointWorld = gameCamera.ScreenToWorldPoint(Input.mousePosition);

            //RayCast（スクリーン座標）
            EventSystem.current.RaycastAll(pointData, RayResult);
            RaycastHit2D[] hit2D = Physics2D.RaycastAll(pointWorld, Vector3.forward);

            //UI用
            foreach (RaycastResult result in RayResult)
            {
                //中身の確認処理
                if(result.gameObject.CompareTag("Player"))
                {
                    dragUIFlag = true;
                    dragCharaUI = result.gameObject;
                }
            }

            

            //判定はUI優先
            if (dragUIFlag == false)
            {
                //ゲーム用
                foreach (RaycastHit2D result in hit2D)
                {
                    //中身の確認処理
                    if (result.transform.gameObject.CompareTag("Player"))
                    {
                        dragFlag = true;
                        dragChara = result.transform.gameObject;
                    }
                }
            }
            

        }
        //左クリックを辞めたら
        else if(Input.GetMouseButtonUp(0))
        {
            //ドラッグしているかとドラッグ中のオブジェクトをリセット
            dragFlag = false;
            dragUIFlag = false;
            dragChara = null;
            dragCharaUI = null;
        }

        //ドラッグが始まっていたら
        if(dragUIFlag)
        {
            
            //リザーブが満タンでないなら配置可能に
            if(charaStock.GetComponent<CharaStockCtr>().GetStockFull() == false)
            {
                putCharaFlag = true;
            }
        }
        else if (dragFlag == true)
        {
            DragScanTile();
        }

//--------------------------------------------------------------------- 
    //ボタンを使う
        if(Input.GetMouseButtonDown(0))
        {
            //Rayを飛ばして当たり判定
            //RaycastAllの引数（PointerEventData）作成
            PointerEventData pointData = new PointerEventData(EventSystem.current);

            //RaycastAllの結果格納用List
            List<RaycastResult> RayResult = new List<RaycastResult>();

            //PointerEventDataにマウスの位置をセット
            pointData.position = Input.mousePosition;

            //RayCast（スクリーン座標）
            EventSystem.current.RaycastAll(pointData, RayResult);

            //UI用
            foreach (RaycastResult result in RayResult)
            {
                //中身の確認処理
                if (result.gameObject.CompareTag("Button"))
                {
                    buttonTauch = true;
                    buttonName = result.gameObject.name;
                }
            }
        }

    }

    //キャラクターをドラッグしているかを取得
    public bool GetCharacterDrag()
    {
        return dragFlag;
    }

    //ドラッグしているキャラクター情報を取得
    public GameObject GetCharaObject()
    {
        return dragChara;
    }

    //キャラクターUIをドラッグしているかを取得
    public bool GetCharacterUIDrag()
    {
        return dragUIFlag;
    }

    //ドラッグしているキャラクターUI情報を取得
    public GameObject GetCharaUIObject()
    {
        return dragCharaUI;
    }

    //スクリーン座標（UI用）のマウスポジションを取得
    public Vector3 GetScreenMousePos()
    {
        return transform.position;
    }

    //ワールド座標（ゲーム用）のマウスのポジションを取得
    public Vector3 GetWorldMousePos()
    {
        return gameCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    //キャラクターが配置可能かを取得
    public bool GetPutChara()
    {
        return putCharaFlag;
    }

    //キャラクター配置可能フラグを外部からセットする
    public void SetPutChara(bool flag)
    {
        putCharaFlag = flag;
    }

    //マウスの下にあるタイルの位置を取得
    public Vector3 GetUnderTilePos()
    {
        return underTile;
    }

    //タイルのタグを調べて配置可能か判定する
    void DragScanTile()
    {
        //Rayを飛ばして当たり判定
        Vector3 pointWorld = new Vector3(0, 0, 0);

        //pointWorldにマウスの位置をセット
        pointWorld = gameCamera.ScreenToWorldPoint(Input.mousePosition);

        //RayCast（スクリーン座標）
        RaycastHit2D[] hit2D = Physics2D.RaycastAll(pointWorld, Vector3.forward);

        //タイルに当たっているかのフラグ
        bool hitTile = false;

        //タイルの検索
        foreach (RaycastHit2D result in hit2D)
        {
            ////タイルでないなら無視
            //if (LayerMask.LayerToName(result.transform.gameObject.layer) != "Tile")
            //{
            //    Debug.Log("Natori_" + result.transform.gameObject.tag);
            //    putCharaFlag = false;
            //    continue;
            //}

            hitTile = true;

            //Floreなら置ける
            if (result.transform.gameObject.CompareTag("Flore"))
            {
                underTile = result.transform.position;
                putCharaFlag = true;
            }
            //Flore以外のタイルなら置けない
            else if (result.transform.gameObject.CompareTag("NonPut") || result.transform.gameObject.CompareTag("Wall"))
            {
                putCharaFlag = false;
            }
        }

        //タイルに当たっていないならフラグリセット
        //hit2Dは配列で当たっているオブジェクトを記憶するため当たっていないと何も起きないことが理由
        if(hitTile == false)
        {
            putCharaFlag = false;
        }
    }

    public bool GetButtonTauch()
    {
        return buttonTauch;
    }

    public void SetButtonTauch(bool flug)
    {
        buttonTauch = flug;
    }

    public string GetButtonName()
    {
        return buttonName;
    }
}
