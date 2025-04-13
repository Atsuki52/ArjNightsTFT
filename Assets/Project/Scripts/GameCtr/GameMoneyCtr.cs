using UnityEngine;
using UnityEngine.UI;

public class GameMoneyCtr : MonoBehaviour
{
    [SerializeField]
    private GameCtr gameCtr;

    [SerializeField]
    private Text moneyUiText;

    [Header("�e��ݒ�")]
    [SerializeField]
    private float incomeInterval; // �����Ԋu
    [SerializeField]
    private int incomeAmount; // �����z

    private int gameMoney;

    //�ꎞ�ϐ��Q
    private float time;

    void Start()
    {
        gameMoney = 0;
        time = 0.0f;
    }

    void FixedUpdate()
    {
        if(gameCtr.CurrentGameState == GameCtr.GameState.InGame)
        {
            time += Time.deltaTime;

            if(time > incomeInterval)
            {
                Add(incomeAmount);
                time = 0.0f;
            }
        }
    }

    #region ���₷�@���炷

    /// <summary>
    /// �����𑝂₷ UI�㏑��
    /// </summary>
    /// <param name="num"></param>
    public void Add(int num)
    {
        gameMoney += num;
        moneyUiText.text = $"{gameMoney:000}";
    }

    /// <summary>
    /// ���������炷 UI�㏑��
    /// </summary>
    /// <param name="num"></param>
    public void Spend(int num)
    {
        gameMoney -= num;
        moneyUiText.text = $"{gameMoney:000}";
    }

    #endregion

    #region �Q�b�^�[ �Z�b�^�[

    public int GameMoney 
    {
        get { return gameMoney;}
        set { gameMoney = value; }
    }

    #endregion
}
