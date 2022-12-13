using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
  void Init();
  void Final();
}