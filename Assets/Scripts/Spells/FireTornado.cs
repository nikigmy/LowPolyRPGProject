using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Spells
{
    class FireTornado : BaseSpell
    {
        public bool Targeting;
        public GameObject ParticleObject;
        public GameObject TargetingParticle;
        private GameObject Caster;
        private Transform CastPosition;

        public override void Cast(GameObject caster, Transform castPosition, GameObject target = null)
        {
            Debug.Log("castedTornado");
            Caster = caster;
            CastPosition = castPosition;
            InitTargeting();
        }

        void Update()
        {
            GetMousePosition();
            if (Targeting)
            {
                if (Input.GetMouseButtonDown(0))
                    Cast();
                else if (Input.GetMouseButtonDown(1))
                    Targeting = false;
                else
                    ShowTargeter();
            }

        }

        void Cast()
        {
            var playerPlane = new Plane(Vector3.up, transform.position);
            
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hitdist = 0.0f;
            if (playerPlane.Raycast(ray, out hitdist))
            {
                var targetPoint = ray.GetPoint(hitdist);
                Instantiate(ParticleObject, targetPoint, Quaternion.identity);
                Targeting = false;
            }
        }

        private Vector3 GetMousePosition()
        {
            Vector3 p = new Vector3();
            Debug.Log(p);
            return p;
        }

        private void InitTargeting()
        {
            Targeting = true;
            ShowTargeter();
        }

        private void ShowTargeter()
        {
            
        }
    }
}
