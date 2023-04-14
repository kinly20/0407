using System;
using System.Collections.Generic;

namespace DRsoft.Engine.Model.Mapping
{
    public class Mpper
    {
        public Dictionary<string, MapperInfo> Dic = new Dictionary<string, MapperInfo>();

        public void Init()
        {
        }
    }


    public class MapperInfo
    {
        // 上位机对应的类型
        public Type SwType;

        // 下位机对应的类型
        public Type XwType;
    }
}