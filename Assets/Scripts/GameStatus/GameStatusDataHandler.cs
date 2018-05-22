/*
 * GAME STATUS ENTITY
 * 
 * Entity Data Handler.
 * 
 * */

using UnityEngine;

namespace HanoiTower.Status
{
    public class GameStatusDataHandler : MonoBehaviour
    {

        GameStatusData data;

        void Awake()
        {
            data = new GameStatusData();
            data.status = GameStatus.NORMAL;
        }

        // Game Status

        public GameStatus GetStatus()
        {
            return data.status;
        }

        public void SetStatus(GameStatus status)
        {
            data.status = status;
        }

        // Int values

        public void SetDataValue(string dataName, int val)
        {
            switch (dataName)
            {
                case "targetPinID":
                    data.targetPin = val;
                    break;
                case "lastVictoryPin":
                    data.lastVictoryPin = val;
                    break;
                default:
                    break;
            }
        }

        public int GetIntValue(string dataName)
        {
            switch (dataName)
            {
                case "targetPinID":
                    return data.targetPin;
                case "lastVictoryPin":
                    return data.lastVictoryPin;
                default:
                    return -1;
            }
        }

    }
}