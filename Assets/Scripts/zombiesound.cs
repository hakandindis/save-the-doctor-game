using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiesound : MonoBehaviour
{
    private AudioSource ses;
    public GameObject player;
    public AudioClip clip;
    void Start()
    {
        ses = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float mesafe = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if(mesafe <= 10)
        {
            Debug.Log("play2");
            if(!ses.isPlaying)
            {
                Debug.Log("play");
                ses.clip = clip;
                ses.Play();
            }
            
            
        }
        else
        {
            
            ses.Stop();
        }
    }
}
