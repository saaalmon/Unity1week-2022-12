using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_E : Shot
{
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public override void Init(int attack, float angle)
  {
    base.Init(attack, angle);
  }

  public override void Final()
  {
    base.Final();
  }

  public override void OnTriggerEnter2D(Collider2D other)
  {
    base.OnTriggerEnter2D(other);

    if (other.TryGetComponent(out Player player))
    {
      Final();
      player.Hit(_attack);
    }
  }
}
