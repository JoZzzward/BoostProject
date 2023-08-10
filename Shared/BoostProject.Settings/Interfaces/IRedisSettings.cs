namespace BoostProject.Settings.Interfaces;

public interface IRedisSettings
{
    int CacheLifeTime { get;  }
    
    string Uri { get; } 
}