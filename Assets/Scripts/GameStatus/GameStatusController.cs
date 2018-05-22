/*
 * GAME STATUS ENTITY
 * 
 * COMPONENT dedicated to the timing of the different Game States
 * When a variation in the status is recorded a coroutine is started maintaining the state
 * for a given amount of time before setting it back to normal.
 * The states are assigned externally according to the game rules
 * 
 * */

using System.Collections;
using UnityEngine;

namespace HanoiTower.Status
{
    public enum GameStatus { NORMAL, RIGHTMOVE, WRONGMOVE, VICTORY }

    public class GameStatusController : MonoBehaviour
    {
        GameStatusDataHandler status;

        void Awake()
        {
            status = GetComponent<GameStatusDataHandler>();
        }

        void Start()
        {
            status.SetDataValue("targetPinID", 2);
            StartCoroutine(CheckStatusCO());
        }

        // Main Update Coroutine

        IEnumerator CheckStatusCO()
        {
            GameStatus currentStatus;
            while (true)
            {
                currentStatus = status.GetStatus();
                if (currentStatus != GameStatus.NORMAL)
                {
                    if (currentStatus == GameStatus.RIGHTMOVE)
                    {
                        yield return StartCoroutine(RightMoveCO());
                    }
                    else if (currentStatus == GameStatus.RIGHTMOVE)
                    {
                        yield return StartCoroutine(WrongMoveCO());
                    }
                    else
                    {
                        yield return StartCoroutine(VictoryCO());
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        // Status timing Coroutines

        IEnumerator RightMoveCO()
        {
            yield return new WaitForSeconds(3f);
            status.SetStatus(GameStatus.NORMAL);
        }

        IEnumerator WrongMoveCO()
        {
            yield return new WaitForSeconds(3f);
            status.SetStatus(GameStatus.NORMAL);
        }

        IEnumerator VictoryCO()
        {
            yield return new WaitForSeconds(5f);
            status.SetStatus(GameStatus.NORMAL);
        }

    }

}
