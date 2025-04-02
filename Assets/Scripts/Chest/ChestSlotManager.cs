using UnityEngine;
using System.Collections.Generic;

public class ChestSlotManager : MonoBehaviour
{
    public static ChestSlotManager Instance { get; private set; }

    [SerializeField] private ChestType[] chestTypes;
    [SerializeField] private ChestSlotUI[] chestSlotUIs;
    [SerializeField] private Popup slotsFullPopup; 
    public Popup timerActivePopup; 

    private Chest[] chestSlots;
    private Queue<Chest> chestQueue = new Queue<Chest>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        chestSlots = new Chest[chestSlotUIs.Length];
    }

    public bool AddChest()
    {
        if (!HasEmptySlot())
        {
            Debug.Log("All slots are full!");
            if (slotsFullPopup != null)
            {
                slotsFullPopup.Show("All slots are full!");
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
            Debug.Log($"Adding chest to slot {slotIndex}");
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
            if (timerActivePopup != null)
            {
                timerActivePopup.Show("Another chest is already unlocking! This chest has been added to the queue.");
            }
            else
            {
                Debug.LogError("TimerActivePopup is not assigned in ChestSlotManager!");
            }
        }

        return true;
    }

    public bool HasEmptySlot()
    {
        for (int i = 0; i < chestSlots.Length; i++)
        {
            if (chestSlots[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    public int GetFirstEmptySlotIndex()
    {
        for (int i = 0; i < chestSlots.Length; i++)
        {
            if (chestSlots[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public Chest GetChest(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < chestSlots.Length)
        {
            return chestSlots[slotIndex];
        }
        return null;
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
}