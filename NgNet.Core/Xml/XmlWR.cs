using System;
using System.Collections.Generic;
using System.Xml;

namespace NgNet.Xml
{
    /// <summary>
    /// XML文档读写
    /// </summary>
    public class XmlWR : IDisposable
    {
        #region 内部变量

        //文档
        System.Xml.XmlDocument m_xmlDoc = null;

        //文档路径
        string m_sXmlPath = "";

        #endregion

        #region 类初始化

        #region 属性

        /// <summary>
        /// 判断节点是否符合条件的委托
        /// </summary>
        /// <param name="xn"></param>
        /// <returns></returns>
        public delegate bool delegateCheckNode(XmlNode xn);

        /// <summary>
        /// XML文档
        /// </summary>
        public System.Xml.XmlDocument XmlDoc
        {
            get
            {
                if (m_xmlDoc == null)
                {
                    if (string.IsNullOrEmpty(m_sXmlPath))
                        throw new Exception("请先设置XML文档路径！");
                    m_xmlDoc = new System.Xml.XmlDocument();
                    m_xmlDoc.Load(m_sXmlPath);//加载文档
                }
                return m_xmlDoc;
            }
            set
            {
                m_xmlDoc = value;
            }
        }

        /// <summary>
        /// 文档路径
        /// </summary>
        public string XmlPath
        {
            get { return m_sXmlPath; }
            set
            {
                m_sXmlPath = value;
                m_xmlDoc = null;//重置文档
            }
        }

        #endregion
        #region 构造函数

        /// <summary>
        /// XML文档读取器
        /// </summary>
        public XmlWR()
        { }

        /// <summary>
        /// XML文档读取器
        /// </summary>
        /// <param name="Xml_Path">XML文档路径</param>
        public XmlWR(string Xml_Path)
        {
            m_sXmlPath = Xml_Path;
        }

        #endregion
        #endregion

        #region 函数

        #region 内部函数


        /// <summary>
        /// 关闭
        /// </summary>
        private void Close()
        {
            if (XmlDoc != null)
            {
                XmlDoc.Save(XmlPath);
                XmlDoc = null;
            }
        }


        /// <summary>
        /// 遍历所有节点
        /// </summary>
        /// <param name="xnList"></param>
        /// <param name="delcn"></param>
        protected void checkAllNode(List<XmlNode> xnList, delegateCheckNode delcn, XmlNode xn)
        {
            //递归
            if (xn.HasChildNodes)
            {
                foreach (XmlNode cxn in xn.ChildNodes)
                {
                    if (delcn != null && delcn(cxn)) //委托不为空且条件符合
                        xnList.Add(cxn);

                    checkAllNode(xnList, delcn, cxn);//如果有子节点则遍历子节点
                }
            }
        }

        /// <summary>
        /// 获取节点通过委托
        /// </summary>
        /// <param name="delcn"></param>
        /// <param name="xn"></param>
        /// <returns></returns>
        protected XmlNode getXmlNodeByDelegate(delegateCheckNode delcn, XmlNode xn)
        {
            if (delcn != null && delcn(xn)) //委托不为空且条件符合
                return xn;

            //递归
            if (xn.HasChildNodes)
            {
                foreach (XmlNode cxn in xn.ChildNodes)
                {
                    XmlNode ccxn = getXmlNodeByDelegate(delcn, cxn);//如果有子节点则遍历子节点
                    if (ccxn != null) return ccxn;
                }
            }

            return null;
        }
        #endregion

        #region 公共函数

        public void LoadXmlString(string xmlString)
        {
            m_xmlDoc = new System.Xml.XmlDocument();
            m_xmlDoc.LoadXml(xmlString);
        }
        /// <summary>
        /// 通过委托获取单个节点
        /// </summary>
        /// <param name="delcn">获取节点的委托</param>
        /// <returns></returns>
        public XmlNode GetNodeByDelegate(delegateCheckNode delcn)
        {
            //循环所有顶级节点
            foreach (XmlNode xn in XmlDoc.ChildNodes)
            {
                if (delcn != null && delcn(xn)) //委托不为空且条件符合
                    return xn;

                if (xn.HasChildNodes)
                {
                    XmlNode cxn = getXmlNodeByDelegate(delcn, xn);//如果有子节点则遍历子节点
                    if (cxn != null) return cxn;
                }
            }
            return null;
        }

