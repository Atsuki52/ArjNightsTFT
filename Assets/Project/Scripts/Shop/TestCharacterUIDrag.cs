using UnityEngine;
using UnityEngine.UI;

public class TestCharacterUIDrag : MonoBehaviour
{
    [SerializeField] Image mouse;
    [SerializeField] GameObject gameChara;
    [SerializeField] GameObject charaStock;

    public bool dragPossible;
    private bool dragCheck;
    private Vector3 preDragPos;
    private GameObject dragedCharaUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //情報のリセット
        dragPossible = false;
        dragCheck = false;
        preDragPos = new Vector3(0,0,0);
        dragedCharaUI = null;

        //マウスの検索
        mouse = GameObject.Find("Mouse").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //UI用
        //キャラクタードラッグ状態なら
        if (mouse.GetComponent<MouseCtr>().GetCharacterUIDrag())
        {
            //ドラッグされているオブジェクトと自身が同じでないなら終了
            
            if (mouse.GetComponent<MouseCtr>().GetCharaUIObject() != transform.gameObject)
            {
                return;
            }

            //ドラッグが始まった時
            if (dragCheck == false)
            {
                preDragPos = transform.position;
                dragCheck = true;
                dragedCharaUI = mouse.GetComponent<MouseCtr>().GetCharaUIObject();
            }

            //マウスに追従
            transform.position = mouse.GetComponent<MouseCtr>().GetScreenMousePos();


        }
        else
        {
            
            //キャラクターを配置可能か
            if (mouse.GetComponent<MouseCtr>().GetPutChara())
            {
                if (dragedCharaUI == transform.gameObject)
                {
                    //リザーブが満タンでないなら
                    if(charaStock.GetComponent<CharaStockCtr>().GetStockFull() == false)
                    {
                        GameObject cloneChara = Instantiate(gameChara);
                        cloneChara.transform.position = charaStock.GetComponent<CharaStockCtr>().GetResurvePos();
                        cloneChara.GetComponent<TestCharacterDrag>().SetResurveIndex(charaStock.GetComponent<CharaStockCtr>().GetResurveIndex());
                        charaStock.GetComponent<CharaStockCtr>().MathStockCount(1);
                        cloneChara.GetComponent<TestCharacterDrag>().SetResurveFlug();

                        dragCheck = false;
                        mouse.GetComponent<MouseCtr>().SetPutChara(false);
                        Destroy(gameObject);
                    }
                    
                }
                
            }
            //ドラッグをやめた時
            else if (dragCheck == true)
            {
                //元の位置に戻す
                transform.position = preDragPos;
                dragCheck = false;
                dragedCharaUI = null;
            }


        }


    }

}
