using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void OnEnable()
    {
        FuelBarController.OnFuelHasRunOutEvent -= OnFuelHasRunOut;
        FuelBarController.OnFuelHasRunOutEvent += OnFuelHasRunOut;

        BoxController.OnBoxDroppedEvent -= OnBoxDropped;
        BoxController.OnBoxDroppedEvent += OnBoxDropped;

        PendulumHead.OnHitForkliftEvent -= OnHitForklift;
        PendulumHead.OnHitForkliftEvent += OnHitForklift;
    }

    private void OnDisable()
    {
        FuelBarController.OnFuelHasRunOutEvent -= OnFuelHasRunOut;
        BoxController.OnBoxDroppedEvent -= OnBoxDropped;
        PendulumHead.OnHitForkliftEvent -= OnHitForklift;
    }


    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //gas has run out
    private void OnFuelHasRunOut()
    {
        RestartLevel();
    }

    //box dropped
    private void OnBoxDropped()
    {
        RestartLevel();
    }

    //forklift hit by pendulum
    private void OnHitForklift()
    {
        RestartLevel();
    }
}
