namespace Mardev.Arq.Shared.Contracts
{
    public class PaginationResponse<T>
    {
        ///<summary>Array of collection items</summary>
        public IEnumerable<T>? Items { get; set; }

        ///<summary>Total number of records</summary>
        /// <example>0</example>
        public long TotalCount { get; set; }

        ///<summary>The (zero-based) offset of the first record in the collection to return</summary>
        /// <example>0</example>
        public int Offset { get; set; }

        ///<summary>The maximum number of entries to return.</summary>
        /// <example>50</example>
        public int Limit { get; set; } = 50;
    }
}
