using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI.Forms
{
    public interface IFilterFileDialogWindow : IFileDialogWindow
    {
        string Filter { get; set; }

        uint FilterIndex { get; set; }
    }
}
