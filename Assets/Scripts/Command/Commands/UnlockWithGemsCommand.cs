using UnityEngine;

public class UnlockWithGemsCommand : ICommand
{
    private Chest chest;
    private int gemCost;

    public UnlockWithGemsCommand(Chest chest)
    {
        this.chest = chest;
        this.gemCost = chest.CalculateGemCost();
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
        PlayerData.Instance.AddGems(gemCost); 
        chest.UndoUnlockWithGems(); 
        Debug.Log($"Undo UnlockWithGems: Refunded {gemCost} gems and restored chest state.");
    }
}