using System.Linq;
using System.Collections.Generic;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Model.DataBaseTable;
using DRsoft.Runtime.Core.DataBase.Common;
using DRsoft.Runtime.Core.DataBase.Common.Extentions;

namespace DRsoft.Engine.Core.Engine
{
    /// <summary>
    ///读写硅胶膜缺陷表:DirtyTable
    /// </summary>
    public abstract partial class AbstractEngine : IEngine
    {
        /// <summary>
        /// 向DirtyTable表中写入数据
        /// </summary>
        /// <param name="dirtyTable"></param>
        public void InsertDirtyTable(DirtyTable dirty)
        {
            DataBase.Create<DirtyTable>(dirty);
        }
        /// <summary>
        /// 向DirtyTable表中写入集合数据
        /// </summary>
        /// <param name="dirtyTable"></param>
        public void InsertListDirtyTable(List<DirtyTable> lstDirty)
        {
            DataBase.Create<DirtyTable>(lstDirty);
        }
        /// <summary>
        /// 根据集合删除DirtyTable表中的数据
        /// </summary>
        /// <param name="lstDirty"></param>
        public void DeleteDirtyTable(List<DirtyTable> lstDirty)
        {
            DataBase.Delete<DirtyTable>(lstDirty);
        }

        /// <summary>
        /// 根据组件ID查询脏污数据
        /// </summary>     
        /// <param name="groupId"></param>
        /// <returns></returns>
        public List<DirtyTable> SelectDirtyTableData(string groupId)
        {
            List<DirtyTable> lstDirty = new List<DirtyTable>();
            var where = LinqHelper.True<DirtyTable>();
            where = where.And(x => x.GroupId == groupId);
            var dt = DataBase.InvokeList<DirtyTable>("dt", where);
            lstDirty = dt.Data.ToArray<DirtyTable>().ToList();                   
            return lstDirty;
        }
        /// <summary>
        /// 根据硅胶膜ID和机台ID查询脏污数据
        /// </summary>
        /// <param name="silicaId"></param>
        /// <param name="machineId"></param>
        /// <returns></returns>
        public List<DirtyTable> SelectDirtyTableData(string silicaId, string machineId)
        {
            List<DirtyTable> lstDirty = new List<DirtyTable>();
            var where = LinqHelper.True<DirtyTable>();
            where = where.And(x => x.SilicaId == silicaId && x.MachineId == machineId);
            var dt = DataBase.InvokeList<DirtyTable>("dt", where);
            lstDirty = dt.Data.ToArray<DirtyTable>().ToList();
            return lstDirty;
        }
    }
}