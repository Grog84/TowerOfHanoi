/*
 * HILLS ENTITY
 * 
 * COMPONENT setting the values of the hills animators parameters according to the Game status
 * 
 * */

using System.Collections;
using UnityEngine;

namespace HanoiTower.Setting
{
    public enum HillsAnimationState { NORMAL, SHAKING, UPDOWN };

    public class HillsController : MonoBehaviour
    {
        Animator[] hillAnimators;

        HillsDataHandler hillsData;
        HillsGameStatusMediator mediator;

        void Start()
        {
            hillAnimators = GetComponentsInChildren<Animator>();
            hillsData = GetComponent<HillsDataHandler>();
            mediator = GetComponent<HillsGameStatusMediator>();

            StartCoroutine(CheckStatusCO());

        }

        IEnumerator CheckStatusCO()
        {
            HillsAnimationState oldState = hillsData.GetState();
            while (true)
            {
                mediator.SetAnimStateFromGameStatus(hillsData);

                if (oldState != hillsData.GetState())
                {
                    oldState = hillsData.GetState();
                    SetAnimatorState(oldState);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        void SetAnimatorState(HillsAnimationState state)
        {
            foreach (Animator an in hillAnimators)
            {
                an.SetInteger("State", (int)state);
            }
        }

    }
}