using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Playables;
using Cysharp.Threading.Tasks;
using System;
using KanKikuchi.AudioManager;

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

  [SerializeField]
  private PlayableDirector _titleInit;
  [SerializeField]
  private PlayableDirector _gameInit;
  [SerializeField]
  private PlayableDirector _resultInit;

  public static GameManager _instance;

  //Managers
  [SerializeField]
  private Player _player;
  [SerializeField]
  private EnemyManager _enemyMana;
  [SerializeField]
  private TimerManager _timerMana;
  [SerializeField]
  private SpManager _spMana;

  void Awake()
  {
    _instance = this;

    SoundManager.PlayBGM(BGMPath.TITLE);

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

  public void ChengeTitleState()
  {
    SoundManager.StopBGM();
    SoundManager.PlayBGM(BGMPath.TITLE);

    SoundManager.PlaySE(SEPath.DECIDE);

    ChangeCurrentState(stateTitle);
  }

  public void ChengeGameState()
  {
    SoundManager.StopBGM();
    SoundManager.PlayBGM(BGMPath.GAME);

    SoundManager.PlaySE(SEPath.START);

    ChangeCurrentState(stateGame);
  }

  public void ChengeResultState()
  {
    SoundManager.StopBGM();
    SoundManager.PlaySE(SEPath.FINISH);

    SoundManager.PlaySE(SEPath.RESULT);

    ChangeCurrentState(stateResult);
  }

  public void SetRanking()
  {
    naichilab.RankingLoader.Instance.SendScoreAndShowRanking(_spMana._count);
  }

  async UniTask TimelinePlay(PlayableDirector timeline)
  {
    timeline.Play();

    Debug.Log(timeline.duration);

    await UniTask.Delay(TimeSpan.FromSeconds(timeline.duration));
  }
}

public partial class GameManager
{
  public class StateTitle : GameStateBase
  {
    async public override void OnEnter(GameManager owner, GameStateBase prevState)
    {
      Debug.Log("Title");

      owner._titleCanvas.gameObject.SetActive(true);

      //仮スクリプト
      owner._player.gameObject.SetActive(false);

      await owner.TimelinePlay(owner._titleInit);

      Debug.Log("timeline Completed!");
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
    async public override void OnEnter(GameManager owner, GameStateBase prevState)
    {
      Debug.Log("Game");

      owner._gameCanvas.gameObject.SetActive(true);

      //仮スクリプト
      owner._timerMana.SetTimer();
      owner._spMana.SetSp();
      owner._player.gameObject.SetActive(true);
      owner._player.Init();

      owner._player.StartShot();
      owner._enemyMana.StartGenerate();

      owner._timerMana.Timer
      .Subscribe(x =>
      {
        if (x <= 0) owner.ChengeResultState();
      })
      .AddTo(owner);

      await owner.TimelinePlay(owner._gameInit);

      Debug.Log("timeline Completed!");
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

      owner._enemyMana.StopGenerate();
      owner._enemyMana.ClearEnemy();
    }
  }

  public class StateResult : GameStateBase
  {
    async public override void OnEnter(GameManager owner, GameStateBase prevState)
    {
      Debug.Log("Result");

      owner._resultCanvas.gameObject.SetActive(true);

      owner._player.gameObject.SetActive(false);

      //仮スクリプト
      await owner.TimelinePlay(owner._resultInit);
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