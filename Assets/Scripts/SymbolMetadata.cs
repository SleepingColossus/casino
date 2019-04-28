public class SymbolMetadata
{
    public SymbolMetadata(float angle, Reward reward)
    {
        Angle = angle;
        Reward = reward;
    }

    public float Angle { get; }
    public Reward Reward { get; }
}
