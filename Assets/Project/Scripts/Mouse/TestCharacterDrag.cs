using UnityEngine;
using UnityEngine.UI;

public class TestCharacterDrag : MonoBehaviour
{
    [SerializeField] GameObject mouse;               //�J�[�]���̏���ۑ�����ϐ�
    [SerializeField] GameObject charaStockCtr;

    private bool dragCheck;                     //�h���b�O����Ă��邩�̃t���O
    private Vector3 preDragPos;                 //�h���b�O�O�̍��W��ۑ�����ϐ�
    private GameObject dragedChara;             //�h���b�O����Ă���L�����N�^�[�Ǝ��g���ׂ邽�߂̕ϐ�
    private bool putStageFlag;                //�L�����N�^�[���X�e�[�W�ɔz�u�ς݂������t���O
    private bool putResurveFlag;                //�L�����N�^�[�����U�[�u�ɔz�u�ς݂��������t���O
    private int resurveIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //���̃��Z�b�g
        dragCheck = false;
        preDragPos = new Vector3(0, 0, 0);
        dragedChara = null;
        putStageFlag = false;

        //�}�E�X�̌���
        mouse = GameObject.Find("Mouse");
    }

    // Update is called once per frame
    void Update()
    {


        //�Q�[���p
        //�h���b�O����Ă���I�u�W�F�N�g�����g�Ɠ����łȂ��Ȃ�I��
        if (mouse.GetComponent<MouseCtr>().GetCharaObject() != transform.gameObject && mouse.GetComponent<MouseCtr>().GetCharacterDrag() == true)
        {
            return;
        }

        //�L�����N�^�[�h���b�O��ԂȂ�
        if (mouse.GetComponent<MouseCtr>().GetCharacterDrag())
        {
            //�h���b�O���n�܂�����
            if (dragCheck == false)
            {
                //�h���b�O����Ă���Q�[���I�u�W�F�N�g���擾
                dragedChara = mouse.GetComponent<MouseCtr>().GetCharaObject();

                //�h���b�O����Ă���Q�[���I�u�W�F�N�g�����łɃX�e�[�W�ɔz�u����Ă�����ȉ��̏��������Ȃ�
                if(dragedChara.GetComponent<TestCharacterDrag>().putStageFlag == true)
                {
                    dragedChara = null;
                    return;
                }

                //�h���b�O�O�̍��W��ۑ�
                this.preDragPos = this.transform.position;

                //�h���b�O����Ă��邱�Ƃ�ۑ�
                dragCheck = true;

                //���g�̃R���C�_�[������
                transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }

            //���g���J�[�\���ɒǏ]������
            Vector3 mousePos = mouse.GetComponent<MouseCtr>().GetWorldMousePos();
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        }
        else
        {

            //�L�����N�^�[��z�u�\��
            if (mouse.GetComponent<MouseCtr>().GetPutChara())
            {
                //�h���b�O����Ă���I�u�W�F�N�g�����g�Ɠ����Ȃ�
                if(dragedChara == transform.gameObject)
                {
                    //�^���̃^�C���̍��W�ɔz�u����
                    transform.position = mouse.GetComponent<MouseCtr>().GetUnderTilePos();
                    dragCheck = false;
                    mouse.GetComponent<MouseCtr>().SetPutChara(false);
                    transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;

                    //���U�[�u�������炷
                    charaStockCtr.GetComponent<CharaStockCtr>().MathStockCount(-1);
                    charaStockCtr.GetComponent<CharaStockCtr>().SetBlankBoxIndex(resurveIndex);

                    //�X�e�[�W�ɔz�u�������Ƃ�ۑ�
                    putStageFlag = true;
                    dragedChara = null;
                }
            }
            //�h���b�O����߂���
            else if (dragCheck == true)
            {
                //���̈ʒu�ɖ߂�
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
