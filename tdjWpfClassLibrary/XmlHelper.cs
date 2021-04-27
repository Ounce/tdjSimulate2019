using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using tdjWpfClassLibrary.Wagon;

namespace tdjWpfClassLibrary
{
    public static class XmlHelper
    {
        public static void WriteXML(string fileName, object model)
        {
            //Create our own namespaces for the output
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //Add an empty namespace and empty value
            ns.Add("", "");
            
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(model.GetType());

            //var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(fileName);

            //writer.Serialize(file, model, ns);
            writer.Serialize(file, model, ns);
            file.Close();
        }

        public static object ReadXML(string fileName, Type model)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(model);
            //System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            XmlReader file = new XmlTextReader(fileName) { Namespaces = false };
            return reader.Deserialize(file);
            //file.Close();
        }
    }
}
