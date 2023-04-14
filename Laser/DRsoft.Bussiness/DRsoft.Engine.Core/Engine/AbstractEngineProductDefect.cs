using System.Linq;
using System.Collections.Generic;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Model.DataBaseTable;
using DRsoft.Runtime.Core.DataBase.Common;
using DRsoft.Runtime.Core.DataBase.Common.Extentions;

namespace DRsoft.Engine.Core.Engine
{
    /// <summary>
    ///串焊产品加工后有问题的产品表:ProductDefectTable
    /// </summary>
    public abstract partial class AbstractEngine : IEngine
    {
        /// <summary>
        /// 向ProductDefectTable表中写入数据
        /// </summary>
        /// <param name="ProductDefectTable"></param>
        public void InsertProductDefectTable(ProductDefectTable productDefect)
        {
            DataBase.Create<ProductDefectTable>(productDefect);
        }
        /// <summary>
        /// 根据集合删除ProductDefectTable表中的数据
        /// </summary>
        /// <param name="lstProductDefect"></param>
        public void DeleteProductDefectTable(List<ProductDefectTable> lstProductDefect)
        {
            DataBase.Delete<ProductDefectTable>(lstProductDefect);
        }

        /// <summary>
        /// 根据条件查询缺陷产品数据
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<ProductDefectTable> SelectProductDefectTableData(string commandType,string condition)
        {
            List<ProductDefectTable> lstProductDefect = new List<ProductDefectTable>();
            var where = LinqHelper.True<ProductDefectTable>();
            switch (commandType)
            {
                case "selectByGroupId":
                    where = where.And(x => x.GroupId == condition);
                    var dt = DataBase.InvokeList<ProductDefectTable>("dt", where);
                    lstProductDefect = dt.Data.ToArray<ProductDefectTable>().ToList();
                    break;
                case "selectBySilicaId":
                    where = where.And(x => x.SilicaId == condition);
                    dt = DataBase.InvokeList<ProductDefectTable>("dt", where);
                    lstProductDefect = dt.Data.ToArray<ProductDefectTable>().ToList();
                    break;
                default:
                    break;
            }
            return lstProductDefect;
        }
    }
}