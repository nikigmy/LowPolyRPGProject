using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    class FrostBall : BaseSpell
    {
        public GameObject ParticleObject;

        public override void Cast(GameObject caster, Transform castPosition, GameObject target = null)
        {
            var spawnedObject = Instantiate(ParticleObject, castPosition.transform.position, castPosition.transform.rotation);
            var movement = spawnedObject.GetComponent<FrostBallMovement>();
            movement.target = target;
        }
    }
}