        /// <summary>
        /// 查找所有符合条件的节点
        /// </summary>
        /// <param name="delcn">条件委托</param>
        /// <returns></returns>
        public List<XmlNode> getNodeListByDelegate(delegateCheckNode delcn)
        {
            List<XmlNode> xnList = new List<XmlNode>();

            //循环所有顶级节点
            foreach (XmlNode xn in XmlDoc.ChildNodes)
            {
                if (delcn != null && delcn(xn)) //委托不为空且条件符合
                    xnList.Add(xn);
                if (xn.HasChildNodes)
                {
                    checkAllNode(xnList, delcn, xn);//如果有子节点则遍历子节点
                }
            }

            return xnList;

        }


        /// <summary>
        /// 通过路径获取节点
        /// </summary>
        /// <param name="nodePath">节点路径（如：application/appsetting）</param>
        /// <returns></returns>
        public XmlNode GetNodeByPath(string nodePath)
        {
            if (string.IsNullOrEmpty(nodePath)) throw new Exception("节点的路径不可为空！");

            //通过路径查得节点
            XmlNode xn = XmlDoc.SelectSingleNode(nodePath);

            return xn;
        }

        /// <summary>
        /// 通过节点名称读取其值
        /// </summary>
        /// <param name="nodePath">节点路径（如：application/appsetting）</param>
        /// <returns></returns>
        public string ReadValueByPath(string nodePath)
        {
            if (string.IsNullOrEmpty(nodePath)) throw new Exception("节点的路径不可为空！");

            XmlNode xn = GetNodeByPath(nodePath);//通过路径获取节点

            if (xn != null) return xn.Value;

            return string.Empty;//返回空值
        }

        /// <summary>
        /// 通过属读取值
        /// </summary>
        /// <param name="AttributeName">属性名</param>
        /// <param name="AttributeValue">属性值</param>
        /// <returns></returns>
        public string ReadValueByAttribute(string AttributeName, string AttributeValue)
        {

            XmlNode xn = GetNodeByAttribute(AttributeName, AttributeValue);//通过属性获取节点

            if (xn != null) //存在此节点
                return xn.Value;

            return string.Empty;//返回空值
        }

        /// <summary>
        /// 通过属性获取节点
        /// </summary>
        /// <param name="AttributeName">属性名</param>
        /// <param name="AttributeValue">属性值</param>
        /// <returns></returns>
        public XmlNode GetNodeByAttribute(string AttributeName, string AttributeValue)
        {
            if (string.IsNullOrEmpty(AttributeName)) throw new Exception("节点的属性名不可为空！");

            //新建委托
            delegateCheckNode delcn = new delegateCheckNode(delegate (XmlNode xn)
            {
                return (xn.Attributes != null && xn.Attributes[AttributeName] != null && xn.Attributes[AttributeName].Value == AttributeValue);
            });

            return GetNodeByDelegate(delcn);//通过委托获取节点
        }

        /// <summary>
        /// 获取所有属性符合条件的节点集合
        /// </summary>
        /// <param name="AttributeName">属性名</param>
        /// <param name="AttributeValue">属性值</param>
        /// <returns></returns>
        public List<XmlNode> GetNodeListByAttribute(string AttributeName, string AttributeValue)
        {
            if (string.IsNullOrEmpty(AttributeName)) throw new Exception("节点的属性名不可为空！");

            //新建委托
            delegateCheckNode delcn = new delegateCheckNode(delegate (XmlNode xn)
            {
                return (xn.Attributes != null && xn.Attributes[AttributeName] != null && xn.Attributes[AttributeName].Value == AttributeValue);
            });

            return getNodeListByDelegate(delcn);//返回所有符合条件的节点
        }

        /// <summary>
        /// 通过节点类型获取所有节点集合
        /// </summary>
        /// <param name="xnType">节点类型</param>
        /// <returns></returns>
        public List<XmlNode> GetNodeListByNodeType(XmlNodeType xnType)
        {
            //新建委托
            delegateCheckNode delcn = new delegateCheckNode(delegate (XmlNode xn)
            {
                return xn.NodeType == xnType;
            });

            return getNodeListByDelegate(delcn);//返回所有符合条件的节点
        }
        /// <summary>
        /// 将XML全加载到集合中
        /// </summary>
        /// <param name="bToLower"></param>
        /// <returns></returns>
        public Dictionary<string, string> TurnXmlToDictionary(bool bToLower)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (XmlNode xn in XmlDoc.ChildNodes)
            {
                initChildPath(xn, "", dic, bToLower);
            }
            return dic;
        }



