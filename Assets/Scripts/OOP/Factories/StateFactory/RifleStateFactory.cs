public class RifleStateFactory : IEntityStateFactory
{
    GunConfig _config;

    public RifleStateFactory(ConfigBase configBase) =>
        _config = configBase.RifleConfig;

    public IEntityState CreateState()
    {
        return new GunState(_config);
    }
}
