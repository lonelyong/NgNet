using System.Drawing;

namespace NgNet.Drawing
{
    public class PictureHelper
    {
        public static Icon Image2Icon(Image image)
        {
            System.IO.MemoryStream mStream = new System.IO.MemoryStream();///创建内存流
            image.Save(mStream, System.Drawing.Imaging.ImageFormat.Icon);
            Icon icon = Icon.FromHandle(new Bitmap(mStream).GetHicon());
            mStream.Close();
            return icon;
        }
    }
}
