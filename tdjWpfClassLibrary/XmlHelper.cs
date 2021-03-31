using System;
using System.Collections.Generic;
using System.Text;

namespace tdjWpfClassLibrary
{
    public static class XmlHelper
    {
        public static void WriteXML(string fileName, object model)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(model.GetType());

            //var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(fileName);

            writer.Serialize(file, model);
            file.Close();
        }

        public static object ReadXML(string fileName, Type model)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(model);
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            return reader.Deserialize(file);
            //file.Close();
        }
    }
}
