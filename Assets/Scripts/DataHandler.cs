/*
 * Data Handler used by the Game Manager in order to modify
 * the data set of entities in game 
 * 
 * */

using HanoiTower.Pin;
using HanoiTower.Ring;
using HanoiTower.Status;

namespace HanoiTower
{
    public class DataHandler
    {

        // RING

        public RingColor GetRingColor(RingDataHandler data)
        {
            return data.GetColorValue();
        }

        public bool GetRingPositionValidity(RingDataHandler data)
        {
            return data.GetBoolValue("pinCorrect");
        }

        public void UnPin(RingDataHandler data)
        {
            data.SetDataValue("pinned", false);

            if (data.GetBoolValue("grabbed"))
            {
                data.SetDataValue(RingExpression.WONDERING);
            }
            else
            {
                data.SetDataValue(RingExpression.HAPPY);
            }
        }

        // PIN

        public int GetPinOccupationValue(PinDataHandler data)
        {
            return data.GetTopStackValue();
        }

        public void RemoveRing(PinDataHandler data)
        {
            data.RemoveFromStack();
        }

        // GENERAL

        public void SetRightOccupation(PinDataHandler pinData, RingDataHandler ringData, GameStatusDataHandler gameStatus)
        {
            pinData.AddToStack((int)ringData.GetColorValue());
            ringData.SetDataValue("pinCorrect", true);
            ringData.SetDataValue("pinned", true);
            gameStatus.SetStatus(GameStatus.RIGHTMOVE);
        }

        public void SetWrongOccupation(PinDataHandler pinData, RingDataHandler ringData, GameStatusDataHandler gameStatus)
        {
            ringData.SetDataValue("pinCorrect", false);
            ringData.SetDataValue("pinned", true);
            ringData.SetDataValue(RingExpression.WORRIED);
            gameStatus.SetStatus(GameStatus.WRONGMOVE);
        }

        public void SetVictory(PinDataHandler pinData, RingDataHandler ringData, GameStatusDataHandler gameStatus)
        {
            pinData.AddToStack((int)ringData.GetColorValue());
            ringData.SetDataValue("pinCorrect", true);
            ringData.SetDataValue("pinned", true);
            if (gameStatus.GetIntValue("targetPinID") == 0)
            {
                gameStatus.SetDataValue("targetPinID", 2);
                gameStatus.SetDataValue("lastVictoryPin", 0);
            }
            else
            {
                gameStatus.SetDataValue("targetPinID", 0);
                gameStatus.SetDataValue("lastVictoryPin", 2);
            }
            gameStatus.SetStatus(GameStatus.VICTORY);
        }

    }
}