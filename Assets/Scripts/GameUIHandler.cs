using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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

    [SerializeField]
    private List<ItemCardButtonHandler> itemCardButtonHandlers;

    [SerializeField]
    private TMP_Text currentLevelText;

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
        xpSlider.DOValue(currentSliderValue, 0.05f);

        if (currentSliderValue != 1f)
        {
            xpSliderFillImage.color = new Color(0.5181223f, 0.9528302f, 0.4449537f);
        }
        else
        {
            xpSliderFillImage.color = Color.yellow;
        }
    }

    public void ToggleLevelUpScreen(bool show)
    {
        if (show)
        {
            levelUpScreen.SetActive(true);
            currentLevelText.text = "Level - <b>" + PlayerLevelUpController.Instance.GetCurrentLevel() + "</b>";

            List<BaseItem> itemsToShow = ItemsHandler.Instance.GetItemsToShowOnLevelUp(3);

            for (int i = 0; i < itemsToShow.Count; i++)
            {
                itemCardButtonHandlers[i].SetUpItemCard(itemsToShow[i]);
            }

        }
        else
        {
            levelUpScreen.SetActive(false);
        }
    }
}
