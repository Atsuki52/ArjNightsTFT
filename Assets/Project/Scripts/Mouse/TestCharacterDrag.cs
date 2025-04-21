using UnityEngine;
using UnityEngine.UI;

public class TestCharacterDrag : MonoBehaviour
{
    [SerializeField] GameObject mouse;               //カーゾルの情報を保存する変数
    [SerializeField] GameObject charaStockCtr;

    private bool dragCheck;                     //ドラッグされているかのフラグ
    private Vector3 preDragPos;                 //ドラッグ前の座標を保存する変数
    private GameObject dragedChara;             //ドラッグされているキャラクターと自身を比べるための変数
    private bool putStageFlag;                //キャラクターがステージに配置済みか示すフラグ
    private bool putResurveFlag;                //キャラクターがリザーブに配置済みかを示すフラグ
    private int resurveIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //情報のリセット
        dragCheck = false;
        preDragPos = new Vector3(0, 0, 0);
        dragedChara = null;
        putStageFlag = false;

        //マウスの検索
        mouse = GameObject.Find("Mouse");
    }

    // Update is called once per frame
    void Update()
    {


        //ゲーム用
        //ドラッグされているオブジェクトが自身と同じでないなら終了
        if (mouse.GetComponent<MouseCtr>().GetCharaObject() != transform.gameObject && mouse.GetComponent<MouseCtr>().GetCharacterDrag() == true)
        {
            return;
        }

        //キャラクタードラッグ状態なら
        if (mouse.GetComponent<MouseCtr>().GetCharacterDrag())
        {
            //ドラッグが始まった時
            if (dragCheck == false)
            {
                //ドラッグされているゲームオブジェクトを取得
                dragedChara = mouse.GetComponent<MouseCtr>().GetCharaObject();

                //ドラッグされているゲームオブジェクトがすでにステージに配置されていたら以下の処理をしない
                if(dragedChara.GetComponent<TestCharacterDrag>().putStageFlag == true)
                {
                    dragedChara = null;
                    return;
                }

                //ドラッグ前の座標を保存
                this.preDragPos = this.transform.position;

                //ドラッグされていることを保存
                dragCheck = true;

                //自身のコライダーを消す
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            //自身をカーソルに追従させる
            Vector3 mousePos = mouse.GetComponent<MouseCtr>().GetWorldMousePos();
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        }
        else
        {

            //キャラクターを配置可能か
            if (mouse.GetComponent<MouseCtr>().GetPutChara())
            {
                //ドラッグされているオブジェクトが自身と同じなら
                if(dragedChara == transform.gameObject)
                {
                    //真下のタイルの座標に配置する
                    transform.position = mouse.GetComponent<MouseCtr>().GetUnderTilePos();
                    dragCheck = false;
                    mouse.GetComponent<MouseCtr>().SetPutChara(false);
                    transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;

                    //リザーブ数を減らす
                    charaStockCtr.GetComponent<CharaStockCtr>().MathStockCount(-1);
                    charaStockCtr.GetComponent<CharaStockCtr>().SetBlankBoxIndex(resurveIndex);

                    //ステージに配置したことを保存
                    putStageFlag = true;
                    dragedChara = null;
                }
            }
            //ドラッグをやめた時
            else if (dragCheck == true)
            {
                //元の位置に戻す
                this.transform.position = this.preDragPos;
                dragCheck = false;
                dragedChara = null;
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
            
        }
    }

    public void SetResurveFlug()
    {
        putResurveFlag = true;
    }

    public void SetResurveIndex(int index)
    {
        resurveIndex = index;
    }

    public int GetResurveIndex()
    {
        return resurveIndex;
    }
}
