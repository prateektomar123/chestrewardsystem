using UnityEngine;
using System.Collections.Generic;

public class ChestSlotManager : MonoBehaviour
{
    public static ChestSlotManager Instance { get; private set; }

    [SerializeField] private ChestType[] chestTypes;
    [SerializeField] private int maxSlots = 4;
    [SerializeField] private Popup slotsFullPopup;
    [SerializeField] private ChestSlotUI[] chestSlotUIs;
    private List<Chest> chestSlots;
    private Queue<Chest> chestQueue; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        chestSlots = new List<Chest>(new Chest[maxSlots]);
        chestQueue = new Queue<Chest>();

        if (chestSlotUIs.Length != maxSlots)
        {
            Debug.LogError($"ChestSlotManager: Expected {maxSlots} ChestSlotUIs, but found {chestSlotUIs.Length}.");
        }
    }

    public bool HasEmptySlot()
    {
        return chestSlots.Contains(null);
    }

    private int GetFirstEmptySlotIndex()
    {
        return chestSlots.IndexOf(null);
    }

    public bool AddChest()
    {
        if (!HasEmptySlot())
        {
            Debug.Log("All slots are full!");
            if (slotsFullPopup != null)
            {
                slotsFullPopup.Show();
            }
            else
            {
                Debug.LogError("SlotsFullPopup is not assigned in ChestSlotManager!");
            }
            return false;
        }

        if (chestTypes == null || chestTypes.Length == 0)
        {
            Debug.LogError("ChestTypes array is empty in ChestSlotManager!");
            return false;
        }

        ChestType randomChestType = chestTypes[Random.Range(0, chestTypes.Length)];
        int slotIndex = GetFirstEmptySlotIndex();
        Chest newChest = new Chest(randomChestType, slotIndex);
        chestSlots[slotIndex] = newChest;

        if (slotIndex >= 0 && slotIndex < chestSlotUIs.Length)
        {
            chestSlotUIs[slotIndex].SetChest(newChest);
        }
        else
        {
            Debug.LogError($"Invalid slot index {slotIndex} for chestSlotUIs.");
        }

        
        if (!TimerManager.Instance.CanStartTimer())
        {
            chestQueue.Enqueue(newChest);
            Debug.Log($"Chest added to queue. Queue size: {chestQueue.Count}");
        }

        return true;
    }

    public void RemoveChest(int slotIndex)
    {
        chestSlots[slotIndex] = null;
        if (slotIndex >= 0 && slotIndex < chestSlotUIs.Length)
        {
            chestSlotUIs[slotIndex].Clear();
        }

        
        if (chestQueue.Count > 0 && TimerManager.Instance.CanStartTimer())
        {
            Chest nextChest = chestQueue.Dequeue();
            nextChest.StartTimer();
            Debug.Log($"Started timer for next chest in queue. Queue size: {chestQueue.Count}");
        }
    }

    public Chest GetChest(int slotIndex)
    {
        return chestSlots[slotIndex];
    }
}