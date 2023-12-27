using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
    public static GameUIHandler Instance;

    [SerializeField]
    private Slider xpSlider;

    [SerializeField]
    private Image xpSliderFillImage;

    [SerializeField]
    private GameObject levelUpScreen;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        xpSlider.value = 0;
        PlayerLevelUpController.Instance.OnXPChange += UpdateSlider;
    }

    private void OnDisable()
    {
        PlayerLevelUpController.Instance.OnXPChange -= UpdateSlider;
    }

    private void UpdateSlider(int currentXP, int xpNeeded)
    {
        float currentSliderValue = currentXP / (float)xpNeeded;
        xpSlider.value = currentSliderValue;

        if (xpSlider.value != 1f)
        {
            xpSliderFillImage.color = new Color(0.5181223f, 0.9528302f, 0.4449537f);
        }
        else
        {
            xpSliderFillImage.color = Color.yellow;
        }
    }

    public void ToggleLevelUpScreen()
    {
        levelUpScreen.SetActive(!levelUpScreen.activeSelf);
    }
}