        /// <summary>
        /// 将XML全加载到集合中
        /// </summary>
        /// <param name="bToLower"></param>
        /// <returns></returns>
        public Dictionary<string, string> TurnXmlToDictionary(XmlNode RootXn, bool bToLower)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            initChildPath(RootXn, "", dic, bToLower);

            foreach (XmlNode xn in RootXn.ChildNodes)
            {
                initChildPath(xn, (bToLower ? RootXn.Name.ToLower() : RootXn.Name), dic, bToLower);
            }

            return dic;
        }

        private void initChildPath(XmlNode xn, string parentPath, Dictionary<string, string> _xmlDictionary, bool bToLower)
        {
            parentPath += (string.IsNullOrEmpty(parentPath) ? "" : "/") + (bToLower ? xn.Name.ToLower() : xn.Name);

            if (xn.Attributes != null && xn.Attributes.Count > 0)
            {
                foreach (XmlAttribute xa in xn.Attributes)
                {
                    string xapath = parentPath + "/" + (bToLower ? xa.Name.ToLower() : xa.Name);
                    if (!_xmlDictionary.ContainsKey(xapath))
                    {
                        _xmlDictionary.Add(xapath, xa.Value);
                    }
                }
            }

            if (xn.NodeType == XmlNodeType.Element && !string.IsNullOrEmpty(xn.InnerText))
            {
                string xtpath = parentPath + "/" + (bToLower ? "innertext" : "InnerText");
                _xmlDictionary.Add(xtpath, xn.InnerText);
            }

            if (xn.HasChildNodes)
            {
                foreach (XmlNode cxn in xn.ChildNodes)
                {
                    initChildPath(cxn, parentPath, _xmlDictionary, bToLower);
                }
            }
        }

        /// <summary>
        /// 获取父节点下子节点值集合
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        public static List<string> GetChildrenKeys(Dictionary<string, string> _xmlDictionary, string parentKey, string onlyAttribute, bool bToLower)
        {
            if (bToLower)
            {
                onlyAttribute = onlyAttribute.ToLower().Trim();
                parentKey = parentKey.ToLower();
            }

            List<string> children = new List<string>();
            foreach (string key in _xmlDictionary.Keys)
            {
                if ((bToLower && key.ToLower().StartsWith(parentKey) && key.ToLower().EndsWith("/" + onlyAttribute)) || (!bToLower && key.StartsWith(parentKey) && key.EndsWith("/" + onlyAttribute)))
                {
                    string midpath = bToLower ? key.ToLower().Substring(parentKey.Length) : key.Substring(parentKey.Length);
                    int lastat = bToLower ? midpath.ToLower().LastIndexOf("/" + onlyAttribute.ToLower()) : midpath.LastIndexOf("/" + onlyAttribute);
                    int firstsplit = midpath.IndexOf("/", midpath.IndexOf("/") + 1);
                    //检　查是否是父节点的一级子节点，
                    if (lastat != -1 && firstsplit == lastat)
                    {
                        children.Add(parentKey + midpath.Substring(0, lastat));
                    }
                }

            }

            return children;
        }
        /// <summary>
        /// 保存文档
        /// </summary>
        public void Save()
        {
            XmlDoc.Save(XmlPath);
        }
        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="fileName">要保存的路径</param>
        public void Save(string fileName)
        {
            XmlDoc.Save(fileName);
        }

        /// <summary>
        /// 生成新的文档
        /// </summary>
        public void CreateNewXML()
        {
            XmlDoc = new System.Xml.XmlDocument();

            //生成根节点
            XmlNode xn = XmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "xml", "");
            xn.Value = "version=\"1.0\" encoding=\"utf-8\"";

            XmlDoc.AppendChild(xn);
        }

        /// <summary>
        /// 生成新的文档
        /// </summary>
        /// <param name="DeclarationValue">XML声明（如：version="1.0" encoding="utf-8"）</param>
        public void CreateNewXML(string DeclarationValue)
        {
            XmlDoc = new System.Xml.XmlDocument();

            //生成根节点
            XmlNode xn = XmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "xml", "");
            xn.Value = DeclarationValue;

            XmlDoc.AppendChild(xn);
        }

        /// <summary>
        /// 生成新节点(此方法生成的节点未保存到文档中)
        /// </summary>
        /// <param name="xnType">节点类型</param>
        /// <param name="name">节点名称</param>
        public XmlNode CreateNewNode(XmlNodeType xnType, string name)
        {
            XmlNode xn = XmlDoc.CreateNode(xnType, name, "");
            return xn;
        }

        /// <summary>
        /// 在指定的父节点中生成一个子节点
        /// </summary>
        /// <param name="parentNode">父节点(如果为NULL，则此节点为根节点)</param>
        /// <param name="xnType">子节点类型</param>
        /// <param name="name">子节点名称</param>
        /// <returns>返回生成的节点</returns>
        public XmlNode CreateNewNode(XmlNode parentNode, XmlNodeType xnType, string name)
        {
            if (parentNode != null)
            {
                //生成子节点
                XmlNode xn = XmlDoc.CreateNode(xnType, name, "");

                //将子节点添加到父节点下
                parentNode.AppendChild(xn);

                return xn;
            }
            else
            {
                //生成子节点
                XmlNode xn = XmlDoc.CreateNode(xnType, name, "");

                //添加到根节点
                XmlDoc.AppendChild(xn);

                return xn;
            }
        }

        /// <summary>
        /// 在指定的父节点中生成一个子节点
        /// </summary>
        /// <param name="parentNode">父节点(如果为NULL，则此节点为根节点)</param>
        /// <param name="xnType">子节点类型</param>
        /// <param name="name">子节点名称</param>
        /// <param name="attributes">此节点的属性</param>
        /// <returns></returns>
        public XmlNode CreateNewNode(XmlNode parentNode, XmlNodeType xnType, string name, Dictionary<string, string> attributes)
        {
            //生成节点
            XmlNode xn = CreateNewNode(parentNode, xnType, name);

            WriteNodeAttributes(xn, attributes);//写属性

            return xn;
        }

        /// <summary>
        /// 给节点写入属性
        /// </summary>
        /// <param name="xn">当前节点</param>
        /// <param name="attributeName">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <returns></returns>
        public XmlNode WriteNodeAttribute(XmlNode xn, string attributeName, string attributeValue)
        {
            if (xn.Attributes[attributeName] != null)//如果属性不存在
            {
                xn.Attributes[attributeName].Value = attributeValue;//添加属性
            }
            else
            {
                //如果不存在属性则新增
                XmlAttribute xa = XmlDoc.CreateAttribute(attributeName);
                xa.Value = attributeValue;
                xn.Attributes.Append(xa);
            }
            return xn;
        }

        /// <summary>
        /// 给节点添加属性
        /// </summary>
        /// <param name="xn"></param>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public XmlNode WriteNodeAttributes(XmlNode xn, Dictionary<string, string> attributes)
        {
            //**********************
            //如果属性不为空，添加属性
            //**********************
            if (attributes != null)
            {
                foreach (string key in attributes.Keys)
                {
                    WriteNodeAttribute(xn, key, attributes[key]);
                }
            }

            return xn;
        }
        /// <summary>
        /// 添加节点通过你节点路径
        /// </summary>
        /// <param name="parentPath">父节点路径（如：application/appsetting）</param>
        /// <param name="xnType">子节点类型</param>
        /// <param name="name">子节点名</param>
        /// <returns></returns>
        public XmlNode CreateNewNodeByParentPath(string parentPath, XmlNodeType xnType, string name)
        {
            //通过节点路径获取节点
            XmlNode parentNode = GetNodeByPath(parentPath);

            if (parentNode == null) throw new Exception("获取父节点错误，确保此节点是否在存！");

            XmlNode xn = CreateNewNode(xnType, name);//生成节点

            parentNode.AppendChild(xn);//将节点添加至父节点下

            return xn;
        }



        /// <summary>
        /// 通过节点路径更改值
        /// </summary>
        /// <param name="nodePath">节点路径（如：application/appsetting）</param>
        /// <param name="value">节点的值</param>
        public void SetValueByPath(string nodePath, string value)
        {
            //通过路径获取节点
            XmlNode xn = GetNodeByPath(nodePath);

            if (xn != null)
            {
                //修改节点的值
                xn.Value = value;
            }
            else
            {
                throw new Exception("获取节点失败，请确保节点路径正确！");
            }
        }

        /// <summary>
        /// 通过节点路径设置属性值
        /// </summary>
        /// <param name="AttributeName"></param>
        /// <param name="AttributeValue"></param>
        public void SetAttributeValue(string nodePath, string AttributeName, string AttributeValue)
        {
            //通过路径获取节点
            XmlNode xn = GetNodeByPath(nodePath);

            if (xn != null)
            {
                //修改节点属性的值
                WriteNodeAttribute(xn, AttributeName, AttributeValue);
            }
            else
            {
                throw new Exception("获取节点失败，请确保节点路径正确！");
            }
        }

        #endregion

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            XmlDoc = null;
            GC.Collect();
        }

        #endregion
    }
}
