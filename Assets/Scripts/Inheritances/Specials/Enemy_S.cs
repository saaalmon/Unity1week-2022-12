using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_S : Enemy
{
  [SerializeField]
  private float _interval;

  // Start is called before the first frame update
  void Start()
  {
    base.Init();

    //仮スクリプト
    //StartCoroutine(ShotIntervaler());
  }

  // Update is called once per frame
  void Update()
  {

  }

  public override void Init()
  {
    base.Init();
  }

  public override void Shot(float anglebase, float angleRange, int count)
  {
    base.Shot(anglebase, angleRange, count);
  }

  public override void Final()
  {
    base.Final();
  }

  //仮スクリプト
  public IEnumerator ShotIntervaler()
  {
    while (true)
    {
      base.Shot(transform.localEulerAngles.z, _angleRange, _count);

      yield return new WaitForSeconds(_interval);
    }
  }
}
