using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour, IDamageable
{
  private Rigidbody2D rb;
  private CircleCollider2D cor;
  private Animator anim;

  [SerializeField]
  private float _speed;
  [SerializeField]
  private int _hpMax;
  [SerializeField]
  private int _attack;

  protected int _hp;

  private Sequence _seq;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.TryGetComponent(out Player player))
    {
      Final();

      player.Hit(_attack);
    }
  }

  public virtual void Init()
  {
    if (rb == null) rb = GetComponent<Rigidbody2D>();
    if (cor == null) cor = GetComponent<CircleCollider2D>();
    if (anim == null) anim = GetComponent<Animator>();

    _hp = _hpMax;

    Move();
  }

  public virtual void Final()
  {
    _seq.Kill();

    Destroy(gameObject);
  }

  public virtual void Move()
  {
    var angle = Utils.GetAngle(
        transform.localPosition,
        Player._instance.transform.localPosition);
    var dir = Utils.GetDirection(angle);

    rb.velocity = dir * _speed;

    // var angles = transform.localEulerAngles;
    // angles.z = angle - 90;
    // transform.localEulerAngles = angles;
  }

  public virtual void Hit(int damage)
  {
    _hp -= damage;

    if (_hp <= 0)
    {
      cor.enabled = false;
      anim.SetBool("isPacked", true);

      SpManager._instance.IncSp();

      _seq = DOTween.Sequence()
      .Append(transform.DOMoveY(0.5f, 0.2f).SetLoops(2, LoopType.Yoyo).SetRelative())
      .AppendCallback(() => Final())
      .SetAutoKill()
      .Play();
    }
    else
    {
      _seq = DOTween.Sequence()
      .Append(transform.DOScale(1.4f, 0.05f).SetLoops(2, LoopType.Yoyo))
      .SetAutoKill()
      .Play();
    }
  }
}
