using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameCtr : MonoBehaviour
{
    public enum GameState
    {
        PreGame,    // �Q�[���O
        InGame,     // �Q�[����
        GameOver    // �Q�[���I��
    }

    public enum GameResult
    {
        NoContest,  // ����
        Win,        // ����
        Lose,       // ����
    }


    [Header("�Q�[���Ǘ��X�N���v�g�Q")]
    [SerializeField]
    private GameTimeCtr timeCtr;
    [SerializeField]
    private GameMoneyCtr moneyCtr;
    [SerializeField]
    private GameBuffCtr buffCtr;

    [Header("�e��ݒ�")]
    [SerializeField]
    private float countdownTime;


    private GameState currentGameState;
    private GameResult currentGameResult;

    // �ꎞ�ϐ��Q
    private float time;
    private int lastCount = -1;


    [Header("�f�o�b�O�p�{�^��")]
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
    /// �Q�[���O�@�J�E���g����
    /// </summary>
    private void PreGame()
    {
        time += Time.deltaTime;

        // �J�E���g�_�E���\������
        int count = Mathf.CeilToInt(countdownTime - time);

        if (count != lastCount)
        {
            if(count == 0)
            {
                Debug.Log("�X�^�[�g");
            }
            else
            {
                Debug.Log($"�J�E���g�_�E��: {count}");
            }

            
            lastCount = count;
        }

        // �Q�[�����n�߂�
        if (time > countdownTime)
        {
            currentGameState = GameState.InGame;

            time = 0;
            lastCount = -1;
        }
    }

    /// <summary>
    /// �Q�[�����̋����@�f�o�b�O�p
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


    #region �I�������Q

    /// <summary>
    /// �Q�[���I������
    /// </summary>
    /// <param name="gameResult"></param>
    public void HandleGameFinish(GameResult gameResult)
    {
        currentGameState = GameState.GameOver;

        // ��������I������

        switch (gameResult)
        {
            case GameResult.Win:
                HandleWin();
                break;

            case GameResult.Lose:
                HandleLose();
                break;

            case GameResult.NoContest:
                Debug.Log("��������");
                break;

            default:
                Debug.Log("��������ł��Ă܂���");
                break;
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void HandleWin()
    {
        Debug.Log("����");
    }

    /// <summary>
    /// �s�k����
    /// </summary>
    private void HandleLose()
    {
        Debug.Log("�܂�");
    }

    #endregion

    #region �Q�b�^�[ �Z�b�^�[

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
