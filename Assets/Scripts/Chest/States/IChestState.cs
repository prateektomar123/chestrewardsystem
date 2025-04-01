public interface IChestState
{
    void EnterState(Chest chest);
    void UpdateState(Chest chest);
    void OnStartTimer(Chest chest);
    void OnUnlockWithGems(Chest chest);
}