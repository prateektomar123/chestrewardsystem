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
        chest.SetState(new UnlockingState());
    }

    public void OnUnlockWithGems(Chest chest)
    {
        
    }
}