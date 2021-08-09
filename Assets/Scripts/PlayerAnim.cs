using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator player_anim;
    // Start is called before the first frame update
    void Start()
    {
        player_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)|| Input.GetMouseButtonDown(0))
        {
            player_anim.ResetTrigger("Idle");
            player_anim.SetBool("FireCheck", true);
        }
        if (Input.GetMouseButtonUp(0))
        {
            player_anim.SetTrigger("Idle");
            player_anim.SetBool("FireCheck", false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.GetComponent<Gun>().Reload();
        }
    }
   
}
