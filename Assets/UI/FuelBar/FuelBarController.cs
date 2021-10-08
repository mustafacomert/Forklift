using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarController : MonoBehaviour
{
    private const int SECONDS = 2;
    private Image fuelBarImg;

    private void OnEnable()
    {
        FuelCanController.OnFuelCollectedEvent -= OnFuelCollected;
        FuelCanController.OnFuelCollectedEvent += OnFuelCollected;
    }


    private void OnDisable()
    {
        FuelCanController.OnFuelCollectedEvent -= OnFuelCollected;
    }


    // Start is called before the first frame update
    void Start()
    {
        fuelBarImg = GetComponent<Image>();
        StartCoroutine(ReduceFuel(SECONDS));
    }

    private IEnumerator ReduceFuel(int seconds)
    {
        while(fuelBarImg.fillAmount > 0)
        {
            yield return new WaitForSeconds(seconds);
            fuelBarImg.fillAmount -= 0.1f;
        }
        Debug.Log("	the gas has run out");
    }

    private void OnFuelCollected(int fuelAmount)
    {
        float fillAmountIncrease = (float)fuelAmount / 100;
        fuelBarImg.fillAmount += fillAmountIncrease;
    }
}
