/*
 * PARTICLES ENTITY
 * 
 * MEDIATOR used in order to translate the game status into the particles data
 * 
 * */


using UnityEngine;

using HanoiTower.Status;

namespace HanoiTower.Particles
{

    public class ParticlesGameStatusMediator : MonoBehaviour
    {

        GameStatusDataHandler gameStatus;

        void Start()
        {
            gameStatus = FindObjectOfType<GameStatusDataHandler>();
        }

        public void CheckGameStatus(ParticlesDataHandler particles)
        {
            if (gameStatus.GetStatus() == GameStatus.VICTORY)
            {
                particles.SetDataValue("burstVictory", true);
                particles.SetDataValue("victoryPinID", gameStatus.GetIntValue("lastVictoryPin"));
            }

        }

    }
}