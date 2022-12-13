using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Generator : MonoBehaviour
{
  private ObjectPool<GameObject> _pool;
  private GameObject _parent;

  public GameObject _obj { get; private set; }

  [SerializeField]
  private string _parentName;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public virtual void Init()
  {
    _parent = new GameObject(_parentName);

    _pool = new ObjectPool<GameObject>
    (
        () => Instantiate(_obj, _parent.transform),
        target => target.gameObject.SetActive(true),
        target => target.gameObject.SetActive(false),
        target => Destroy(target),
        true, 10, 100
    );
  }

  public virtual IPoolable Generate(GameObject obj)
  {
    _obj = obj;
    var prefab = _pool.Get();

    return prefab.GetComponent<IPoolable>();
  }

  public void Released(GameObject obj)
  {
    _pool.Release(obj);
  }

  public void Cleared()
  {
    var objs = _parent.GetComponentsInChildren<GameObject>();

    for (var i = 0; i < objs.Length; i++)
    {
      _pool.Release(objs[i]);
    }
  }
}
