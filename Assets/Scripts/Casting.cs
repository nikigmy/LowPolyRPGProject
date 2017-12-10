using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    public Animator animator;
    public List<KeyCode> keymap;
    public bool QuickCast;
    public GameObject CastPosition;

    public GameObject[] SpellPrefabs;
    private GameObject spellParent;
    private List<Spell> spells;
    // Use this for initialization
    void Start()
    {
        spells = new List<Spell>();
        spellParent = Instantiate(new GameObject("Spell Parent"), gameObject.transform);
        InitSpells();
    }

    void InitSpells()
    {
        ClearSpells();
        foreach (var spell in SpellPrefabs)
        {
            spells.Add(Instantiate(spell, spellParent.transform).GetComponent<Spell>());
        }
    }

    void ClearSpells()
    {
        for (int i = 0; i < spellParent.transform.childCount; i++)
        {
            Destroy(spellParent.transform.GetChild(i));
        }
        spells.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < keymap.Count; i++)
        {
            if (spells.Count <= i)
                break;
            if (Input.GetKeyDown(keymap[i]))
            {
                Debug.Log("Stell clicked");
                spells[i].Cast(gameObject, CastPosition.transform);
            }
        }
    }
}
