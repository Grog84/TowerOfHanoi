/*
 * RING ENTITY
 * 
 * COMPONENT procedurally generating the ring.
 * It assigns the proper sprite (according to the information contained in the ring RingDescription)
 * to the ring and modifies the size of the colliders accordingly
 * 
 * */

using UnityEngine;

namespace HanoiTower.Ring
{
    public class RingSpritesController : MonoBehaviour
    {

        RingDataHandler ringData;

        SpriteRenderer FrontRenderer;       // Ring Sprite renderers
        SpriteRenderer BackRenderer;

        SpriteRenderer ExpressionRenderer;  // Facial expression Sprite renderer

        GameObject Glow;                    // Glow effect Game object used for activation and deactivation of the glow effect

        SpriteRenderer GlowFrontRenderer;   // Glow effect Sprite renderers
        SpriteRenderer GlowBackRenderer;

        BoxCollider2D boxL;                 // Collider representing the solid ring part on the Left of the hole
        BoxCollider2D boxR;                 // Collider representing the solid ring part on the Right of the hole

        BoxCollider2D boxHole;              // Collider representing the ring hole

        BoxCollider2D boxSelection;         // Collider used for the grabbing raycast

        bool glowActive = false;            // Glow activation status

        void Awake()
        {
            ringData = GetComponent<RingDataHandler>();

            FrontRenderer = transform.Find("Front").GetComponent<SpriteRenderer>();
            BackRenderer = transform.Find("Back").GetComponent<SpriteRenderer>();

            Glow = transform.Find("Glow").gameObject;

            GlowFrontRenderer = Glow.transform.Find("GlowFront").GetComponent<SpriteRenderer>();
            GlowBackRenderer = Glow.transform.Find("GlowBack").GetComponent<SpriteRenderer>();

            BoxCollider2D[] boxs = GetComponents<BoxCollider2D>();

            boxL = boxs[0];
            boxR = boxs[1];

            boxSelection = transform.Find("Selection").GetComponent<BoxCollider2D>();
            boxHole = transform.Find("Hole").GetComponent<BoxCollider2D>();
        }

        void Start()
        {
            FrontRenderer.sprite = ringData.description.front;
            BackRenderer.sprite = ringData.description.back;

            boxSelection.size = new Vector2(FrontRenderer.sprite.bounds.size.x, (FrontRenderer.sprite.bounds.size.y) * 0.7f);

            boxL.size = new Vector2((boxSelection.size.x - boxHole.size.x) / 2.0f, FrontRenderer.sprite.bounds.size.y);
            boxR.size = boxL.size;

            boxL.offset = new Vector2(-(boxL.size.x / 2.0f + boxHole.size.x / 2.0f), 0f);
            boxR.offset = new Vector2(boxL.size.x / 2.0f + boxHole.size.x / 2.0f, 0f);

            GlowFrontRenderer.sprite = ringData.description.frontGlow;
            GlowBackRenderer.sprite = ringData.description.backGlow;

            if (ringData.description.Expressions.Length > 0)
            {
                ExpressionRenderer = transform.Find("Expression").GetComponent<SpriteRenderer>();

                SetExpression(RingExpression.HAPPY);

                gameObject.AddComponent<RingExpressionController>();
            }

        }

        public void SetExpression(RingExpression val)
        {
            ExpressionRenderer.sprite = ringData.description.Expressions[(int)val];
        }

        public void SetGlowActive(bool val)
        {
            Glow.SetActive(val);
        }

        private void Update()
        {
            if (ringData.GetBoolValue("grabbed") && !glowActive)
            {
                glowActive = true;
                SetGlowActive(true);
            }

            else if (glowActive && !ringData.GetBoolValue("grabbed"))
            {
                glowActive = false;
                SetGlowActive(false);
            }
        }

    }
}