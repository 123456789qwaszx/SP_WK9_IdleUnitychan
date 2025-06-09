using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

    public class BearSMBIdle : SceneLinkedSMB<BearBehavior>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);

            m_MonoBehaviour.FindTarget();
            if (m_MonoBehaviour.target != null)
            {
                m_MonoBehaviour.StartPursuit();
            }
        }
    }
