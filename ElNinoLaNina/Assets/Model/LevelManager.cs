public interface LevelManager
{
    // checks if conditions are satisfied for a win, if true call PlayerWin()
    public void AttemptWin();

    // called if prop detects a death
    public void PlayerDeath(string cause);

    // called by AttemptWin(), updates save state's current level, transitions to next level
    public void PlayerWin();
}