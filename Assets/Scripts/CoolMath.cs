public class CoolMath
{
    public static float map(float value, float minStart, float maxStart, float minEnd, float maxEnd)
    {
        return minEnd + (value - minStart) * (maxEnd - minEnd) / (maxStart - minStart);
    }

}
