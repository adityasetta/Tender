namespace Tender.Infrastructure.Repositories
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Helper for query serialization
    /// </summary>
    public class QueryHelper
    {
        /// <summary>
        /// XMLs the serializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataToSerialize">The data to serialize.</param>
        /// <returns></returns>
        public static string XmlSerializer<T>(T dataToSerialize)
        {
            try
            {
                // removes version
                XmlWriterSettings settings = new();
                settings.OmitXmlDeclaration = true;

                XmlSerializer xsSubmit = new(typeof(T));
                StringWriter sw = new();
                using (XmlWriter writer = XmlWriter.Create(sw, settings))
                {
                    // removes namespace
                    var xmlns = new XmlSerializerNamespaces();
                    xmlns.Add(string.Empty, string.Empty);

                    xsSubmit.Serialize(writer, dataToSerialize, xmlns);
                    return sw.ToString(); // Your XML
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// XMLs the deserializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlText">The XML text.</param>
        /// <returns></returns>
        public static T XMLDeserializer<T>(string xmlText)
        {
            try
            {
                var stringReader = new StringReader(xmlText);
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
            catch
            {
                throw;
            }
        }
    }
}
