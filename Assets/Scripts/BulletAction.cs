using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BulletAction : MonoBehaviour
{
    public ObjectsPool Pool;

    //data part
    public BulletType type;

    public float duration;
    public int harm;
    public int speed;

    private float originZ;

    void Awake()
    {
        originZ = gameObject.GetComponent<RectTransform>().localEulerAngles.z;
    }

    void Start()
    {
        Pool = transform.parent.parent.gameObject.GetComponent<ObjectsPool>();
    }

    void OnEnable()
    {
        Pool = transform.parent.parent.gameObject.GetComponent<ObjectsPool>();
        Invoke("DestorySelf", duration);
    }

    void OnDisable()
    {
        gameObject.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, originZ);
        CancelInvoke();
    }

    public void Init(int angle)
    {
        Vector3 originVector = gameObject.GetComponent<RectTransform>().localEulerAngles;
        gameObject.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, originVector.z + angle);
    }

    public void OnHit()
    {
        //StopAllCoroutines();
        CancelInvoke();
        Pool.ReturnInstance(type, gameObject);
    }

    void Update()
    {
        gameObject.GetComponent<RectTransform>().position += transform.right * speed * Time.deltaTime;
    }

    //IEnumerator DestorySelf()
    //{
    //    yield return new WaitForSeconds(duration);
    //    Pool.ReturnInstance(type,gameObject);
    //}

    void DestorySelf()
    {
        Pool.ReturnInstance(type, gameObject);
    }
}