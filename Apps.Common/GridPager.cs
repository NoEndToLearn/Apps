namespace Apps.Common
{
    using System;

    public class GridPager
    {
        /// <summary>
        /// Gets or sets 每页行数
        /// </summary>
        public int rows { get; set; }

        /// <summary>
        /// Gets or sets 当前页是第几页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// Gets or sets 排序方式
        /// </summary>
        public string order { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// Gets or sets 总行数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// Gets 总页数
        /// </summary>
        public int totalPages
        {
            get
            {
                return (int)Math.Ceiling((float)this.total / (float)rows);
            }
        }
    }
}
