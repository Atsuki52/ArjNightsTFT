using UnityEngine;

public class GameTimeCtr : MonoBehaviour
{
    [SerializeField]
    private GameCtr gameCtr;

    private float gameTime;

    void Start()
    {
        gameTime = 0.0f;
    }

    void FixedUpdate()
    {
        if(gameCtr.CurrentGameState == GameCtr.GameState.InGame)
        gameTime += Time.deltaTime;
    }

    #region ゲッター セッター

    public float GameTime
    {
        get { return gameTime; }
    }

    #endregion
}
