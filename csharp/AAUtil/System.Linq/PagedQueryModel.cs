namespace System.Linq
{
    /// <summary>
    /// 分页查询模型对象
    /// </summary>
    public class PagedQueryModel
    {
        /// <summary>
        /// 默认的分页显示数
        /// </summary>
        public const int DefaultPageSize = 20;

        /// <summary>
        /// 单页显示行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 分页索引，从1开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 排序表达式(asc可忽略,例:field1 desc,field2,field3 desc)
        /// </summary>
        public string SortExpression { get; set; }

        /// <summary>
        /// 获取总数,默认为true
        /// </summary>
        public bool GetTotal { get; set; } = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedQueryModel()
        {
            PageIndex = 1;
            PageSize = DefaultPageSize;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">分页索引，从1开始</param>
        /// <param name="pageSize">单页显示行数</param>
        public PagedQueryModel(int pageIndex, int pageSize = DefaultPageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }

    /// <summary>
    /// 分页查询模型对象
    /// </summary>
    public class PagedQueryModel<T> : PagedQueryModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PagedQueryModel()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="param">查询参数</param>
        public PagedQueryModel(T param)
        {
            Para = param;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">分页索引，从1开始</param>
        /// <param name="pageSize">分页显示数</param>
        /// <param name="param">查询参数</param>
        public PagedQueryModel(int pageIndex, int pageSize = DefaultPageSize, T param = default(T))
            : base(pageIndex, pageSize)
        {
            Para = param;
        }

        /// <summary>
        /// 查询参数
        /// </summary>
        public T Para { get; set; }
    }
}
