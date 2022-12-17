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
  private int _spMax;

  private int _count;

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
  }

  public bool IncSp()
  {
    _count++;
    var incSp = _sp.Value + 1;

    if (incSp >= _spMax) _sp.Value = _spMax;
    else _sp.Value = incSp;

    return incSp >= _spMax;
  }

  public int GetSpMax()
  {
    return _spMax;
  }
}
