using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Enemy : MonoBehaviour, IDamageable
{
  private Rigidbody2D rb;

  [SerializeField]
  private Shot _shot;
  [SerializeField]
  private float _speed;
  [SerializeField]
  private int _hpMax;
  [SerializeField]
  private int _attack;

  [SerializeField]
  protected float _angleRange;
  [SerializeField]
  protected int _count;

  protected int _hp;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public virtual void Shot(float angleBase, float angleRange, int count)
  {
    var pos = transform.position;
    var rot = transform.rotation;

    if (1 < count)
    {
      for (var i = 0; i < count; i++)
      {
        var angle = angleBase + angleRange * ((float)i / (count - 1) - 0.5f);

        var shot = Instantiate(_shot, pos, rot);
        shot.Init(_attack, angle);
      }
    }
    else if (count == 1)
    {
      var shot = Instantiate(_shot, pos, rot);
      shot.Init(_attack, angleBase);
    }
  }

  public virtual void Init()
  {
    _hp = _hpMax;
  }

  public virtual void Final()
  {
    Destroy(gameObject);
  }

  public virtual void Hit(int damage)
  {
    _hp -= damage;

    if (_hp <= 0) Final();
  }
}
