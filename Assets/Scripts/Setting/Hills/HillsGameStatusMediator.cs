/*
 * HILLS ENTITY
 * 
 * MEDIATOR translating the Game Status into a Hills animation state
 * 
 * */

using UnityEngine;

using HanoiTower.Status;

namespace HanoiTower.Setting
{
    public class HillsGameStatusMediator: MonoBehaviour
    {

        GameStatusDataHandler gameStatus;

        void Start()
        {
            gameStatus = FindObjectOfType<GameStatusDataHandler>();
        }

        public void SetAnimStateFromGameStatus(HillsDataHandler hillsData)
        {
            switch (gameStatus.GetStatus())
            {
                case GameStatus.NORMAL:
                    hillsData.SetState(HillsAnimationState.NORMAL);
                    break;
                case GameStatus.RIGHTMOVE:
                    hillsData.SetState(HillsAnimationState.SHAKING);
                    break;
                case GameStatus.WRONGMOVE:
                    hillsData.SetState(HillsAnimationState.UPDOWN);
                    break;
                case GameStatus.VICTORY:
                    hillsData.SetState(HillsAnimationState.SHAKING);
                    break;
                default:
                    break;
            }

        }

    }
}
