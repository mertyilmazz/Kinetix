using Kinetix.Business.Abstract;
using System.IO;
using System.Xml.Serialization;

namespace Kinetix.Business.Concrete
{
    public class XmlManager<T> : IXmlManager<T>
    {
        private readonly XmlSerializer _xmlSerializer;
              
        public XmlManager()
        {
            _xmlSerializer = new XmlSerializer(typeof(T));
        }
        public string Serialize(T obj)
        {
            using (StringWriter textWriter = new StringWriter())
            {
                _xmlSerializer.Serialize(textWriter, obj);
                return textWriter.ToString();
            }


        }
    }
}
