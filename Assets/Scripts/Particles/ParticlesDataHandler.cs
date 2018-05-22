/*
 * PARTICLES ENTITY
 * 
 * Entity Data Handler.
 * 
 * */

using UnityEngine;


namespace HanoiTower.Particles
{
    public class ParticlesDataHandler : MonoBehaviour
    {
        ParticlesData data;
        
        void Awake()
        {
            data = new ParticlesData();
            data.burstVictory = false;
        }

        // Bool values

        public void SetDataValue(string dataName, bool val)
        {
            switch (dataName)
            {
                case "burstVictory":
                    data.burstVictory = val;
                    break;
                default:
                    break;
            }
        }

        public bool GetBoolValue(string dataName)
        {
            switch (dataName)
            {
                case "burstVictory":
                    return data.burstVictory;
                default:
                    return false;
            }
        }

        // Int values

        public void SetDataValue(string dataName, int val)
        {
            switch (dataName)
            {
                case "victoryPinID":
                    data.victoryPinID = val;
                    break;
                default:
                    break;
            }
        }

        public int GetIntValue(string dataName)
        {
            switch (dataName)
            {
                case "victoryPinID":
                    return data.victoryPinID;
                default:
                    return -1;
            }
        }


    }
}