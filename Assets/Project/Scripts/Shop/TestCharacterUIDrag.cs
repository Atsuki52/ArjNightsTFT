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
        //���̃��Z�b�g
        dragPossible = false;
        dragCheck = false;
        preDragPos = new Vector3(0,0,0);
        dragedCharaUI = null;

        //�}�E�X�̌���
        mouse = GameObject.Find("Mouse").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //UI�p
        //�L�����N�^�[�h���b�O��ԂȂ�
        if (mouse.GetComponent<MouseCtr>().GetCharacterUIDrag())
        {
            //�h���b�O����Ă���I�u�W�F�N�g�Ǝ��g�������łȂ��Ȃ�I��
            
            if (mouse.GetComponent<MouseCtr>().GetCharaUIObject() != transform.gameObject)
            {
                return;
            }

            //�h���b�O���n�܂�����
            if (dragCheck == false)
            {
                preDragPos = transform.position;
                dragCheck = true;
                dragedCharaUI = mouse.GetComponent<MouseCtr>().GetCharaUIObject();
            }

            //�}�E�X�ɒǏ]
            transform.position = mouse.GetComponent<MouseCtr>().GetScreenMousePos();


        }
        else
        {
            
            //�L�����N�^�[��z�u�\��
            if (mouse.GetComponent<MouseCtr>().GetPutChara())
            {
                if (dragedCharaUI == transform.gameObject)
                {
                    //���U�[�u�����^���łȂ��Ȃ�
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
            //�h���b�O����߂���
            else if (dragCheck == true)
            {
                //���̈ʒu�ɖ߂�
                transform.position = preDragPos;
                dragCheck = false;
                dragedCharaUI = null;
            }


        }


    }

}
