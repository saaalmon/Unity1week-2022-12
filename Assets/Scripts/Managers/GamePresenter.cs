using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using TMPro;

public class GamePresenter : MonoBehaviour
{
  //Model
  [SerializeField]
  private HpManager _hpMana;
  [SerializeField]
  private TimerManager _timerMana;
  [SerializeField]
  private SpManager _spMana;

  //View
  [SerializeField]
  private Image _removePanel;
  [SerializeField]
  private TextMeshProUGUI _timerText;
  [SerializeField]
  private Image _spGauge;

  // Start is called before the first frame update
  void Start()
  {
    _hpMana.Hp.Subscribe(x =>
    {
      if (x <= 0) _removePanel.gameObject.SetActive(true);
      else _removePanel.gameObject.SetActive(false);
    })
    .AddTo(this);

    _timerMana.Timer.Subscribe(x =>
    {
      _timerText.text = x.ToString("F2");
    })
    .AddTo(this);

    _spMana.Sp.Subscribe(x =>
    {
      var spMax = _spMana.GetSpMax();
      _spGauge.fillAmount = (float)x / spMax;
    })
    .AddTo(this);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
