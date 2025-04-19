

using UnityEngine;
using System.Collections.Generic;
using System;


public class ShopCtr : MonoBehaviour
{
    struct GoodsInfo
    {
        public bool use;
        public Vector3 pos;
        public GameObject box;
        public int preNum;
    }

    [SerializeField] private GameObject stockCtr;           //�X�g�b�N�󋵂�ۑ�����ϐ�
    [SerializeField] private List<GameObject> shopGoodsObject;    //���i�o�^������ϐ�
    [SerializeField] private int goodsNumMax;               //���i�̍݌ɍő�l

    private GoodsInfo[] goodsBox = new GoodsInfo[2];                         //���i��ۑ�����ϐ�
    private int[] goodsNum = new int[4];
    private int[] goodsPosX = { 480 - 120, 480 + 230 };
    private int shopGoodsMax;
    private bool sellAllFlug;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //����ޏ��i�����邩�ۑ�
        shopGoodsMax = shopGoodsObject.Count;

        for (int i = 0; i < shopGoodsMax; i++)
        {
            goodsNum[i] = goodsNumMax;
        }

        //�ŏ���2���i����ׂ�
        for (int i = 0; i < 2; i++)
        {
            
            goodsBox[i].pos = new Vector3(goodsPosX[i], transform.position.y, transform.position.z);
            goodsBox[i].box = GoodsSupply(i);
            goodsBox[i].box.transform.parent = transform;
            goodsBox[i].use = true;
        }

        sellAllFlug = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //�S�Ĕ���Ă����珈�����Ȃ�
        if(sellAllFlug)
        {
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            //���i�����ꂽ�甄�ꂽ�ꏊ�ɕ�[����
            if (goodsBox[i].box == null)
            {
                GoodsStock(goodsBox[i].preNum);


                //�݌ɑS������Ă邩�̌���
                int sellAll = shopGoodsMax;
                for (int n = 0; n < shopGoodsMax; n++)
                {
                    if (goodsNum[n] <= 0)
                    {
                        sellAll--;
                    }
                }
                if (sellAll > 0)
                {
                    goodsBox[i].pos = new Vector3(goodsPosX[i], transform.position.y, transform.position.z);
                    goodsBox[i].box = GoodsSupply(i);
                    if(goodsBox[i].box != null)
                    {
                        goodsBox[i].box.transform.parent = transform;
                    }
                    
                    //goodsBox[i].use = false;
                }
                else
                {
                    sellAllFlug = true;
                }




            }
        }
    }

    private GameObject GoodsSupply(int index)
    {
        

        int rand = 0;
        int debugNum = 0;
        while(true)
        {
            rand = UnityEngine.Random.Range(0, shopGoodsMax);
            int goodsCheck = 0;
            for (int i = 0; i < 2; i++)
            {
                if (goodsBox[i].preNum == rand)
                {
                    goodsCheck--;
                }
            }

            if (goodsNum[rand] + goodsCheck > 0)
            {
                Debug.Log("Natori_CanPutNum:" + (goodsNum[rand] + goodsCheck));
                break;
            }
            debugNum++;
            if(debugNum > 100)
            {
                Debug.Log("Natori_GoodsSetAll");
                return null;
            }
        }



        GameObject goods = Instantiate(shopGoodsObject[rand], goodsBox[index].pos, Quaternion.identity);
        goods.transform.parent = transform;
        goodsBox[index].preNum = rand;

        return goods;
    }

    public void ShopReroll()
    {
        for (int i = 0; i < 2; i++)
        {
            
            GameObject objectBox = GoodsSupply(i);
            if(objectBox == null)
            {
                break;
            }
            Destroy(goodsBox[i].box);
            goodsBox[i].box = objectBox;
        }
        //Debug.Log("Reroll");
    }

    //���i�����ꂽ��݌ɂ����炷
    public void GoodsStock(int i)
    {
        if(goodsNum[i] > 0)
        {
            goodsNum[i]--;
            
        }
        Debug.Log("Natori_GoodsNum:" + i + "/Stock�F" + goodsNum[i]);
    }
}
