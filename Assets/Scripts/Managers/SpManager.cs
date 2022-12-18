using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SpManager : MonoBehaviour
{
  public IReadOnlyReactiveProperty<int> Sp => _sp;
  private readonly IntReactiveProperty _sp = new IntReactiveProperty(0);

  public static SpManager _instance;


  [SerializeField]
  private EnemyManager _enemyMana;
  [SerializeField]
  private int _spMin;
  [SerializeField]
  private int _spMax;
  [SerializeField]
  private int _levelMax;

  [SerializeField]
  private float _intervalMin;
  [SerializeField]
  private float _intervalMax;

  private int _level;
  private int _spLimit;
  public int _count;

  // Start is called before the first frame update
  void Start()
  {
    _instance = this;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetSp()
  {
    _count = 0;
    _sp.Value = 0;
    _level = 0;
    _spLimit = _spMin;

    _enemyMana.SetInterval(SetEnemyIterval(0));
  }

  public bool IncSp()
  {
    _count++;
    var incSp = _sp.Value + 1;

    if (incSp >= _spLimit)
    {
      _sp.Value = 0;
      _level++;

      var ratio = (float)_level / _levelMax;
      var spLimit = Mathf.Lerp(_spMin, _spMax, (float)_level / _levelMax);
      _spLimit = (int)spLimit;

      Debug.Log(ratio);

      _enemyMana.SetInterval(SetEnemyIterval(ratio));
    }
    else _sp.Value = incSp;

    return incSp >= _spLimit;
  }

  public int GetSpMax()
  {
    return _spLimit;
  }

  private float SetEnemyIterval(float ratio)
  {
    var interval = Mathf.Lerp(_intervalMin, _intervalMax, (float)1 - ratio);

    Debug.Log(interval);

    return interval;
  }
}
