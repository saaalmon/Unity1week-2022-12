using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TimerManager : MonoBehaviour
{
  public IReadOnlyReactiveProperty<float> Timer => _timer;
  private readonly FloatReactiveProperty _timer = new FloatReactiveProperty(0);

  [SerializeField]
  private float _timerMax;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetTimer()
  {
    _timer.Value = _timerMax;
  }

  public bool DecTimer(float time)
  {
    var decTimer = _timer.Value - time;

    if (decTimer <= 0) _timer.Value = 0;
    else _timer.Value = decTimer;

    return decTimer <= 0;
  }
}
