using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaiYan.Core.Page
{
    public interface ILoadingItem<T>
    {
        Task<IList<T>> GetNextItemsAsync();
        bool HasMore();
        void Reset();
    }
}
