public struct GameActions
{
    public GameAction Action;

    public GameActions(GameAction action)
    {
        Action = action;
    }
}

public enum GameAction
{
    StartGame,
    ExitGame,
    OpenSettings,
    OpenShop,
    CloseButton
}