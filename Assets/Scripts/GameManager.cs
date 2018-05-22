/*
 * Game Manager
 * 
 * The game manager is the responsible of all
 * the operations that requires different game entities
 * 
 * */

using UnityEngine;

using HanoiTower.Pin;
using HanoiTower.Ring;

using HanoiTower.Setting;
using HanoiTower.Status;

namespace HanoiTower
{
    public class GameManager : MonoBehaviour
    {

        [HideInInspector] public InputManager input;

        DataHandler dh;
        GameStatusDataHandler gameStatus;

        void Awake()
        {
            #if UNITY_EDITOR
                    input = gameObject.AddComponent<MouseInputManager>();
            #else
                    input = gameObject.AddComponent<TouchInputManager>();
            #endif

            dh = new DataHandler();
            gameStatus = FindObjectOfType<GameStatusDataHandler>();
        }

        // The Camera To Screen and the Cloud Spawner are manually initialized
        // since the latter would require the infomration about the screen size in order to
        // operate te object pooling
        void Start()
        {
            FindObjectOfType<CameraToScreen>().Init();
            FindObjectOfType<CloudSpawner>().Init();
        }

        

        public void Pin(PinDataHandler pinData, Transform ringTransform)
        {
            RingDataHandler ringData = ringTransform.GetComponent<RingDataHandler>();
            RingColor ringColor = dh.GetRingColor(ringData);

            int pinOccupation = dh.GetPinOccupationValue(pinData);

            if (pinOccupation > (int)ringColor)     // ring in a wrong spot
            {
                dh.SetWrongOccupation(pinData, ringData, gameStatus);
            }
            else
            {
                if (gameStatus.GetIntValue("targetPinID") == pinData.GetID() && pinData.GetTopStackValue() == (int)RingColor.GREEN && pinData.GetStackLength() == 4)
                {
                    dh.SetVictory(pinData, ringData, gameStatus);
                }
                else
                {
                    dh.SetRightOccupation(pinData, ringData, gameStatus);
                }
            }


        }

        public void UnPin(PinDataHandler pinData, Transform ringTransform)
        {
            RingDataHandler ringData = ringTransform.GetComponent<RingDataHandler>();

            if (dh.GetRingPositionValidity(ringData))
            {
                dh.RemoveRing(pinData);
            }

            dh.UnPin(ringData);

        }


    }
}