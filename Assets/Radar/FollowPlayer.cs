using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 
    Bu script maincamera'nin uzerine eklenmelidir.  
*/
public class FollowPlayer : MonoBehaviour
{
    //mainCamera nin koordinatlarini takip etmesini istediğiniz obje
    public Transform PlayerTransform;
    private Vector3 _cameraoffset;

    [Range(0.01f, 1.0f)]
    public float Smoothfactor = 0.5f;

    // Start is called before the first frame update
    void Start()
    {   
        //mainCamera ile obje arasindaki mesafe
        _cameraoffset = transform.position - PlayerTransform.position;

    }

    // LateUpdate is called once per frame
    void LateUpdate()
    {

        /* Her yeni frame ile birlikte takip edilen objenin position degerine mainCamera ile obje arasındaki fark eklenir. 
           Boylece aralarındaki uzaklık hep sabit kalir. */
        
        Vector3 newPos = PlayerTransform.position + _cameraoffset;

        //mainCamera nin position degerleri her frame ile birlikte tekrar guncellenir
        transform.position = Vector3.Slerp(transform.position, newPos, Smoothfactor);

    }
}
