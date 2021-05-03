namespace Kinetix.Business.Abstract
{
    public interface IXmlManager<T>
    {
        string Serialize(T obj);
    }
}
