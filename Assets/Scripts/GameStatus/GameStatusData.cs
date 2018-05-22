/*
 * GAME STATUS ENTITY
 * 
 * Entity Data Set.
 * 
 * */

namespace HanoiTower.Status
{
    public class GameStatusData
    {
        public GameStatus status;       // Status of the game

        public int lastVictoryPin;      // ID of the pin where the victory condition was met for the last time

        public int targetPin;           // ID of the pin where the victory condition has to be fullfilled
    }
}