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
    }

    private void OnDisable()
    {
        FuelBarController.OnFuelHasRunOutEvent -= OnFuelHasRunOut;
        BoxController.OnBoxDroppedEvent -= OnBoxDropped;
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnFuelHasRunOut()
    {
        Debug.Log("gas is over");
        RestartLevel();
    }


    private void OnBoxDropped()
    {
        Debug.Log("box is dropped");
        RestartLevel();
    }

}
