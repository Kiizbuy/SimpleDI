namespace SimpleDI.Providers
{
    public interface IObjectProvider<T> where T : class
    {
        T GetObject(DiContainer diContainer);
    }
}