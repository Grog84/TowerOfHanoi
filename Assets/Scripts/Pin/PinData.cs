/*
 * PIN ENTITY
 * 
 * Entity Data Set. Implemented as a scriptable object in order to be editable from unity inspector and assigned correctly
 * The pinID goes from left to right in the scene arrangement.
 * 
 * */

using System.Collections.Generic;
using UnityEngine;

namespace HanoiTower.Pin
{
    [CreateAssetMenu(menuName = "Custom/Data/Pin")]
    public class PinData : ScriptableObject
    {

        public int pinID;

        // Stack of rings in the pin. The int number is the cast of the RingColor enum to int
        [HideInInspector] public Stack<int> currentOccupation = new Stack<int>();

    }

}