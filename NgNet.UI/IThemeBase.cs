using System.Drawing;
namespace NgNet.UI
{
    public interface IThemeBase
    {
        Color BackColor { get; set; }
        Color ForeColor { get; set; }
        Color BorderColor { get; set; }
        void SetTheme(IThemeBase t);
    }
}
