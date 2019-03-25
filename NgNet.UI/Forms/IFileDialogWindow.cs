using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI.Forms
{
    public interface IFileDialogWindow : ICommDialogWindow
    {
        string EnterPath { get; set; }

        string InputPath { get; }
    }
}
