/*
 * HILLS ENTITY
 * 
 * Entity Data Handler.
 * 
 * */

using UnityEngine;

namespace HanoiTower.Setting
{
    public class HillsDataHandler : MonoBehaviour
    {

        HillsData data;

        void Awake()
        {
            data = new HillsData();
            data.state = HillsAnimationState.NORMAL;
        }

        public HillsAnimationState GetState()
        {
            return data.state;
        }

        public void SetState(HillsAnimationState state)
        {
            data.state = state;
        }
    }
}