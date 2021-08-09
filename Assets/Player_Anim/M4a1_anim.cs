using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class M4a1_anim : MonoBehaviour
{
    float startx;
    float starty;
    public float timeIdle;
    public float timeFire;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        starty = transform.localPosition.y;
        IdleGo();
    }
    void IdleGo()
    {
        gameObject.transform.DOLocalMove(new Vector3(transform.localPosition.x,-0.301f,transform.localPosition.z),timeIdle);
        IdleBack();
    }
    void IdleBack()
    {
        gameObject.transform.DOLocalMove(new Vector3(transform.localPosition.x, -0.301f, transform.localPosition.z), timeIdle);
        IdleGo();
    }
}
