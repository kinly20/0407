using Autofac;
using DRsoft.Modeling.Provider;
using DRsoft.Runtime.Core.Platform.Module;
using DRsoft.Runtime.Core.Platform.Services;

namespace DRsoft.Modeling
{
    public class ModelingModule : AbstractModule
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="builder"></param>
        protected override void RegisterBuilder(ContainerBuilder builder)
        {
            builder.RegisterType<ModelingProvider>().As<IModelingProvider>().SingleInstance();
        }

        public override void Initialize(AppModuleContext context)
        {
            base.Initialize(context);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
