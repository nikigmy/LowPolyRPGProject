using UnityEngine;
using com.ootii.Collections;

#if !(UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4)
using UnityEngine.AI;
#endif

namespace com.ootii.Actors.LifeCores
{
    /// <summary>
    /// Effect that changes a character's movement
    /// </summary>
    public class ModifyMovement : ActorCoreEffect
    {
        /// <summary>
        /// Defines how much we'll modify the movement
        /// </summary>
        public float _MovementFactor = 1f;
        public float MovementFactor
        {
            get { return _MovementFactor; }
            set { _MovementFactor = value; }
        }

        // Stored speed to reset
        protected float mOriginalSpeed = 0f;

        // Actor controller we want to tap into
        //protected ActorController mActorController = null;

        // NavMesh driver that we'll change
        //protected NavMeshDriver mNavMeshDriver = null;

        // NavMesh agent we will tap into
        protected NavMeshAgent mNavMeshAgent = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ModifyMovement() : base()
        {
        }

        /// <summary>
        /// ActorCore constructor
        /// </summary>
        public ModifyMovement(ActorCore rActorCore) : base(rActorCore)
        {

        }
        

        /// <summary>
        /// Releases the effect as an allocation
        /// </summary>
        public override void Release()
        {
            ModifyMovement.Release(this);
        }

        #region Editor Functions

#if UNITY_EDITOR

        /// <summary>
        /// Called when the inspector needs to draw
        /// </summary>
        public override bool OnInspectorGUI(UnityEngine.Object rTarget)
        {
            bool lIsDirty = base.OnInspectorGUI(rTarget);

            return lIsDirty;
        }

#endif

        #endregion

        // ******************************** OBJECT POOL ********************************

        /// <summary>
        /// Allows us to reuse objects without having to reallocate them over and over
        /// </summary>
        private static ObjectPool<ModifyMovement> sPool = new ObjectPool<ModifyMovement>(10, 10);

        /// <summary>
        /// Pulls an object from the pool.
        /// </summary>
        /// <returns></returns>
        public static ModifyMovement Allocate()
        {
            ModifyMovement lInstance = sPool.Allocate();
            return lInstance;
        }

        /// <summary>
        /// Returns an element back to the pool.
        /// </summary>
        /// <param name="rEdge"></param>
        public static void Release(ModifyMovement rInstance)
        {
            if (rInstance == null) { return; }

            rInstance.Clear();
            sPool.Release(rInstance);
        }
    }
}
