/*
 * RING ENTITY
 * 
 * COMPONENT animating the facial expression of the rings
 * 
 * */

using UnityEngine;

namespace HanoiTower.Ring
{
    public class RingExpressionController : MonoBehaviour
    {
        RingDataHandler ringData;
        RingSpritesController SpritesController;

        RingExpression currentExpression = RingExpression.HAPPY;

        private void Awake()
        {
            ringData = GetComponent<RingDataHandler>();
            SpritesController = GetComponent<RingSpritesController>();
        }

        private void Update()
        {
            RingExpression desiredExpression = ringData.GetExpressionValue();
            if (desiredExpression != currentExpression)
            {
                SpritesController.SetExpression(desiredExpression);
                currentExpression = desiredExpression;
            }
        }
    }
}