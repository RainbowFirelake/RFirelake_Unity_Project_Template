using RFirelake.Infrastructure.AssetProvider;
using RFirelake.Infrastructure.Factories;
using RFirelake.Infrastructure.Logs;
using VContainer;
using VContainer.Unity;

namespace RFirelake.Architecture
{
    public class ProjectScope : LifetimeScope
    {
        [UnityEngine.SerializeField]
        private LoggerConfiguration _loggerConfiguration;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register(typeof(Logger<>), Lifetime.Transient)
                .AsImplementedInterfaces()
                .WithParameter<ILoggerConfiguration>(_loggerConfiguration);

            builder.Register<IAssetProvider, AddressableAssetProvider>(Lifetime.Transient);
            builder.Register<GameObjectFactory>(Lifetime.Singleton);
        }
    }
}
