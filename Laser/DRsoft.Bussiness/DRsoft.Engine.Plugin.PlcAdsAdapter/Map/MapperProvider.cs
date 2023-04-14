using AutoMapper;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter.Map
{
    /// <summary>
    /// 
    /// </summary>
    public class MapperProvider
    {
        private IMapper autoMapper;

        public MapConfigProvider Provider { get; }
        public IMapper Mapper => autoMapper;

        public MapperProvider(MapConfigProvider provider)
        {
            Provider = provider;
            BuildAutoMapper();
        }

        /// <summary>
        /// 构建AutoMapper
        /// </summary>
        private void BuildAutoMapper()
        {
            autoMapper = AutoMapperConfig().CreateMapper();
        }

        /// <summary>
        /// 构建真实对象的映射
        /// </summary>
        /// <returns></returns>
        private MapperConfiguration AutoMapperConfig()
        {
            /*
             * 实体与字段的映射
             */
            return new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<AuthorModel, AuthorDTO>()
                //.ForMember(d => d.ID1, o => o.MapFrom(x => x.ID))
                //.ForMember(d => d.D1, o => o.MapFrom(x => Process(x.D)));
            });
        }
    }
}