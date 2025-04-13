using UnityEngine;
using UnityEngine.UI;

public class GameMoneyCtr : MonoBehaviour
{
    [SerializeField]
    private GameCtr gameCtr;

    [SerializeField]
    private Text moneyUiText;

    [Header("各種設定")]
    [SerializeField]
    private float incomeInterval; // 収入間隔
    [SerializeField]
    private int incomeAmount; // 収入額

    private int gameMoney;

    //一時変数群
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

    #region 増やす　減らす

    /// <summary>
    /// お金を増やす UI上書き
    /// </summary>
    /// <param name="num"></param>
    public void Add(int num)
    {
        gameMoney += num;
        moneyUiText.text = $"{gameMoney:000}";
    }

    /// <summary>
    /// お金を減らす UI上書き
    /// </summary>
    /// <param name="num"></param>
    public void Spend(int num)
    {
        gameMoney -= num;
        moneyUiText.text = $"{gameMoney:000}";
    }

    #endregion

    #region ゲッター セッター

    public int GameMoney 
    {
        get { return gameMoney;}
        set { gameMoney = value; }
    }

    #endregion
}
