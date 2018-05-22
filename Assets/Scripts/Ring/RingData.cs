/*
 * RING ENTITY
 * 
 * Entity Data Set.
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HanoiTower.Ring
{
    
    [CreateAssetMenu(menuName = "Custom/Data/Ring")]
    public class RingData : ScriptableObject
    {
        
        [HideInInspector] public bool grabbed;                  // grabbing status of the ring

        [HideInInspector] public bool pinned;                   // pinning status of the ring

        [HideInInspector] public bool isPinCorrect;             // pinning has occurred in the right position

        [HideInInspector] public RingExpression expression;     // ring facial expression

        [HideInInspector] public RingColor color;               // ring color

        [HideInInspector] public List<RingColor> collidingWith = new List<RingColor>();     // List of the other rings currently colliding

        [HideInInspector] public Vector2 grabOffset;            // offset with respect to the center of the ring where the grabbing has occured

    }
}

