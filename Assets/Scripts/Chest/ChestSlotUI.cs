using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestSlotUI : MonoBehaviour
{
    [SerializeField] private Image chestImage;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button startTimerButton;
    [SerializeField] private Button unlockWithGemsButton;
    private Chest chest;

    void Awake()
    {
        startTimerButton.onClick.AddListener(OnStartTimerButton);
        unlockWithGemsButton.onClick.AddListener(OnUnlockWithGemsButton);
        Clear();
    }

    public void SetChest(Chest newChest)
    {
        if (chest != null)
        {
            chest.OnStateChanged -= UpdateUI;
        }

        chest = newChest;
        chestImage.sprite = chest.chestType.chestSprite;
        chest.OnStateChanged += UpdateUI;
        UpdateUI();
    }

    public void Clear()
    {
        if (chest != null)
        {
            chest.OnStateChanged -= UpdateUI;
        }

        chest = null;
        chestImage.sprite = null;
        timerText.text = "Empty";
        startTimerButton.gameObject.SetActive(false);
        unlockWithGemsButton.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (chest == null) return;

        timerText.text = chest.remainingTime > 0 ? FormatTime(chest.remainingTime) : "Collect";
        startTimerButton.gameObject.SetActive(chest.remainingTime == chest.chestType.timerInMinutes * 60f);
        unlockWithGemsButton.gameObject.SetActive(chest.remainingTime > 0);
    }

    private string FormatTime(float seconds)
    {
        int hours = (int)(seconds / 3600);
        int minutes = (int)((seconds % 3600) / 60);
        int secs = (int)(seconds % 60);
        return $"{hours:D2}H {minutes:D2}M {secs:D2}S";
    }

    public void OnStartTimerButton()
    {
        if (chest != null)
        {
            ICommand command = new StartTimerCommand(chest);
            command.Execute();
        }
    }

    public void OnUnlockWithGemsButton()
    {
        if (chest != null)
        {
            ICommand command = new UnlockWithGemsCommand(chest);
            command.Execute();
        }
    }
}