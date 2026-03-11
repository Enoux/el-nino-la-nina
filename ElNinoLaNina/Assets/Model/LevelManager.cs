public interface LevelManager
{
    // checks if conditions are satisfied for a win, if true call PlayerWin()
    public void AttemptWin();

    // called by AttemptWin(), updates save state's current level, transitions to next level
    public void PlayerWin();
}