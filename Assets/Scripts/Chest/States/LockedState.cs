using UnityEngine;

public class LockedState : IChestState
{
    public void EnterState(Chest chest)
    {
        chest.remainingTime = chest.chestType.timerInMinutes * 60f;
    }

    public void UpdateState(Chest chest)
    {
        
    }

    public void OnStartTimer(Chest chest)
    {
        if (TimerManager.Instance.CanStartTimer())
        {
            chest.SetState(new UnlockingState());
        }
        else
        {
            Debug.Log("Cannot start timer: Another chest is already unlocking.");
        }
    }

    public void OnUnlockWithGems(Chest chest)
    {
        
    }
}