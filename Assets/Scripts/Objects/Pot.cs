using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Lootable
{
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(BreakCo());
    }

    IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(.3f);
        MakeLoot();
        this.gameObject.SetActive(false);
    }
}
