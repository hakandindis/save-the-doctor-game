using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem prtsys;
    public GameObject impactEffect;
    public GameObject impactEffect2;
    public GameObject zombie_Blood;
    public float ammo;
    private Animator play_anim;
    private AudioSource sound;
    void Start()
    {
        prtsys.gameObject.active = false;
        sound = gameObject.GetComponent<AudioSource>();
        play_anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            prtsys.gameObject.active = true;
            prtsys.Play();
            sound.Play();
        }
        else if (Input.GetMouseButton(0))
        {
            //prtsys.Play();
            prtsys.gameObject.active = true;
            sound.Play();
            if (ammo <= 0)
            {
                Reload();
            }
            else
            {
                Shoot();
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            prtsys.gameObject.active = false;
            //sound.Stop();
            prtsys.Stop();
            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            Reload();
        }



    }
    void Shoot()
    {
        RaycastHit hit;
        ammo--;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target trg = hit.transform.GetComponent<Target>();
            if (trg != null)
            {
                trg.TakeDamage(damage);
                GameObject impactgo3 = Instantiate(zombie_Blood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactgo3, 0.7f);
            }
            GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            GameObject impactGo2 = Instantiate(impactEffect2, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactGo, 0.2f);
            Destroy(impactGo2, 0.6f);
        }
    }
    public void Reload()
    {
        play_anim.SetTrigger("Reload");
        ammo = 300;

    }
}
