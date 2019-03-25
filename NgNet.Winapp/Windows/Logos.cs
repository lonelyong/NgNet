using System.Drawing;

namespace NgNet.Windows
{
    public class Logos
    {
        #region private fields

        #endregion

        #region proptected fields

        #endregion

        #region public properties
        public static Bitmap Windows
        {
            get
            {

                return Properties.Resources.winLogo;
            }
        }

        public static Bitmap Application
        {
            get
            {
                return Properties.Resources.application;
            }
        }

        public static Bitmap Computer
        {
            get
            {
                return Properties.Resources.computer;
            }
        }
        #endregion

        #region constructor
        public Logos()
        {

        }
        #endregion

        #region private methods

        #endregion

        #region public methods

        #endregion
    }
}
