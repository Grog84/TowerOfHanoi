/*
 * RING ENTITY
 * 
 * COMPONENT animating the ring.
 * Is animating the ring when it is positioned in a wrong pin
 * 
 * */

using System.Collections;
using UnityEngine;


namespace HanoiTower.Ring
{
    public class RingAnimation : MonoBehaviour
    {
        RingDataHandler ringData;

        Rigidbody2D rb;

        bool unPinCOActive = false;

        void Awake()
        {
            ringData = GetComponent<RingDataHandler>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (ringData.GetBoolValue("pinned") && !ringData.GetBoolValue("pinCorrect") && !ringData.GetBoolValue("grabbed")  // ring left in the wrong position
                && !unPinCOActive)
            {
                unPinCOActive = true;
                StartCoroutine(UnPinCO());
            }

            if (unPinCOActive && ringData.GetBoolValue("grabbed"))
            {
                unPinCOActive = false;
            }
            
        }

        IEnumerator UnPinCO()
        {
            while (transform.position.y < 1.2f)
            {
                rb.AddForce(Vector2.up * 10f);

                if (!unPinCOActive)
                {
                    yield break;
                }

                yield return null;
            }

            // Arbitrary values that could be exposed for design purpose
            rb.AddForce(new Vector2(0.5f, 0.5f) * 100f);

            unPinCOActive = false;
        }

    }
}