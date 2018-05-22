/*
 * PIN ENTITY
 * 
 * COMPONENT checking the trigger collision between the ring hole and the pin
 * It is calling the Game Manager in order to perform the pin and unpin actions.
 * 
 * */

using UnityEngine;

namespace HanoiTower.Pin
{
    public class PinRingConnection : MonoBehaviour
    {
        GameManager gm;
        PinDataHandler pinData;

        void Awake()
        {
            pinData = GetComponent<PinDataHandler>();    
        }

        void Start()
        {
            gm = FindObjectOfType<GameManager>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "RingHole")
            {
                gm.Pin(pinData, collision.transform.parent);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "RingHole")
            {
                gm.UnPin(pinData, collision.transform.parent);
            }
        }

    }
}