/*
 * PIN ENTITY
 * 
 * Entity Data Handler.
 * 
 * */

using UnityEngine;

namespace HanoiTower.Pin
{
    public class PinDataHandler : MonoBehaviour
    {

        public PinData pinData;

        private void Awake()
        {
            pinData.currentOccupation.Push(-1);
        }

        public int GetID()
        {
            return pinData.pinID;
        }

        // Ring Stack Methods

        public void AddToStack(int val)
        {     
            pinData.currentOccupation.Push(val);      
        }

        public int RemoveFromStack()
        {
            return pinData.currentOccupation.Pop();
        }

        public int GetTopStackValue()
        {
            return pinData.currentOccupation.Peek();
        }

        public int GetStackLength()
        {
            return pinData.currentOccupation.Count;
        }
    }
}