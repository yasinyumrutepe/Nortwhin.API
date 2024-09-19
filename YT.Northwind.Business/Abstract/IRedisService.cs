

namespace 
    
    
    Northwind.Business.Abstract
{
    public interface IRedisService
    {
        T GetData<T>(string key);
        T SetData<T>(string key, T data);
        void Delete<T>(string key);

      

    }
}
