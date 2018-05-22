/*
 * RING ENTITY
 * 
 * COMPONENT dealing with the grabbing state of the rings
 * 
 * */

using System.Collections;
using UnityEngine;

namespace HanoiTower.Ring
{
    public class RingGrabController : MonoBehaviour
    {
        GameManager gm;
        RingDataHandler ringData;

        bool isGrabbing;

        RaycastHit2D hit;
        float rotationSpeed;

        Vector2 grabOffset;

        void Start()
        {
            gm = FindObjectOfType<GameManager>();
            ringData = GetComponent<RingDataHandler>();

            isGrabbing = false;

            rotationSpeed = ringData.GetFloatValue("rotationSpeed");
        }

        void Update()
        {
            if (gm.input.IsPressed() && !isGrabbing)
            {
                hit = Physics2D.Raycast(gm.input.GetPosition(), Vector3.forward, float.MaxValue , 1 << LayerMask.NameToLayer("Selection"));
                if (hit.collider != null)
                {
                    if (hit.transform == transform)
                    {
                        StartGrabbing();
                    }
                }
            }
            else if (gm.input.IsPressed() && isGrabbing)
            {
                hit = Physics2D.Raycast(gm.input.GetPosition(), Vector3.forward, float.MaxValue, 1 << LayerMask.NameToLayer("Selection"));
                if (hit.collider == null || hit.transform != transform)
                {
                    StopGrabbing();
                }
            }

            if (isGrabbing && !gm.input.IsPressed())
            {
                StopGrabbing();
            }

        }

        void StartGrabbing()
        {
            isGrabbing = true;
            ringData.SetDataValue("grabbed", true);
            ringData.SetDataValue(RingExpression.WONDERING);

            grabOffset = gm.input.GetPosition() - transform.position.ToVector2();

            ringData.SetDataValue("grabOffset", grabOffset);

            StartCoroutine(RotateHorizontal());

        }

        void StopGrabbing()
        {
            isGrabbing = false;
            ringData.SetDataValue("grabbed", false);

            if (ringData.GetBoolValue("pinned") && !ringData.GetBoolValue("pinCorrect"))
            {
                ringData.SetDataValue(RingExpression.WORRIED);
            }
            else { ringData.SetDataValue(RingExpression.HAPPY); }
        }

        IEnumerator RotateHorizontal()
        {
            Quaternion targetRotation = Quaternion.identity;
            while (transform.rotation != targetRotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                yield return null;
            }
        }

    }
}