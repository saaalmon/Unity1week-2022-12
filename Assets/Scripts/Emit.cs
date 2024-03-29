using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using KanKikuchi.AudioManager;

public class Emit : MonoBehaviour
{
  //Title
  [SerializeField]
  private GameObject _titleLogo;

  //Result
  [SerializeField]
  private SpManager _spMana;
  [SerializeField]
  private TextMeshProUGUI _resultScoreText;

  public void TitleLogoAnim()
  {
    var seq = DOTween.Sequence()
    .Append(_titleLogo.transform.DOScale(1.5f, 0.2f).SetLoops(2, LoopType.Yoyo))
    .Play();
  }

  public void ScoreCount()
  {
    var score = _spMana._count;

    var seq = DOTween.Sequence()
    .AppendCallback(() => _resultScoreText.text = "0")
    .Append(_resultScoreText.DOCounter(0, score, 1.5f))
    .AppendCallback(() => SoundManager.PlaySE(SEPath.SCORE))
    .Append(_resultScoreText.DOScale(1.5f, 0.2f).SetLoops(2, LoopType.Yoyo))
    .Play();
  }
}
