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
        //UI�p
        //�{�^���ɐG������ԂȂ�
        if (mouse.GetComponent<MouseCtr>().GetButtonTauch())
        {
            //�������^�b�`���ꂽ��
            if (mouse.GetComponent<MouseCtr>().GetButtonName() == transform.gameObject.name)
            {
                //�����[���w��
                shop.GetComponent<ShopCtr>().ShopReroll();

                //�G�����������Z�b�g
                mouse.GetComponent<MouseCtr>().SetButtonTauch(false);
            }
        }
    }
}
