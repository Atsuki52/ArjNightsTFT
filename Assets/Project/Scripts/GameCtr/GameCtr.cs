using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameCtr : MonoBehaviour
{
    public enum GameState
    {
        PreGame,    // ゲーム前
        InGame,     // ゲーム中
        GameOver    // ゲーム終了
    }

    public enum GameResult
    {
        NoContest,  // 無効
        Win,        // 勝ち
        Lose,       // 負け
    }


    [Header("ゲーム管理スクリプト群")]
    [SerializeField]
    private GameTimeCtr timeCtr;
    [SerializeField]
    private GameMoneyCtr moneyCtr;
    [SerializeField]
    private GameBuffCtr buffCtr;

    [Header("各種設定")]
    [SerializeField]
    private float countdownTime;


    private GameState currentGameState;
    private GameResult currentGameResult;

    // 一時変数群
    private float time;
    private int lastCount = -1;


    [Header("デバッグ用ボタン")]
    [SerializeField]
    private bool isDebug;
    [SerializeField]
    private bool debugWin;
    [SerializeField]
    private bool debugLose;
    [SerializeField]
    private float debugFinishTime;

    void Start()
    {
        currentGameState = GameState.PreGame;
        currentGameResult = GameResult.NoContest;
    }

    private void FixedUpdate()
    {
        switch (currentGameState)
        {
            case GameState.PreGame:
                PreGame();
                break;

            case GameState.InGame:
                if (isDebug) InGameDebug();
                break;

            case GameState.GameOver:
                break;
        }
    }

    /// <summary>
    /// ゲーム前　カウント処理
    /// </summary>
    private void PreGame()
    {
        time += Time.deltaTime;

        // カウントダウン表示処理
        int count = Mathf.CeilToInt(countdownTime - time);

        if (count != lastCount)
        {
            if(count == 0)
            {
                Debug.Log("スタート");
            }
            else
            {
                Debug.Log($"カウントダウン: {count}");
            }

            
            lastCount = count;
        }

        // ゲームを始める
        if (time > countdownTime)
        {
            currentGameState = GameState.InGame;

            time = 0;
            lastCount = -1;
        }
    }

    /// <summary>
    /// ゲーム中の挙動　デバッグ用
    /// </summary>
    private void InGameDebug()
    {
        time += Time.deltaTime;

        if (time > debugFinishTime)
        {
            if(debugWin) HandleGameFinish(GameResult.Win);
            else if(debugLose) HandleGameFinish(GameResult.Lose);
            else HandleGameFinish(GameResult.NoContest);
        }
    }


    #region 終了処理群

    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    /// <param name="gameResult"></param>
    public void HandleGameFinish(GameResult gameResult)
    {
        currentGameState = GameState.GameOver;

        // 何かしら終了処理

        switch (gameResult)
        {
            case GameResult.Win:
                HandleWin();
                break;

            case GameResult.Lose:
                HandleLose();
                break;

            case GameResult.NoContest:
                Debug.Log("無効試合");
                break;

            default:
                Debug.Log("勝利判定できてません");
                break;
        }
    }

    /// <summary>
    /// 勝利処理
    /// </summary>
    private void HandleWin()
    {
        Debug.Log("かち");
    }

    /// <summary>
    /// 敗北処理
    /// </summary>
    private void HandleLose()
    {
        Debug.Log("まけ");
    }

    #endregion

    #region ゲッター セッター

    public GameState CurrentGameState
    {
        get { return currentGameState; }
        set { currentGameState = value; }
    }

    public GameResult CurrentGameResult
    {
        get { return currentGameResult; }
        set { currentGameResult = value; }
    }

    #endregion
}
