/*
 * RING ENTITY
 * 
 * COMPONENT dealing with the movement of the rings when they are grabbed
 * Physics is used when the rings are pinned, otherwise a simple translation is preferred
 * 
 * */

using System.Collections;
using UnityEngine;

namespace HanoiTower.Ring
{
    public class RingMovement : MonoBehaviour
    {
        GameManager gm;
        RingDataHandler ringData;

        Vector2 oldPosition;
        Vector2 deltaPosition;

        Vector2 deltaSpeedPosition;

        float speed;
        Vector2 speedVec;
        float inertia;

        Rigidbody2D rb;

        bool grabbed;

        void Start()
        {
            gm = FindObjectOfType<GameManager>();
            ringData = GetComponent<RingDataHandler>();

            speed = ringData.GetFloatValue("speed");
            speedVec = Vector2.zero;
            inertia = ringData.GetFloatValue("inertia");

            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (ringData.GetBoolValue("grabbed"))
            {
                if (!grabbed) // grab just started
                {
                    grabbed = true;
                    oldPosition = transform.position.ToVector2() + ringData.GetVec2Value("grabOffset");
                }

                GetDeltaPos(gm.input.GetPosition());

                StartCoroutine(RecordSpeedCO());

                if (ringData.GetBoolValue("pinned"))                // The ring is in the pin 
                {

                    float translationValue = deltaPosition.y;

                    if (ringData.GetCollisionList().Count > 0)      // The ring is colliding with another ring
                    {
                        foreach (RingColor cl in ringData.GetCollisionList())
                        {
                            if (cl < ringData.GetColorValue())
                            {
                                translationValue = Mathf.Max(0.02f, deltaPosition.y);  // The value 0.02 allows a small bump downward
                                break;
                            }
                            
                        }
                    }

                    // Move using Physics 
                    rb.MovePosition(transform.position.ToVector2() + (translationValue * speed * Time.deltaTime) * Vector2.up);

                    speedVec.Set(0, speedVec.y);
                }
                else
                {
                    // Move without using Physics for better response in safe conditions
                    transform.Translate(deltaPosition.x * speed * Time.deltaTime, deltaPosition.y * speed * Time.deltaTime, 0);
                }

                oldPosition = transform.position.ToVector2() + ringData.GetVec2Value("grabOffset");
            }
            else  // ring not currently grabbed
            {
                
                if (grabbed) // grab just finished
                {
                    grabbed = false;
                    
                    rb.AddForce(speedVec * inertia);
                }
            }
        }

        Vector2 GetDeltaPos(Vector2 newPosition)
        {
            // Checks if the current input is overlapping an obstcle
            if (gm.input.IsAnyOnObstacle(oldPosition, newPosition, 50, !ringData.GetBoolValue("pinned")))
            {
                newPosition = oldPosition;
            }
            
            deltaPosition = newPosition - oldPosition;

            return deltaPosition;
        }


        // The speed recording runs on slower update in order to prevent wrong feelings when the ring is released
        // The micro movements in the mouse or touch inputs just before the ring is released could in fact
        // cause undesired behaviours

        IEnumerator RecordSpeedCO()
        {
            Vector2 oldPos = oldPosition;
            Vector2 newPos = oldPosition;

            while (grabbed)
            {
                newPos = gm.input.GetPosition();
                deltaSpeedPosition = newPos - oldPos;
                oldPos = newPos;

                speedVec.Set(deltaSpeedPosition.x, deltaSpeedPosition.y);

                yield return new WaitForSeconds(0.1f);
            }

        }

    }
}