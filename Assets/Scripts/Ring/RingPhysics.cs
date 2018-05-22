/*
 * RING ENTITY
 * 
 * COMPONENT checking the collision of the ring with the other rings
 * and controlling the gravity applied.
 * 
 * */

using UnityEngine;

namespace HanoiTower.Ring
{
    public class RingPhysics : MonoBehaviour
    {

        Rigidbody2D rb;
        RingDataHandler ringData;

        bool gravityActive = true;
        float gravityScale;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            gravityScale = rb.gravityScale;

            ringData = GetComponent<RingDataHandler>();
        }

        void Update()
        {
            if (ringData.GetBoolValue("grabbed") && gravityActive)
            {
                rb.gravityScale = 0f;
                gravityActive = false;

                rb.velocity = Vector2.zero;
            }

            if (!gravityActive && !ringData.GetBoolValue("grabbed"))
            {
                rb.gravityScale = gravityScale;
                gravityActive = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Ring")
            {
                ringData.AddToCollisionList(collision.transform.GetComponent<RingDataHandler>().GetColorValue());    
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.transform.tag == "Ring")
            {
                ringData.RemoveFromCollisionList(collision.transform.GetComponent<RingDataHandler>().GetColorValue());
            }
        }

    }
}