/*
 * INPUT MANAGER
 * 
 * This is the class from where the mouse and touch input manager are derived
 * It is never used as is and could have been made an abstract class
 * However given the methods in common between the touch and mouse managers it has been
 * implemented as a proper class
 * 
 * */

using System.Collections.Generic;
using UnityEngine;

namespace HanoiTower
{
    public class InputManager : MonoBehaviour
    {
        protected bool pressed;             

        protected Vector2 pressPosition;

        protected List<Vector4> obstaclesBounds = new List<Vector4>();  // List tracking all the 2D boxes defining an obstacle area
        protected List<Vector4> pinBounds = new List<Vector4>();        // List tracking all the 2D boxes defining the pin collisions and side areas

        enum Lists { OBSTACLES, PINS};

        protected void Start()
        {
            
            BoxCollider2D box = null;

            // Filling obstaclesBounds list
            Transform setting = GameObject.Find("Setting").transform;

            foreach (Transform tr in setting)
            {
                box = tr.GetComponent<BoxCollider2D>();
                if (box != null)
                {
                    AddBox(box, Lists.OBSTACLES);
                }
            }

            Transform plate = GameObject.Find("Plate").transform;

            box = plate.GetComponent<BoxCollider2D>();
            AddBox(box, Lists.OBSTACLES);


            // Filling pinBounds list
            Transform pins = GameObject.Find("Pins").transform;

            foreach (Transform tr in pins)
            {
                box = tr.GetComponent<BoxCollider2D>();
                if (box != null)
                {
                    AddBox(box, Lists.PINS);
                }
            }


        }

        public bool IsPressed()
        {
            return pressed;
        }

        public Vector2 GetPosition()
        {
            return Camera.main.ScreenToWorldPoint(pressPosition);
        }

        // Adds a box element to the selected list
        void AddBox(BoxCollider2D box, Lists list)
        {
            if (list == Lists.OBSTACLES)
            {
                obstaclesBounds.Add(new Vector4(box.bounds.center.x - box.bounds.extents.x,             // box bot left corner
                                                        box.bounds.center.y - box.bounds.extents.y,
                                                        box.bounds.center.x + box.bounds.extents.x,     // box top right corner
                                                        box.bounds.center.y + box.bounds.extents.y));
            }
            else if (list == Lists.PINS)
            {
                pinBounds.Add(new Vector4(box.bounds.center.x - box.bounds.extents.x,           // box bot left corner
                                              box.bounds.center.y - box.bounds.extents.y,
                                              box.bounds.center.x + box.bounds.extents.x,       // box top right corner
                                              box.bounds.center.y + box.bounds.extents.y));
            }
        }

        // Checks if a position is found on an obstacle
        public bool IsOnObstacle(Vector2 pos, bool checkPins)
        {
            foreach (Vector4 box in obstaclesBounds)
            {
                if (pos.x > box.x && pos.x < box.z && pos.y > box.y && pos.y < box.w)
                {
                    return true;
                }
            }

            if (checkPins)
            {
                foreach (Vector4 box in pinBounds)
                {
                    if (pos.x > box.x && pos.x < box.z && pos.y > box.y && pos.y < box.w)
                    {
                        return true;
                    }
                }
            }


            return false;
        }

        // Checks if any of the [pointsNbr] positions between two point is found on an obstacle
        public bool IsAnyOnObstacle(Vector2 initPos, Vector2 finalPos, int pointsNbr, bool checkPins)
        {
            Vector2 direction = finalPos - initPos;
            float distance = direction.magnitude;
            direction = direction.normalized;

            for (int i = 0; i < pointsNbr; i++)
            {
                if (IsOnObstacle(initPos + direction * i * distance / pointsNbr, checkPins))
                {
                    return true;
                }
            }

            return false;
        }

    }
}