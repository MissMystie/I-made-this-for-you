using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mystie.Animation
{
    public class DisableOnExit : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator anim, AnimatorStateInfo stateInfo, int layerIndex)
        {
            anim.gameObject.SetActive(false);
        }
    }
}
