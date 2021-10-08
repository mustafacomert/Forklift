using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarController : MonoBehaviour
{
    public static event Action OnFuelHasRunOutEvent;

    private const int SECONDS = 5;
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
    void Awake()
    {
        fuelBarImg = GetComponent<Image>();
        StartCoroutine(ReduceFuel(SECONDS));
    }

    private IEnumerator ReduceFuel(int seconds)
    {
        while(fuelBarImg.fillAmount > 0)
        {
            yield return new WaitForSeconds(seconds);
            UpdateFillAmountOfBar(-0.1f);
            
        }
    }

    private void OnFuelCollected(int fuelAmount)
    {
        float fillAmountIncrease = (float)fuelAmount / 100;
        UpdateFillAmountOfBar(fillAmountIncrease);
    }

    private void UpdateFillAmountOfBar(float amount)
    {
        fuelBarImg.fillAmount += amount;
        if (fuelBarImg.fillAmount <= 0)
        {
            OnFuelHasRunOutEvent?.Invoke();
        }
    }
}
