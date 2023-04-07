using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntelligentScrewing.tools
{
    class dataMonitor
    {
         public delegate void ChangedEventHandler();//定义委托
            public event ChangedEventHandler Changed;//定义事件
            private int _nowCount;
            public dataMonitor() { }
            protected virtual void OnChanged()
            {
                if (Changed != null)
                {
                    Changed();
                }
            }
            public int nowData
            { get { return _nowCount; }
                set
                {
                    if(_nowCount!=value)
                    { _nowCount = value;
                    OnChanged();
                    }
                }
            }
    }
}
