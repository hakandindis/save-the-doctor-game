using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class FireSystem : MonoBehaviour
{

    RaycastHit hit;
    public GameObject RayPoint;
    public bool CanFire;
    float gunTimer;
    public float gunCooldown;
    public ParticleSystem MuzzleFlash;
    public int ammo=30;
    [Header("Oyun Sesleri")]
    AudioSource SesKaynak;
    public AudioClip FireSound;
    public float range;
    public AnimationClip reload;
    Animation anim;
    public GameObject zombie_Blood;
    public float damage;
    public GameObject impactEffect;
    public GameObject fpsCam;
    public GameObject time;
    public GameObject finish;
    // Start is called before the first frame update
    void Start()
    {
        SesKaynak = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();
        Cursor.visible = false;
        ammo = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponentInParent<Hakan>().health > 0 && !time.GetComponent<NewTimer>().isFinish() && !finish.GetComponent<GameOver>().finish_check)
        {
            Debug.Log("DENEM");
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(ammo<= 0)
        {
            MuzzleFlash.Stop();
            MuzzleFlash.gameObject.active = false;
            SesKaynak.Stop();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            MuzzleFlash.Stop();
            MuzzleFlash.gameObject.active = false;
            SesKaynak.Stop();
            anim.clip = reload;
            anim.Play();
            ammo = 30;
            CanFire = true;
        }
        if (Input.GetKey(KeyCode.Mouse0) && ammo>0 && Time.time > gunTimer && !anim.isPlaying)
        {
            Fire();
            gunTimer = Time.time + gunCooldown;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            MuzzleFlash.Stop();
            MuzzleFlash.gameObject.active = false;
            SesKaynak.Stop();
        }
    }

    void Fire()
    {
        RaycastHit hit;
        MuzzleFlash.gameObject.active = true;
        MuzzleFlash.Play();
        SesKaynak.Play();
        SesKaynak.clip = FireSound;
        ammo--;
       if(Physics.Raycast(fpsCam.transform.position,fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                target.TakeDamage(damage);
                GameObject impactgo3 = Instantiate(zombie_Blood, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactgo3, 0.7f);
                Debug.Log(target.name);
            }
            else
            {
                GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGo, 0.2f);
            }
        }
        //if (Physics.Raycast(RayPoint.transform.position, RayPoint.transform.forward, out hit, range))
        //{
            
        //    Target trg = hit.transform.GetComponent<Target>();
        //    if (trg != null)
        //    {
        //        Debug.Log(trg.gameObject.name);
        //        trg.TakeDamage(damage);
        //        GameObject impactgo3 = Instantiate(zombie_Blood, hit.point, Quaternion.LookRotation(hit.normal));
        //        Destroy(impactgo3, 0.7f);
        //    }
        //    GameObject impactGo = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //    Destroy(impactGo, 0.2f);
        //    Debug.Log(hit.transform.name);
        
    }


}