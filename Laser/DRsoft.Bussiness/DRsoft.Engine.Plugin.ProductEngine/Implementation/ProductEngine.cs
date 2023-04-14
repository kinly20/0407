using DRsoft.Engine.Core.Control.AbstractController;
using DRsoft.Engine.Core.Engine;
using DRsoft.Runtime.Core.Platform.Events;

namespace DRsoft.Engine.Plugin.Engine.Product.Implementation
{
    /// <summary>
    /// 生产引擎
    /// </summary>
    public class ProductEngine : AbstractEngine
    {
        public ProductEngine() : base()
        {
        }

        public ProductEngine(AbstractController controller,
            IEventAggregator eventAggregator,
            EngineConfig config)
            : base(controller, eventAggregator, config)
        {
        }
    }
}