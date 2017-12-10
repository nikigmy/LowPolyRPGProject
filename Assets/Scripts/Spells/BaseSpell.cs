using UnityEngine;

namespace Assets.Scripts.Spells
{
    public class BaseSpell : MonoBehaviour
    {
        public virtual void Cast(GameObject caster, Transform castPosition, GameObject target = null)
        {

        }
    }
}
