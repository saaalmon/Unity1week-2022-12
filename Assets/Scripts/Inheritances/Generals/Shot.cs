using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Shot : MonoBehaviour
{
  private Rigidbody2D rb;

  [SerializeField]
  private float _speed;

  protected int _attack;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public virtual void Init(int attack, float angle)
  {
    if (rb == null) rb = GetComponent<Rigidbody2D>();

    _attack = attack;

    var angles = transform.localEulerAngles;
    angles.z = angle - 90;
    transform.localEulerAngles = angles;

    var dir = Utils.GetDirection(angle);

    rb.velocity = dir * _speed;

    //仮スクリプト
    StartCoroutine(WaitForTimer());
  }

  public virtual void Final()
  {
    Destroy(gameObject);
  }

  public virtual void OnTriggerEnter2D(Collider2D other)
  {
    
  }

  //仮スクリプト
  private IEnumerator WaitForTimer()
  {
    yield return new WaitForSeconds(2.0f);

    Final();
  }
}
