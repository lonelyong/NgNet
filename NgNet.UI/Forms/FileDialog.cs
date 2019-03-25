using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI.Forms
{

    public abstract class FileDialog : CommDialog<string>
    {
        public virtual string Enterpath
        {
            set
            {
                (_DialogWindow as IFileDialogWindow).EnterPath = value;
            }
        }

        public virtual string InputPath
        {
            get
            {
                return DialogResult == System.Windows.Forms.DialogResult.OK ? (_DialogWindow as IFileDialogWindow).InputPath : null;
            }
        }

        public virtual Size Size
        {
            get
            {
                return _DialogWindow.Size;
            }
            set
            {
                _DialogWindow.Size = value;
            }
        }
    }
}
