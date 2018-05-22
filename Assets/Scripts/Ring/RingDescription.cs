/*
 * RING ENTITY
 * 
 * This is the set of data exposed for design purpose.
 * Being a scriptable object it possible to modify its values from the unity editor
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HanoiTower.Ring
{
    public enum RingExpression { HAPPY, WONDERING, WORRIED };

    // Ring colors are sorted by size in the correct order
    public enum RingColor { YELLOW, BLUE, GREEN, RED };

    [CreateAssetMenu(menuName = "Custom/Description/Ring")]
    public class RingDescription : ScriptableObject
    {

        public RingColor color;

        [Space(10)]
        [Tooltip("Ring speed in following the player input")]
        [Range(10f, 50f)]
        public float speed;
        [Tooltip("Rotation speed in order to get to the horizontal position")]
        [Range(5, 15)]
        public float rotationSpeed;
        [Tooltip("Inertia of the ring when it is tossed")]
        [Range(50, 200)]
        public float inertia;

        [Space(10)]
        public Sprite front;
        public Sprite back;

        [Space(10)]
        public Sprite frontGlow;
        public Sprite backGlow;

        [Tooltip("List containing the ring facial expressions. If None is present the program works correctly." +
            " However if any are desired, 3 expressions must be created in the following order: HAPPY, WONDERING, WORRIED ")]
        [Space(10)]
        public Sprite[] Expressions;

    }
}

