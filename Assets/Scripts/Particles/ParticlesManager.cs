/*
 * PARTICLES ENTITY
 * 
 * COMPONENT dedicated to the activation of the particles effects.
 * 
 * */

using System.Collections;
using UnityEngine;

namespace HanoiTower.Particles
{
    public class ParticlesManager : MonoBehaviour {

        public ParticleSystem[] starParticles;

        ParticlesDataHandler data;

        ParticlesGameStatusMediator mediator;

        int lastStarPos;

        private void Awake()
        {
            data = GetComponent<ParticlesDataHandler>();
            mediator = GetComponent<ParticlesGameStatusMediator>();
        }

        void Start()
        {
            lastStarPos = 0;   

            StartCoroutine(CheckVictoryCO());
        }

        IEnumerator CheckVictoryCO()
        {
            while (true)
            {
                if (data.GetBoolValue("burstVictory"))
                {
 
                    if (data.GetIntValue("victoryPinID") != lastStarPos)
                    {
                        lastStarPos = data.GetIntValue("victoryPinID");
                        starParticles[data.GetIntValue("victoryPinID")].Play();
                    }
                    

                    data.SetDataValue("burstVictory", false);
                }

                mediator.CheckGameStatus(data);
                yield return new WaitForSeconds(0.1f);
            }

        }

    }
}

