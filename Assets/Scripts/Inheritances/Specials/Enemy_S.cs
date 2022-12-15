using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_S : Enemy
{
  // Start is called before the first frame update
  void Start()
  {
    //ase.Init();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public override void Init()
  {
    base.Init();

    Destroy(gameObject, 4.5f);
  }

  public override void Final()
  {
    base.Final();
  }
}
