using UnityEngine;

namespace NetworkVisualizer.Visual
{
    /// <summary>
    /// This Behaviour fires the OnExit event, when a animation has completed.
    /// </summary>
    public class DeviceAnimationBehaviour : StateMachineBehaviour
    {

        public delegate void ExitHandler();
        public ExitHandler OnExit;

        /// <summary>
        /// OnStateExit is called when a transition ends and the state machine finishes evaluating this state.
        /// </summary>
        /// <param name="animator"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layerIndex"></param>
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (OnExit != null)
                OnExit();
        }
    }
}