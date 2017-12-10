using Assets.Scripts.Spells;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

    public BaseSpell spell;

    public virtual void Cast(GameObject caster, Transform castPosition, GameObject target = null)
    {
        Debug.Log("Stell castibg");
        spell.Cast(caster, castPosition, target);
    }
}
