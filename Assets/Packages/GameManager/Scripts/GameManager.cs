using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
  private static readonly StateTitle stateTitle = new StateTitle();
  private static readonly StateGame stateGame = new StateGame();
  private static readonly StateResult stateResult = new StateResult();

  private GameStateBase currentState = stateTitle;

  [SerializeField]
  private CanvasGroup _titleCanvas;
  [SerializeField]
  private CanvasGroup _gameCanvas;
  [SerializeField]
  private CanvasGroup _resultCanvas;

  public static GameManager _instance;

  //Managers
  [SerializeField]
  private Player _player;
  [SerializeField]
  private TimerManager _timerMana;
  [SerializeField]
  private SpManager _spMana;

  void Awake()
  {
    _instance = this;

    _titleCanvas.gameObject.SetActive(false);
    _gameCanvas.gameObject.SetActive(false);
    _resultCanvas.gameObject.SetActive(false);
    currentState.OnEnter(this, null);
  }

  void Update()
  {
    currentState.OnUpdate(this);
  }

  public void ChangeCurrentState(GameStateBase nextState)
  {
    currentState.OnExit(this, nextState);
    nextState.OnEnter(this, currentState);
    currentState = nextState;
  }

  public GameStateBase GetCurrentState()
  {
    return currentState;
  }
}
public partial class GameManager
{
  public class StateTitle : GameStateBase
  {
    public override void OnEnter(GameManager owner, GameStateBase prevState)
    {
      Debug.Log("Title");

      owner._titleCanvas.gameObject.SetActive(true);

      //仮スクリプト
      owner._player.gameObject.SetActive(false);

      owner.ChangeCurrentState(stateGame);
    }

    public override void OnUpdate(GameManager owner)
    {

    }

    public override void OnExit(GameManager owner, GameStateBase nextState)
    {
      owner._titleCanvas.gameObject.SetActive(false);
    }
  }

  public class StateGame : GameStateBase
  {
    public override void OnEnter(GameManager owner, GameStateBase prevState)
    {
      Debug.Log("Game");

      owner._gameCanvas.gameObject.SetActive(true);

      //仮スクリプト
      owner._player.gameObject.SetActive(true);

      owner._timerMana.SetTimer();
      owner._spMana.SetSp();
    }

    public override void OnUpdate(GameManager owner)
    {
      //仮スクリプト
      var judge = owner._timerMana.DecTimer(Time.deltaTime);

      if (judge) owner.ChangeCurrentState(stateResult);
    }

    public override void OnExit(GameManager owner, GameStateBase nextState)
    {
      owner._gameCanvas.gameObject.SetActive(false);
    }
  }

  public class StateResult : GameStateBase
  {
    public override void OnEnter(GameManager owner, GameStateBase prevState)
    {
      Debug.Log("Result");

      owner._resultCanvas.gameObject.SetActive(true);

      //仮スクリプト
      owner._player.gameObject.SetActive(false);
    }

    public override void OnUpdate(GameManager owner)
    {

    }

    public override void OnExit(GameManager owner, GameStateBase nextState)
    {
      owner._resultCanvas.gameObject.SetActive(false);
    }
  }
}