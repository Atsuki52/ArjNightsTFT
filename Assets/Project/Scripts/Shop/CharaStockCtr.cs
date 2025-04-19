using UnityEngine;
using System.Collections.Generic;

public class CharaStockCtr : MonoBehaviour
{
    [SerializeField] private List<Vector3> list;     //リザーブの1つひとつをオブジェクトとして保存
    private int stockCount;                             //いくつストックされているかを保存
    private bool[] blankBoxFlug = new bool[5];
    private int setIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //情報のリセット
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

    //ストックが満タンかを取得する
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

    //配置できるリザーブの座標を取得。必ずこの後にMathStockCount(1);
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

    //ストック数を変更する
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
