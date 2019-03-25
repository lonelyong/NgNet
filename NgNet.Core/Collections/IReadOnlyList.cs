

using System;
using System.Collections;
using System.Collections.Generic;

namespace NgNet.Collections
{
    public interface IReadOnlyList<T> : System.Collections.Generic.IReadOnlyList<T>
    {
        int IndexOf(T item);
        int IndexOf(T item, int index);
        int IndexOf(T item, int index, int count);

        int LastIndexOf(T item);
        int LastIndexOf(T item, int index);
        int LastIndexOf(T item, int index, int count);

        bool Contains(T item);
    }
}
