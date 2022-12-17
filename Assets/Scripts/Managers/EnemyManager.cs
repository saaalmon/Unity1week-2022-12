using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RESPAWN_TYPE
{
  UP,
  RIGHT,
  DOWN,
  LEFT,
  SIZEOF,
}

public class EnemyManager : MonoBehaviour
{
  [SerializeField]
  private Enemy _enemy;

  [SerializeField]
  private float _interval;
  [SerializeField]
  private Vector2 _range;

  private Coroutine _cor;

  // Start is called before the first frame update
  void Start()
  {
    //StartCoroutine(EnemyGenerate());
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void StartGenerate()
  {
    _cor = StartCoroutine(EnemyGenerate());
  }

  public void StopGenerate()
  {
    StopCoroutine(_cor);
  }

  private IEnumerator EnemyGenerate()
  {
    while (true)
    {
      yield return new WaitForSeconds(_interval);

      var randPos = new Vector2(Random.Range(-_range.x, _range.x), Random.Range(-_range.y, _range.y));
      var enemy = Instantiate(_enemy, randPos, Quaternion.identity);
      enemy.transform.position = RespawnPos((RESPAWN_TYPE)Random.Range(0, (int)RESPAWN_TYPE.SIZEOF));
      enemy.Init();
    }
  }

  private Vector3 RespawnPos(RESPAWN_TYPE respawnType)
  {
    var pos = Vector3.zero;

    switch (respawnType)
    {
      case RESPAWN_TYPE.UP:
        pos.x = Random.Range(-_range.x, _range.x);
        pos.y = _range.y;
        break;

      case RESPAWN_TYPE.RIGHT:
        pos.x = _range.x;
        pos.y = Random.Range(-_range.y, _range.y);
        break;

      case RESPAWN_TYPE.DOWN:
        pos.x = Random.Range(-_range.x, _range.x);
        pos.y = -_range.y;
        break;

      case RESPAWN_TYPE.LEFT:
        pos.x = -_range.x;
        pos.y = Random.Range(-_range.y, _range.y);
        break;
    }

    return pos;
  }
}
