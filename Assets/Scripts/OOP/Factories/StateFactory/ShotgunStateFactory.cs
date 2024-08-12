public class ShotgunStateFactory : IEntityStateFactory
{
    GunConfig _config;

    public ShotgunStateFactory(ConfigBase configBase) =>
        _config = configBase.ShotgunConfig;

    public IEntityState CreateState()
    {
        return new GunState(_config);
    }
}
