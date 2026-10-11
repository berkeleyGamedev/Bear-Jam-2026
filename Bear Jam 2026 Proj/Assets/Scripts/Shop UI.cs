using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject GoldenTicketButton;
    public GameObject ExtraComboButton;
    public GameObject PlushieSoulButton;
    public TextMeshProUGUI DescriptionText;
    public TextMeshProUGUI CandyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ChangeDescription("Hover over things to see what they do! Click to Buy!!!");
    }

    // Update is called once per frame
    void Update()
    {
        CandyText.text = "Total Candy: " + ScoreTracker.instance.totalCandy;
    }

    public void ChangeDescription(string description)
    {
        DescriptionText.text = description;
    }

    public void BuyGoldenTicket(int cost)
    {
        if (GameManager.instance.HasGoldenTicket) return;
        if (ScoreTracker.instance.totalCandy < cost) return;
        ScoreTracker.instance.totalCandy -= cost;
        Debug.Log("Bought Golden Ticket");
        GameManager.instance.HasGoldenTicket = true;
        TextMeshProUGUI buttonText = GoldenTicketButton.GetComponentInChildren<TextMeshProUGUI>();
        if(buttonText != null)
        {
            buttonText.text = "SOLD";
        }
    }

    public void BuyExtraCombo(int cost)
    {
        if (GameManager.instance.HasExtraCombo) return;
        if (ScoreTracker.instance.totalCandy < cost) return;
        ScoreTracker.instance.totalCandy -= cost;
        Debug.Log("Bought Extra Combo");
        GameManager.instance.HasExtraCombo = true;
        TextMeshProUGUI buttonText = ExtraComboButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = "SOLD";
        }
    }

    public void BuyPlushieSoul(int cost)
    {
        if (GameManager.instance.HasPlushieSoul) return;
        if (ScoreTracker.instance.totalCandy < cost) return;
        ScoreTracker.instance.totalCandy -= cost;
        Debug.Log("Bought Plushie Soul");
        GameManager.instance.HasPlushieSoul = true;
        TextMeshProUGUI buttonText = PlushieSoulButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = "SOLD";
        }
    }
}
