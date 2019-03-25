using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Text;

namespace NgNet.Data.Json
{
    public class Tools
    {
        #region private fields
        private const string _COLONS = ":";
        private const string _QUOTE = "\"";
        private const string _COMMA = ",";
        private const string _LEFT_BRACE = "{";
        private const string _RIGHT_BRACE = "}";
        private const string _LEFT_BRACKET = "[";
        private const string _RIGHT_BRACKET = "]";
        private const string _ENTER = "\n";

        #endregion

        #region proptected fields

        #endregion

        #region public properties

        #endregion

        #region constructor
        public Tools()
        {

        }
        #endregion

        #region private methods

        #endregion

        #region public methods
        public static string List2Json(IEnumerable<string> list)
        {
            return new JavaScriptSerializer().Serialize(list);
        }

        public static List<string> Json2List(string json)
        {
            return new JavaScriptSerializer().Deserialize<List<string>>(json);
        }

        public static string List2Json<T>(IEnumerable<T> list)
        {
            return new JavaScriptSerializer().Serialize(list);
        }

        public static List<T> Json2List<T>(string json)
        {
            return new JavaScriptSerializer().Deserialize<List<T>>(json);
        }
        #endregion
    }
}
