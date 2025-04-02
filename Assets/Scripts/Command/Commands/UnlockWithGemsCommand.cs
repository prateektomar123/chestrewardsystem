using UnityEngine;

public class UnlockWithGemsCommand : ICommand
{
    private Chest chest;
    private int gemCost;
    private float previousRemainingTime;
    private IChestState previousState;

    public UnlockWithGemsCommand(Chest chest)
    {
        this.chest = chest;
        this.gemCost = chest.CalculateGemCost();
        this.previousRemainingTime = chest.remainingTime; 
        this.previousState = chest.currentState; 
    }

    public void Execute()
    {
        if (PlayerData.Instance.SpendGems(gemCost))
        {
            chest.UnlockWithGems();
        }
    }

    public void Undo()
    {
        PlayerData.Instance.AddGems(gemCost); // Refund the gems
        chest.remainingTime = previousRemainingTime; // Restore the previous time
        chest.SetState(previousState); // Restore the previous state
        if (previousState is UnlockingState)
        {
            TimerManager.Instance.StartTimer(chest); // Resume timer if it was unlocking
        }
        Debug.Log($"Undo UnlockWithGems: Refunded {gemCost} gems, restored time to {previousRemainingTime}, state to {previousState.GetType().Name}");
    }
}