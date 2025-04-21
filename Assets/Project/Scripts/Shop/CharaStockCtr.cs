using UnityEngine;
using System.Collections.Generic;

public class CharaStockCtr : MonoBehaviour
{
    [SerializeField] private List<Vector3> list;     //���U�[�u��1�ЂƂ��I�u�W�F�N�g�Ƃ��ĕۑ�
    private int stockCount;                             //�����X�g�b�N����Ă��邩��ۑ�
    private bool[] blankBoxFlug = new bool[5];
    private int setIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //���̃��Z�b�g
        stockCount = 0;
        for(int i = 0; i < 5; i ++)
        {
            blankBoxFlug[i] = true;
        }
        setIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�X�g�b�N�����^�������擾����
    public bool GetStockFull()
    {
        if(stockCount >= list.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //�z�u�ł��郊�U�[�u�̍��W���擾�B�K�����̌��MathStockCount(1);
    public Vector3 GetResurvePos()
    {
        for (int i = 0; i < 5; i++)
        {
            if (blankBoxFlug[i])
            {
                setIndex = i;
                blankBoxFlug[i] = false;
                return list[i];
            }
        }
        return new Vector3(0,0,0);
    }

    public int GetResurveIndex()
    {
        return setIndex;
    }

    //�X�g�b�N����ύX����
    public void MathStockCount(int sign)
    {
        if(sign > 0)
        {
            if(stockCount > 5 - 1)
            {
                return;
            }
        }
        else if (sign < 0)
        {
            if (stockCount < 0 + 1)
            {
                return;
            }
        }

        stockCount = stockCount + (1 * sign);
    }

    public void SetBlankBoxIndex(int index)
    {
        blankBoxFlug[index] = true;
    }
}
