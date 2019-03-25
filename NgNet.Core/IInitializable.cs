using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet
{
    public interface IInitializable
    {
        /// <summary>
        /// 获取一个值该值指示是否已经初始化完毕
        /// </summary>
        bool IsInitialized { get; }

        void Init(object obj = null);
    }
}
