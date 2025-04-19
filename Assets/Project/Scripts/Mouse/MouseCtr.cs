using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseCtr : MonoBehaviour
{
    [SerializeField] private Camera gameCamera;     //���C���J��������̃X�N���[�����W���擾���邽�߂̕ϐ�
    [SerializeField] private GameObject charaStock;
    [SerializeField] private GameObject shop;

    private bool dragFlag;                          //�Q�[�����Ńh���b�O���s���Ă��邩�̔���t���O
    private bool dragUIFlag;                        //UI���Ńh���b�O���s���Ă��邩�̔���t���O

    private bool putCharaFlag;                      //�h���b�O�������̂�u���邩�̔���t���O
    private Vector3 underTile;                      //�}�E�X�̐^���ɂ���^�C���̈ʒu��ۑ�����ϐ�
    private GameObject dragChara;                   //�h���b�O���Ă���Q�[�����I�u�W�F�N�g��ۑ�����ϐ�
    private GameObject dragCharaUI;                 //�h���b�O���Ă���UI���I�u�W�F�N�g��ۑ�����ϐ�
    private bool buttonTauch;
    private string buttonName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //���̃��Z�b�g
        dragFlag = false;
        dragUIFlag = false;
        dragChara = null;
        dragCharaUI = null;
    }

    // Update is called once per frame
    void Update()
    {
//--------------------------------------------------------------------
    //�}�E�X�̈ړ�
        //�J�[�\���̈ʒu���Ƃ��ăJ�[�\���摜���ړ�
        transform.position = Input.mousePosition;
//--------------------------------------------------------------------
    //�L�����N�^�[�h���b�O
        //���N���b�N���ɐ^���ɂȂɂ����邩
        if (Input.GetMouseButtonDown(0))
        {
            //Ray���΂��ē����蔻��
            //RaycastAll�̈����iPointerEventData�j�쐬
            PointerEventData pointData = new PointerEventData(EventSystem.current);
            Vector3 pointWorld = new Vector3(0, 0, 0);

            //RaycastAll�̌��ʊi�[�pList
            List<RaycastResult> RayResult = new List<RaycastResult>();

            //PointerEventData�Ƀ}�E�X�̈ʒu���Z�b�g
            pointData.position = Input.mousePosition;
            pointWorld = gameCamera.ScreenToWorldPoint(Input.mousePosition);

            //RayCast�i�X�N���[�����W�j
            EventSystem.current.RaycastAll(pointData, RayResult);
            RaycastHit2D[] hit2D = Physics2D.RaycastAll(pointWorld, Vector3.forward);

            //UI�p
            foreach (RaycastResult result in RayResult)
            {
                //���g�̊m�F����
                if(result.gameObject.CompareTag("Player"))
                {
                    dragUIFlag = true;
                    dragCharaUI = result.gameObject;
                }
            }

            

            //�����UI�D��
            if (dragUIFlag == false)
            {
                //�Q�[���p
                foreach (RaycastHit2D result in hit2D)
                {
                    //���g�̊m�F����
                    if (result.transform.gameObject.CompareTag("Player"))
                    {
                        dragFlag = true;
                        dragChara = result.transform.gameObject;
                    }
                }
            }
            

        }
        //���N���b�N�����߂���
        else if(Input.GetMouseButtonUp(0))
        {
            //�h���b�O���Ă��邩�ƃh���b�O���̃I�u�W�F�N�g�����Z�b�g
            dragFlag = false;
            dragUIFlag = false;
            dragChara = null;
            dragCharaUI = null;
        }

        //�h���b�O���n�܂��Ă�����
        if(dragUIFlag)
        {
            
            //���U�[�u�����^���łȂ��Ȃ�z�u�\��
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
    //�{�^�����g��
        if(Input.GetMouseButtonDown(0))
        {
            //Ray���΂��ē����蔻��
            //RaycastAll�̈����iPointerEventData�j�쐬
            PointerEventData pointData = new PointerEventData(EventSystem.current);

            //RaycastAll�̌��ʊi�[�pList
            List<RaycastResult> RayResult = new List<RaycastResult>();

            //PointerEventData�Ƀ}�E�X�̈ʒu���Z�b�g
            pointData.position = Input.mousePosition;

            //RayCast�i�X�N���[�����W�j
            EventSystem.current.RaycastAll(pointData, RayResult);

            //UI�p
            foreach (RaycastResult result in RayResult)
            {
                //���g�̊m�F����
                if (result.gameObject.CompareTag("Button"))
                {
                    buttonTauch = true;
                    buttonName = result.gameObject.name;
                }
            }
        }

    }

    //�L�����N�^�[���h���b�O���Ă��邩���擾
    public bool GetCharacterDrag()
    {
        return dragFlag;
    }

    //�h���b�O���Ă���L�����N�^�[�����擾
    public GameObject GetCharaObject()
    {
        return dragChara;
    }

    //�L�����N�^�[UI���h���b�O���Ă��邩���擾
    public bool GetCharacterUIDrag()
    {
        return dragUIFlag;
    }

    //�h���b�O���Ă���L�����N�^�[UI�����擾
    public GameObject GetCharaUIObject()
    {
        return dragCharaUI;
    }

    //�X�N���[�����W�iUI�p�j�̃}�E�X�|�W�V�������擾
    public Vector3 GetScreenMousePos()
    {
        return transform.position;
    }

    //���[���h���W�i�Q�[���p�j�̃}�E�X�̃|�W�V�������擾
    public Vector3 GetWorldMousePos()
    {
        return gameCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    //�L�����N�^�[���z�u�\�����擾
    public bool GetPutChara()
    {
        return putCharaFlag;
    }

    //�L�����N�^�[�z�u�\�t���O���O������Z�b�g����
    public void SetPutChara(bool flag)
    {
        putCharaFlag = flag;
    }

    //�}�E�X�̉��ɂ���^�C���̈ʒu���擾
    public Vector3 GetUnderTilePos()
    {
        return underTile;
    }

    //�^�C���̃^�O�𒲂ׂĔz�u�\�����肷��
    void DragScanTile()
    {
        //Ray���΂��ē����蔻��
        Vector3 pointWorld = new Vector3(0, 0, 0);

        //pointWorld�Ƀ}�E�X�̈ʒu���Z�b�g
        pointWorld = gameCamera.ScreenToWorldPoint(Input.mousePosition);

        //RayCast�i�X�N���[�����W�j
        RaycastHit2D[] hit2D = Physics2D.RaycastAll(pointWorld, Vector3.forward);

        //�^�C���ɓ������Ă��邩�̃t���O
        bool hitTile = false;

        //�^�C���̌���
        foreach (RaycastHit2D result in hit2D)
        {
            ////�^�C���łȂ��Ȃ疳��
            //if (LayerMask.LayerToName(result.transform.gameObject.layer) != "Tile")
            //{
            //    Debug.Log("Natori_" + result.transform.gameObject.tag);
            //    putCharaFlag = false;
            //    continue;
            //}

            hitTile = true;

            //Flore�Ȃ�u����
            if (result.transform.gameObject.CompareTag("Flore"))
            {
                underTile = result.transform.position;
                putCharaFlag = true;
            }
            //Flore�ȊO�̃^�C���Ȃ�u���Ȃ�
            else if (result.transform.gameObject.CompareTag("NonPut") || result.transform.gameObject.CompareTag("Wall"))
            {
                putCharaFlag = false;
            }
        }

        //�^�C���ɓ������Ă��Ȃ��Ȃ�t���O���Z�b�g
        //hit2D�͔z��œ������Ă���I�u�W�F�N�g���L�����邽�ߓ������Ă��Ȃ��Ɖ����N���Ȃ����Ƃ����R
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
