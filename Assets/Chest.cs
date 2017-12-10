using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class Chest : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        var interactable = GetComponent<Interactable>();
        interactable.eventa += Open;
    }

    private void Open(object sender, EventArgs e)
    {
        anim.SetTrigger("Open");
        DropLoot();
    }

    private void DropLoot()
    {
        //throw new NotImplementedException();
    }
}
