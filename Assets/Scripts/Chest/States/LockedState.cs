using UnityEngine;

public class LockedState : IChestState
{
    public void EnterState(Chest chest)
    {
        chest.remainingTime = chest.chestType.timerInMinutes * 60f;
    }

    public void UpdateState(Chest chest)
    {
        // No update needed in Locked state
    }

    public void OnStartTimer(Chest chest)
    {
        if (TimerManager.Instance.CanStartTimer())
        {
            Debug.Log($"LockedState: Transitioning to UnlockingState for chest in slot {chest.slotIndex}");
            TimerManager.Instance.StartTimer(chest);
            chest.SetState(new UnlockingState());
        }
        else
        {
            Debug.Log("Cannot start timer: Another chest is already unlocking.");
            if (ChestSlotManager.Instance != null && ChestSlotManager.Instance.timerActivePopup != null)
            {
                ChestSlotManager.Instance.timerActivePopup.Show("Another chest is already unlocking!");
            }
            else
            {
                Debug.LogError("TimerActivePopup is not assigned in ChestSlotManager!");
            }
        }
    }

    public void OnUnlockWithGems(Chest chest)
    {
        chest.remainingTime = 0;
        chest.SetState(new CollectedState());
    }
}