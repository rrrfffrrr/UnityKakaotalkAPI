namespace Kakaotalk
{
    [System.Flags]
    public enum LoginMethod
    {
        Error = 0,
        Kakaotalk = 1,
        KakaoAccount = 2,
        Both = 3,
    }
}