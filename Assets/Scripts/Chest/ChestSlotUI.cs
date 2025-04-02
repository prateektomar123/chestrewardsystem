using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlotUI : MonoBehaviour
{
    [SerializeField] private Image chestImage;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button startTimerButton;
    [SerializeField] private Button unlockWithGemsButton;
    [SerializeField] private TextMeshProUGUI unlockWithGemsButtonText;
    [SerializeField] private Button collectButton;
    [SerializeField] private Button undoButton; 
    private Chest chest;
    private ChestController chestController;
    private bool canUndo; 

    void Awake()
    {
        chestController = FindObjectOfType<ChestController>();
        startTimerButton.onClick.AddListener(OnStartTimerButton);
        unlockWithGemsButton.onClick.AddListener(OnUnlockWithGemsButton);
        collectButton.onClick.AddListener(OnCollectButton);
        undoButton.onClick.AddListener(OnUndoButton);
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
        undoButton.gameObject.SetActive(false);
        canUndo = false;
    }

    private void UpdateUI()
    {
        if (chest == null) return;

        bool isLocked = chest.remainingTime == chest.chestType.timerInMinutes * 60f;
        bool isCollected = chest.remainingTime <= 0;

        //Debug.Log($"UpdateUI: remainingTime = {chest.remainingTime}, isCollected = {isCollected}, isLocked = {isLocked}");

        timerText.text = isCollected ? "Collect" : FormatTime(chest.remainingTime);
        startTimerButton.gameObject.SetActive(isLocked);
        unlockWithGemsButton.gameObject.SetActive(!isLocked && !isCollected);
        collectButton.gameObject.SetActive(isCollected);

        if (undoButton != null)
        {
            undoButton.gameObject.SetActive(canUndo);
            //Debug.Log($"UpdateUI: canUndo = {canUndo}, undoButton active = {undoButton.gameObject.activeSelf}");
        }
        else
        {
            //Debug.LogError("UndoButton is not assigned in ChestSlotUI!");
        }
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
            chestController.StartTimer(chest.slotIndex);
        }
    }

    public void OnUnlockWithGemsButton()
    {
        if (chest != null)
        {
            canUndo = true; 
            Debug.Log($"OnUnlockWithGemsButton: canUndo set to {canUndo}");
            ICommand command = new UnlockWithGemsCommand(chest);
            CommandManager.Instance.ExecuteCommand(command);
            //chestController.UnlockWithGems(chest.slotIndex);
            UpdateUI();
        }
    }

    public void OnCollectButton()
    {
        if (chest != null)
        {
            chestController.CollectRewards(chest.slotIndex);
            canUndo = false; 
        }
    }

    public void OnUndoButton()
    {
        CommandManager.Instance.Undo();
        canUndo = false; 
        UpdateUI(); 
    }
}