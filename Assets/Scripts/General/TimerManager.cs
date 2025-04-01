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
        return activeChest == null;
    }

    public void StartTimer(Chest chest)
    {
        if (activeChest != null)
        {
            Debug.LogWarning("A chest is already unlocking! Cannot start a new timer.");
            return;
        }

        activeChest = chest;
    }

    public void StopTimer(Chest chest)
    {
        if (activeChest == chest)
        {
            activeChest = null;
            
            ChestSlotManager.Instance.RemoveChest(chest.slotIndex);
        }
    }

    void Update()
    {
        if (activeChest != null)
        {
            activeChest.Update();
        }
    }
}