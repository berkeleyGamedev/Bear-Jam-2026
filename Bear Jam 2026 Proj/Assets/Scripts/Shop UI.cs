using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject GoldenTicketButton;
    public GameObject ExtraComboButton;
    public GameObject PlushieSoulButton;
    public TextMeshProUGUI DescriptionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeDescription("Hover over things to see what they do! Click to Buy!!!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDescription(string description)
    {
        DescriptionText.text = description;
    }

    public void BuyGoldenTicket()
    {
        Debug.Log("Bought Golden Ticket");
        TextMeshProUGUI buttonText = GoldenTicketButton.GetComponentInChildren<TextMeshProUGUI>();
        if(buttonText != null)
        {
            buttonText.text = "SOLD";
        }
    }

    public void BuyExtraCombo()
    {
        Debug.Log("Bought Extra Combo");
        TextMeshProUGUI buttonText = ExtraComboButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = "SOLD";
        }
    }

    public void BuyPlushieSoul()
    {
        Debug.Log("Bought Plushie Soul");
        TextMeshProUGUI buttonText = PlushieSoulButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = "SOLD";
        }
    }
}
