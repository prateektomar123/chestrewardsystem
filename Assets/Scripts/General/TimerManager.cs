using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    private Chest activeChest;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool CanStartTimer()
    {
        Debug.Log($"TimerManager: CanStartTimer? activeChest is {(activeChest == null ? "null" : "not null")}");
        return activeChest == null;
    }

    public void StartTimer(Chest chest)
    {
        if (activeChest != null)
        {
            Debug.LogWarning($"A chest is already unlocking in slot {activeChest.slotIndex}! Cannot start a new timer.");
            return;
        }

        Debug.Log($"TimerManager: Starting timer for chest in slot {chest.slotIndex}");
        activeChest = chest;
    }

    public void StopTimer(Chest chest)
    {
        if (activeChest == chest)
        {
            activeChest = null;
        }
    }

    void Update()
{
    if (activeChest != null)
    {
        Debug.Log($"TimerManager: Updating activeChest in slot {activeChest.slotIndex}");
        activeChest.Update();
    }
}
}