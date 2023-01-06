using UI;

public class PlayerStats
{
    private static PlayerStats _instance;
    private int _points;

    public static PlayerStats Instance => _instance ??= new PlayerStats();

    public void AddPoints(int count)
    {
        _points++;
        UIMediator.Instance.UpdatePoints(_points);
    }
}