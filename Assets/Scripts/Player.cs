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
  private SpriteRenderer sprite;
  private Camera _mainCamera;

  public static Player _instance;

  [SerializeField]
  private HpManager _hpMana;
  [SerializeField]
  private Shot _spShot;
  [SerializeField]
  private Shot[] _shots;
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

  [SerializeField]
  private float _interval;

  private float _angle;
  private Coroutine _cor;
  void Awake()
  {
    _instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    //Init();
  }

  // Update is called once per frame
  void Update()
  {
    Aim();
  }

  public void Init()
  {
    if (rb == null) rb = GetComponent<Rigidbody2D>();
    if (sprite == null) sprite = GetComponent<SpriteRenderer>();
    if (_mainCamera == null) _mainCamera = Camera.main;

    _cor = StartCoroutine(ShotRepeat());

    _hpMana.SetHp();
  }

  private void Final()
  {
    gameObject.SetActive(false);
    StopCoroutine(_cor);
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

    if (Mathf.Abs(_angle) > 90f) sprite.flipX = false;
    if (Mathf.Abs(_angle) < 90f) sprite.flipX = true;

    // var angles = transform.localEulerAngles;
    // angles.z = _angle - 90;
    // transform.localEulerAngles = angles;
  }

  private void Shot(Shot shot, float angleBase, float angleRange, int count)
  {
    var pos = transform.position;
    var rot = transform.rotation;

    if (1 < count)
    {
      for (var i = 0; i < count; i++)
      {
        var angle = angleBase + angleRange * ((float)i / (count - 1) - 0.5f);

        var pre = Instantiate(shot, pos, rot);
        pre.Init(_attack, angle);
      }
    }
    else if (count == 1)
    {
      var angle = angleBase + UnityEngine.Random.Range(-_angleLimit, _angleLimit);

      var pre = Instantiate(shot, pos, rot);
      pre.Init(_attack, angle);
    }

    // var shot = _gene.Generate(_shot.gameObject);
    // shot.Init(transform.position, Quaternion.identity, _gene);
  }

  private void Blast()
  {
    Shot(_spShot, _angle, _angleRange, _count);
  }

  async public void Hit(int damage)
  {
    Debug.Log("Hit");

    var judgeHp = _hpMana.DecHp(damage);

    if (judgeHp)
    {
      Final();
      await Restart();
    }
  }


  //仮スクリプト
  private IEnumerator ShotRepeat()
  {
    while (true)
    {
      yield return new WaitForSeconds(_interval);

      var rand = UnityEngine.Random.Range(0, _shots.Length);
      Shot(_shots[rand], _angle, _angleRange, 1);
    }
  }

  async UniTask Restart()
  {
    await UniTask.Delay(TimeSpan.FromSeconds(1.0f));

    gameObject.SetActive(true);
    Blast();
    Init();
  }
}
