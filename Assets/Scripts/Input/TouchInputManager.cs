/*
 * INPUT MANAGER
 * 
 * Deals with the touch inputs. It is the class used on the actual devices
 * 
 * */

using UnityEngine;


namespace HanoiTower
{
    public class TouchInputManager : InputManager
    {
        private void Update()
        {
            if (Input.touchCount > 0)
            {
                pressed = true;
            }

            if (pressed)
            {
                pressPosition.Set(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended && pressed)
            {
                pressed = false;
            }

        }
    }
}