using System.Drawing;
using System;
using System.ComponentModel;

namespace NgNet.UI.Forms
{
    public interface ICommDialogWindow : ITheme, System.Windows.Forms.IWin32Window, IComponent, IDisposable
    {
        Icon Icon { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        System.Windows.Forms.DialogResult DialogResult { get; set; }

        bool IsLoaded { get; }

        Size Size { get; set; }

        string Text { get; set; }

        void Activate();

        void Show();

        void Show(System.Windows.Forms.IWin32Window owner);

        System.Windows.Forms.DialogResult ShowDialog();

        System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.IWin32Window owner);

        bool Focus();

        void Close();
    }
}
