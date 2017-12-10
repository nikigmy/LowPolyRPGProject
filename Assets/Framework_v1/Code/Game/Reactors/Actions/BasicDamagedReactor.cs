using System;
using UnityEngine;
using com.ootii.Base;
using com.ootii.Actors.LifeCores;
using com.ootii.Actors.AnimationControllers;
using com.ootii.Actors.Combat;
using com.ootii.Helpers;
using com.ootii.Messages;

namespace com.ootii.Reactors
{
    /// <summary>
    /// Basic reactor used for when a damage message comes in.
    /// </summary>
    [Serializable]
    [BaseName("Basic Damaged Reactor")]
    [BaseDescription("Basic reactor for handling messages of type DamageMessage. This is where damage is applied and any other effects or animations should be activated.")]
    public class BasicDamagedReactor : ReactorAction
    {
        /// <summary>
        /// Attribute identifier that represents the health attribute
        /// </summary>
        public string _HealthID = "Health";
        public string HealthID
        {
            get { return _HealthID; }
            set { _HealthID = value; }
        }

        /// <summary>
        /// Motion name to use when damage is taken
        /// </summary>
        public string _DamagedMotion = "BasicDamaged";
        public string DamagedMotion
        {
            get { return _DamagedMotion; }
            set { _DamagedMotion = value; }
        }

        // ActorCore the reactor belongs to
        [NonSerialized]
        protected ActorCore mActorCore = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BasicDamagedReactor() : base()
        {
            _ActivationType = 0;
        }

        /// <summary>
        /// ActorCore constructor
        /// </summary>
        public BasicDamagedReactor(GameObject rOwner) : base(rOwner)
        {
            _ActivationType = 0;
            mActorCore = rOwner.GetComponent<ActorCore>();
        }

        /// <summary>
        /// Initialize the reactor
        /// </summary>
        public override void Awake()
        {
            if (mOwner != null)
            {
                mActorCore = mOwner.GetComponent<ActorCore>();
            }
        }

        /// <summary>
        /// Called when the reactor is meant to be deactivated
        /// </summary>
        public override void Deactivate()
        {
            base.Deactivate();

            mMessage = null;
        }

        #region Editor Functions

#if UNITY_EDITOR

        /// <summary>
        /// Called when the inspector needs to draw
        /// </summary>
        public override bool OnInspectorGUI(UnityEditor.SerializedObject rTargetSO, UnityEngine.Object rTarget)
        {
            _EditorShowActivationType = false;
            bool lIsDirty = base.OnInspectorGUI(rTargetSO, rTarget);

            if (EditorHelper.TextField("Health ID", "Attribute identifier that represents the health attribute", HealthID, rTarget))
            {
                lIsDirty = true;
                HealthID = EditorHelper.FieldStringValue;
            }

            if (EditorHelper.TextField("Damaged Motion", "Name of motion to activate when damage occurs and the message isn't handled.", DamagedMotion, rTarget))
            {
                lIsDirty = true;
                DamagedMotion = EditorHelper.FieldStringValue;
            }

            return lIsDirty;
        }

#endif

        #endregion
    }
}
