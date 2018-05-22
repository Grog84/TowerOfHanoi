/*
 * INPUT MANAGER
 * 
 * Deals with the mouse inputs. It is the class used in unity editor mode
 * 
 * */

using UnityEngine;

namespace HanoiTower
{
    public class MouseInputManager : InputManager
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                pressed = true;
            }

            if (pressed)
            {
                pressPosition.Set(Input.mousePosition.x, Input.mousePosition.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                pressed = false;
            }

        }
    }
}