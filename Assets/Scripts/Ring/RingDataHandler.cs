/*
 * RING ENTITY
 * 
 * Entity Data Handler.
 * 
 * */

using System.Collections.Generic;
using UnityEngine;


namespace HanoiTower.Ring
{
    public class RingDataHandler : MonoBehaviour
    {
        public RingDescription description;

        RingData data;

        void Awake()
        {
            data = ScriptableObject.CreateInstance<RingData>();
        }

        void Start()
        {
            SetDataValue(description.color);
        }

        // Bool values

        public void SetDataValue(string dataName, bool val)
        {
            switch (dataName)
            {
                case "grabbed":
                    data.grabbed = val;
                    break;
                case "pinned":
                    data.pinned = val;
                    break;
                case "pinCorrect":
                    data.isPinCorrect = val;
                    break;
                default:
                    break;
            }
        }

        public bool GetBoolValue(string dataName)
        {
            switch (dataName)
            {
                case "grabbed":
                    return data.grabbed;
                case "pinned":
                    return data.pinned;
                case "pinCorrect":
                    return data.isPinCorrect;
                default:
                    return false;
            }
        }

        // Float values

        public float GetFloatValue(string dataName)
        {
            switch (dataName)
            {
                case "speed":
                    return description.speed;
                case "rotationSpeed":
                    return description.rotationSpeed;
                case "inertia":
                    return description.inertia;
                default:
                    return -1f;
            }
        }

        // Vector2 values

        public void SetDataValue(string dataName, Vector2 val)
        {
            switch (dataName)
            {
                case "grabOffset":
                    data.grabOffset = val;
                    break;
                default:
                    break;
            }
        }

        public Vector2 GetVec2Value(string dataName)
        {
            switch (dataName)
            {
                case "grabOffset":
                    return data.grabOffset;
                default:
                    return Vector2.zero;
            }
        }

        // Ring Expressions

        public void SetDataValue(RingExpression val)
        {
            data.expression = val;
        }

        public RingExpression GetExpressionValue()
        {
            return data.expression;
        }

        // Ring Color

        public void SetDataValue(RingColor val)
        {    
            data.color = val;          
        }

        public RingColor GetColorValue()
        {
            return data.color;
        }

        // Collision List

        public void AddToCollisionList(RingColor color)
        {
            data.collidingWith.Add(color);
        }

        public void RemoveFromCollisionList(RingColor color)
        {
            data.collidingWith.Remove(color);
        }

        public List<RingColor> GetCollisionList()
        {
            return data.collidingWith;
        }

    }

}