using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{

    public abstract class FilterFileDialog : FileDialog
    {
        public virtual string Filter
        {
            get
            {
                return (_DialogWindow as IFilterFileDialogWindow).Filter;
            }
            set
            {
                (_DialogWindow as IFilterFileDialogWindow).Filter = value;
            }
        }

        public virtual uint FilterIndex
        {
            get
            {
                return (_DialogWindow as IFilterFileDialogWindow).FilterIndex;
            }
            set
            {
                (_DialogWindow as IFilterFileDialogWindow).FilterIndex = value;
            }
        }

        public abstract IEnumerable<string> Show2(IWin32Window f);
    }
}
