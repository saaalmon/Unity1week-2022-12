using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour, IManagerable
{
    private IManagerable[] m_masterObject;
    
    public enum GameState
    {
        Init,
        Game,
        Result,
        End
    }

    public static Manager m_instance;

    public CanvasGroup InitCanvas;
    public CanvasGroup GameCanvas;
    public CanvasGroup ResultCanvas;

    private GameState currentGameState;

    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;

        m_masterObject = InterfaceUtils.FindObjectOfInterfaces<IManagerable>();

        SetCurrentState(GameState.Init);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0) return;

        OnGameStateUpdate(currentGameState);
    }

    //ステートをセット
    public void SetCurrentState(GameState state)
    {
        currentGameState = state;

        switch (state)
        {
            case GameState.Init:
                InitStartAction();
                break;
            case GameState.Game:
                GameStartAction();
                break;
            case GameState.Result:
                ResultStartAction();
                break;
            case GameState.End:
                EndStartAction();
                break;
        }
    }

    //ステートのスイッチ
    public void OnGameStateUpdate(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                InitAction();
                break;
            case GameState.Game:
                GameAction();
                break;
            case GameState.Result:
                ResultAction();
                break;
            case GameState.End:
                EndAction();
                break;
        }
    }

    //ステートごとの処理
    public void InitStartAction()
    {
        Debug.Log("Init");
        
        foreach (var list in m_masterObject)
        {
            // インタフェースを継承しているか確認
            if (list is IManagerable managerable)
            {
                managerable.InitStart();
            }
        }
    }

    public void GameStartAction()
    {
        Debug.Log("Game");

        foreach (var list in m_masterObject)
        {
            // インタフェースを継承しているか確認
            if (list is IManagerable managerable)
            {
                managerable.GameStart();
            }
        }
    }

    public void ResultStartAction()
    {
        Debug.Log("Result");

        foreach (var list in m_masterObject)
        {
            // インタフェースを継承しているか確認
            if (list is IManagerable managerable)
            {
                managerable.ResultStart();
            }
        }
    }

    public void EndStartAction()
    {
        Debug.Log("End");

        foreach (var list in m_masterObject)
        {
            // インタフェースを継承しているか確認
            if (list is IManagerable managerable)
            {
                managerable.EndStart();
            }
        }
    }

    //ステートごとの処理
    public void InitAction()
    {
        foreach (var list in m_masterObject)
        {
            // インタフェースを継承しているか確認
            if (list is IManagerable managerable)
            {
                managerable.InitUpdate();
            }
        }
    }

    public void GameAction()
    {
        foreach (var list in m_masterObject)
        {
            // インタフェースを継承しているか確認
            if (list is IManagerable managerable)
            {
                managerable.GameUpdate();
            }
        }
    }

    public void ResultAction()
    {
        foreach (var list in m_masterObject)
        {
            // インタフェースを継承しているか確認
            if (list is IManagerable managerable)
            {
                managerable.ResultUpdate();
            }
        }
    }

    public void EndAction()
    {
        foreach (var list in m_masterObject)
        {
            // インタフェースを継承しているか確認
            if (list is IManagerable managerable)
            {
                managerable.EndUpdate();
            }
        }
    }

    public void InitStart()
    {
        ResultCanvas.gameObject.SetActive(false);
        InitCanvas.gameObject.SetActive(true);
    }

    public void InitUpdate()
    {
        
    }

    public void GameStart()
    {
        InitCanvas.gameObject.SetActive(false);
        GameCanvas.gameObject.SetActive(true);
    }

    public void GameUpdate()
    {
        
    }

    public void ResultStart()
    {
        GameCanvas.gameObject.SetActive(false);
        ResultCanvas.gameObject.SetActive(true);
    }

    public void ResultUpdate()
    {
        
    }

    public void EndStart()
    {
        
    }

    public void EndUpdate()
    {

    }
}
