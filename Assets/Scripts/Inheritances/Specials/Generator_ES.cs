using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator_ES : Generator
{
  // Start is called before the first frame update
  void Start()
  {
    base.Init();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public override IPoolable Generate(GameObject obj)
  {
    return base.Generate(obj);
  }
}
