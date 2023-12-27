public abstract class BaseGameState
{
    public abstract void OnStateEnter(GameManager gameManager);
    public abstract void StateUpdate(GameManager gameManager);
    public abstract void OnStateExit(GameManager gameManager);
}