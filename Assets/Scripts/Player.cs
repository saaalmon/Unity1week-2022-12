using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Player : MonoBehaviour, IDamageable
{
  private Rigidbody2D rb;
  private Camera _mainCamera;

  [SerializeField]
  private HpManager _hpMana;
  [SerializeField]
  private Shot _shot;
  [SerializeField]
  private float _speed;
  [SerializeField]
  private int _attack;

  [SerializeField]
  private float _angleRange;
  [SerializeField]
  private int _count;
  [SerializeField]
  private float _angleLimit;

  private float _angle;

  // Start is called before the first frame update
  void Start()
  {
    Init();
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    Aim();

    if (Input.GetMouseButtonDown(0))
    {
      Shot(_angle, _angleRange, 1);
    }
  }

  public void Init()
  {
    if (rb == null) rb = GetComponent<Rigidbody2D>();
    if (_mainCamera == null) _mainCamera = Camera.main;

    _hpMana.SetHp();
  }

  private void Move()
  {
    var h = Input.GetAxis("Horizontal");
    var v = Input.GetAxis("Vertical");

    rb.velocity = new Vector2(h, v) * _speed;
  }

  private void Aim()
  {
    var screenPos = _mainCamera.WorldToScreenPoint(transform.position);
    var dir = Input.mousePosition - screenPos;
    _angle = Utils.GetAngle(Vector3.zero, dir);

    var angles = transform.localEulerAngles;
    angles.z = _angle - 90;
    transform.localEulerAngles = angles;
  }

  private void Shot(float angleBase, float angleRange, int count)
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
      var angle = angleBase + UnityEngine.Random.Range(-_angleLimit, _angleLimit);

      var shot = Instantiate(_shot, pos, rot);
      shot.Init(_attack, angle);
    }

    // var shot = _gene.Generate(_shot.gameObject);
    // shot.Init(transform.position, Quaternion.identity, _gene);
  }

  private void Blast()
  {
    Shot(_angle, _angleRange, _count);
  }

  async public void Hit(int damage)
  {
    Debug.Log("Hit");

    var judgeHp = _hpMana.DecHp(damage);

    if (judgeHp)
    {
      gameObject.SetActive(false);

      await Restart();
    }
  }

  //仮スクリプト
  async UniTask Restart()
  {
    await UniTask.Delay(TimeSpan.FromSeconds(2.0f));

    gameObject.SetActive(true);
    Blast();
    Init();
  }
}
