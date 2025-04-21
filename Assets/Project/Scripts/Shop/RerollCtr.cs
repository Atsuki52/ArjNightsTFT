using UnityEngine;
using UnityEngine.UI;


public class RerollCtr : MonoBehaviour
{
    [SerializeField] Image mouse;
    [SerializeField] GameObject shop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //UI用
        //ボタンに触った状態なら
        if (mouse.GetComponent<MouseCtr>().GetButtonTauch())
        {
            //自分をタッチされたら
            if (mouse.GetComponent<MouseCtr>().GetButtonName() == transform.gameObject.name)
            {
                //リロール指示
                shop.GetComponent<ShopCtr>().ShopReroll();

                //触った情報をリセット
                mouse.GetComponent<MouseCtr>().SetButtonTauch(false);
            }
        }
    }
}
