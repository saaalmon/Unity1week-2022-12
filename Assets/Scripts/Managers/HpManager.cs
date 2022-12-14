using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class HpManager : MonoBehaviour
{
  public IReadOnlyReactiveProperty<int> Hp => _hp;
  private readonly IntReactiveProperty _hp = new IntReactiveProperty(0);

  [SerializeField]
  private int _hpMax;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetHp()
  {
    _hp.Value = _hpMax;
  }

  public bool IncHp(int point)
  {
    var incHp = _hp.Value + point;

    if (incHp >= _hpMax) _hp.Value = _hpMax;
    else _hp.Value = incHp;

    return incHp >= _hpMax;
  }

  public bool DecHp(int point)
  {
    var decHp = _hp.Value - point;

    if (decHp <= 0) _hp.Value = 0;
    else _hp.Value = decHp;

    return decHp <= 0;
  }
}
