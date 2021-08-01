using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MyConsole
{
    public class XmlHelp
    {
        private string filePath { get; set; }
        private XmlDocument xmlDoc { get; set; }

        public XmlHelp(string path) {
            filePath = path;
            xmlDoc = new XmlDocument();
            if (File.Exists(path)) {
                xmlDoc.Load(path);
            }
        }

        /// <summary>
        /// 创建单个节点
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="ChildName"></param>
        /// <param name="ChildValue"></param>
        /// <param name="xmlAttributes"></param>
        /// <returns></returns>

        public XmlNode createChildNodeAppendNode([NotNull] XmlNode xmlNode, [NotNull] string ChildName, string ChildValue, List<XmlAtt> xmlAttributes) {
            XmlNode retNode = null;
            try
            {
                XmlNode child = createNode(xmlNode, ChildName, ChildValue, xmlAttributes);
                xmlNode.AppendChild(child);
                xmlDoc.Save(filePath);
                retNode = child;
            }
            catch (Exception ex) {
                retNode = null;
            }
            return retNode;
        }

        /// <summary>
        /// 在节点前插入一个节点
        /// </summary>
        /// <param name="root"></param>
        /// <param name="xmlNode"></param>
        /// <param name="ChildName"></param>
        /// <param name="ChildValue"></param>
        /// <param name="xmlAttributes"></param>
        /// <returns></returns>
        public XmlNode createChildNodeAfter([NotNull] XmlNode xmlNode, [NotNull] string ChildName, string ChildValue, List<XmlAtt> xmlAttributes) {
            XmlNode retNode = null;

            try
            {
                XmlNode child = createNode(xmlNode, ChildName, ChildValue, xmlAttributes);
                if (xmlNode.ParentNode != null) {
                    xmlNode.ParentNode.InsertAfter(child, xmlNode);
                    xmlDoc.Save(filePath);
                    retNode = child;
                }
            }
            catch (Exception ex)
            {
                retNode = null;
            }
            return retNode;
        }

        /// <summary>
        /// 创建一个新节点，在xmlNode节点之前
        /// </summary>
        /// <param name="root"></param>
        /// <param name="xmlNode"></param>
        /// <param name="ChildName"></param>
        /// <param name="ChildValue"></param>
        /// <param name="xmlAttributes"></param>
        /// <returns></returns>
        public XmlNode createChildNodeBefore([NotNull] XmlNode xmlNode, [NotNull] string ChildName, string ChildValue, List<XmlAtt> xmlAttributes)
        {
            XmlNode retNode = null;

            try
            {
                XmlNode child = createNode(xmlNode, ChildName, ChildValue, xmlAttributes);
                if (xmlNode.ParentNode != null)
                {
                    xmlNode.ParentNode.InsertBefore(child, xmlNode);
                    xmlDoc.Save(filePath);
                    retNode = child;
                }
            }
            catch (Exception ex)
            {
                retNode = null;
            }
            return retNode;
        }


        /// <summary>
        /// 根据节点属性，创建新节点
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="ChildName"></param>
        /// <param name="ChildValue"></param>
        /// <param name="xmlAttributes"></param>
        /// <returns></returns>
        private XmlNode createNode([NotNull] XmlNode xmlNode, [NotNull] string ChildName, string ChildValue, List<XmlAtt> xmlAttributes) {
            XmlNode retNode = null;
            try
            {
                //1、创建子节点
                XmlElement child = xmlDoc.CreateElement(ChildName);
                //child.InnerText = ChildValue==null ? ChildValue.ToString() :null;
                child.InnerText = ChildValue;

                if (xmlAttributes != null)
                {
                    foreach (XmlAtt attribute in xmlAttributes)
                    {
                        child.SetAttribute(attribute.Name, attribute.Value);
                    }
                }
                retNode = (XmlNode)child;
            }
            catch (Exception ex) {

            }
            return retNode;
        }

        /// <summary>
        /// 根据节点路劲，获取单一节点
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public XmlNode getSingleNode(string path) {
            return xmlDoc.SelectSingleNode(path);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="attiName"></param>
        /// <param name="attiValue"></param>
        /// <returns></returns>
        public Boolean removeNode(string path, string attiName = null, string attiValue = null) {
            Boolean ret = true;
            try
            {
                //获取同名同级节点集合
                XmlNodeList nodelist = xmlDoc.SelectNodes(path);
                XmlNode xmlNode = null;
                if (nodelist.Count > 1)
                { //超过一个，需要在根据属性去筛选
                    foreach (XmlNode n in nodelist)
                    {
                        foreach (XmlAttribute a in n.Attributes)
                        {
                            if (a.Name == attiName && a.Value == attiValue)
                            {
                                xmlNode = n;
                                goto gotoBreak;
                            }
                        }

                    }
                gotoBreak:
                    XmlNode fatherNode = xmlNode.ParentNode;
                    fatherNode.RemoveChild(xmlNode);
                }
                else
                {
                    xmlNode = nodelist[0];
                    XmlNode fatherNode = xmlNode.ParentNode;
                    fatherNode.RemoveChild(xmlNode);
                }
                xmlDoc.Save(filePath);
            }
            catch (Exception ex) {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 根据节点名称，读取所有这个名称的节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<T> geElementList<T>(string name) where T : new() {
            List<T> list = new List<T>();

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName(name);

            foreach (XmlElement node in nodeList) {
                T t = new T();
                foreach (PropertyInfo info in t.GetType().GetProperties()) {

                }
            }

            return list;
        }

    }


    public class LinqXmlHelp {
        private string filePath { get; set; }

        public LinqXmlHelp(string path) {
            filePath = path;
        }

        /// <summary>
        /// 根据传入的数据，创建xml文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lists"></param>
        public void createXmlByObject<T>(List<T> lists) {

            List<XElement> exes = new List<XElement>();
            lists.ForEach(a =>
            {
                List<XElement> chils = new List<XElement>();
                //获取T中的属性，给每个属性生成一个子节点
                foreach (PropertyInfo info in a.GetType().GetProperties()) {
                    chils.Add(new XElement(info.Name, info.GetValue(a)));
                }
                exes.Add(new XElement(a.GetType().Name, chils));
            });
            XDocument xdoc = new XDocument(
                   new XDeclaration("1.0", "utf-8", null),
                   new XElement("root", exes)
                );
            xdoc.Save(filePath);
        }
    }

    public class XmlAtt { 
       public string Name { get; set; }

        public string Value { get; set; }
    }
}
