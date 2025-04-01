using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestSlotUI : MonoBehaviour
{
    [SerializeField] private Image chestImage;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button startTimerButton;
    [SerializeField] private Button unlockWithGemsButton;
    [SerializeField] private Button collectButton;
    private Chest chest;
    private ChestController chestController;

    void Awake()
    {
        chestController = FindObjectOfType<ChestController>();
        //startTimerButton.onClick.AddListener(OnStartTimerButton);
        unlockWithGemsButton.onClick.AddListener(OnUnlockWithGemsButton);
        collectButton.onClick.AddListener(OnCollectButton);
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
        collectButton.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (chest == null) return;

        bool isLocked = chest.remainingTime == chest.chestType.timerInMinutes * 60f;
        bool isCollected = chest.remainingTime <= 0;

        timerText.text = isCollected ? "Collect" : FormatTime(chest.remainingTime);
        startTimerButton.gameObject.SetActive(isLocked);
        unlockWithGemsButton.gameObject.SetActive(!isLocked && !isCollected);
        collectButton.gameObject.SetActive(isCollected);
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
        Debug.Log($"StartTimerButton clicked for chest in slot {chest.slotIndex}");
        ICommand command = new StartTimerCommand(chest);
        command.Execute();
        chestController.StartTimer(chest.slotIndex);
    }
    else
    {
        Debug.LogError("StartTimerButton clicked, but chest is null!");
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

    public void OnCollectButton()
    {
        if (chest != null)
        {
            chestController.CollectRewards(chest.slotIndex);
        }
    }
}