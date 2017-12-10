using UnityEngine;
using System.Collections;
using com.ootii.Actors.AnimationControllers;
using com.ootii.Helpers;

#if !(UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4)
using UnityEngine.AI;
#endif

namespace com.ootii.Actors.Navigation
{
    /// <summary>
    /// Defines different ways to move on the offmesh link
    /// </summary>
    public enum OffMeshLinkMoveType
    {
        AutoDetect,
        Teleport,
        Linear,
        Parabola,
        Curve,
        Drop,
        ClimbOnto
    }

    /// <summary>
    /// Component that is added temporarily to travel across Nav Mesh Off-Mesh Links. This
    /// allows us to travel in lots of different ways.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class OffMeshLinkDriver : MonoBehaviour
    {
        /// <summary>
        /// Nav Mesh Agent we're dealing with
        /// </summary>
        protected NavMeshAgent mNavMeshAgent = null;
        public NavMeshAgent NavMeshAgent
        {
            get { return mNavMeshAgent; }            
            set { mNavMeshAgent = value; }
        }

        /// <summary>
        /// Movement type to use
        /// </summary>
        protected OffMeshLinkMoveType mMoveType = OffMeshLinkMoveType.AutoDetect;
        public OffMeshLinkMoveType MoveType
        {
            get { return mMoveType; }
            set { mMoveType = value; }
        }

        /// <summary>
        /// Speed of the movement
        /// </summary>
        protected float mSpeed = 0f;
        public float Speed
        {
            get { return mSpeed; }
            set { mSpeed = value; }
        }

        /// <summary>
        /// Height when using  parabola
        /// </summary>
        protected float mHeight = 1f;
        public float Height
        {
            get { return mHeight; }
            set { mHeight = value; }
        }

        /// <summary>
        /// Curve we can use with the movement
        /// </summary>
        protected AnimationCurve mCurve = null;
        public AnimationCurve Curve
        {
            get { return mCurve; }
            set { mCurve = value; }
        }

        /// <summary>
        /// Determines if the mover has finished
        /// </summary>
        protected bool mHasCompleted = false;
        public bool HasCompleted
        {
            get { return mHasCompleted; }
        }

        /// <summary>
        /// Information specific to this off-mesh link
        /// </summary>
        protected OffMeshLinkData mOffMeshLinkData;

        /// <summary>
        /// Motion Controller tied to this actor
        /// </summary>
        //protected MotionController mMotionController = null;

        /// <summary>
        /// Start position of the actor when this began
        /// </summary>
        protected Vector3 mStartPosition = Vector3.zero;
        

        /// <summary>
        /// Moves the agent to the link in a straight line
        /// </summary>
        /// <param name="speed">Speed at which we'll move</param>
        protected IEnumerator MoveLinear(float rSpeed)
        {
            Vector3 lEndPos = mOffMeshLinkData.endPos + Vector3.up * mNavMeshAgent.baseOffset;
            while (mNavMeshAgent.transform.position != lEndPos)
            {
                mNavMeshAgent.transform.position = Vector3.MoveTowards(mNavMeshAgent.transform.position, lEndPos, rSpeed * Time.deltaTime);
                yield return null;
            }
        }

        /// <summary>
        /// Moves across the off-mesh line following a curve.
        /// </summary>
        /// <param name="speed">Speed at which we'll move</param>
        protected IEnumerator MoveCurve(float rSpeed)
        {
            float lDuration = Vector3.Distance(mOffMeshLinkData.startPos, mOffMeshLinkData.endPos) / rSpeed;

            Vector3 lStartPos = mNavMeshAgent.transform.position;
            Vector3 lEndPos = mOffMeshLinkData.endPos + (Vector3.up * mNavMeshAgent.baseOffset);

            float lNormalizedTime = 0.0f;
            while (lNormalizedTime < 1.0f)
            {
                float lYOffset = (mCurve != null ? mCurve.Evaluate(lNormalizedTime) : 0f);
                mNavMeshAgent.transform.position = Vector3.Lerp(lStartPos, lEndPos, lNormalizedTime) + (Vector3.up * lYOffset);

                lNormalizedTime = lNormalizedTime + (Time.deltaTime / lDuration);
                yield return null;
            }
        }
    }
}

