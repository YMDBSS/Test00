using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Test00
{
    class Program
    {
        static void Main(string[] args)
        {


            IDictionary<string, string> schemaMapping = new Dictionary<string, string>();

            schemaMapping.Add("xsd/REMITTable1_V2.xsd", "http://www.acer.europa.eu/REMIT/REMITTable1_V2.xsd");
            schemaMapping.Add("xsd/REMITTable2_V1.xsd", "http://www.acer.europa.eu/REMIT/REMITTable2_V1.xsd");

            ValidateFileAgainstSchema("xml/RemitTable1.xml", schemaMapping);

            Console.WriteLine("==============End of XML file validation===============");


            ValidateFileAgainstSchema("xml/RemitTable2.xml", schemaMapping);



            Console.ReadLine();


        }



        private static void ValidateFileAgainstSchema(string pathToFile, IDictionary<string, string> schemaMapping)
        {

            foreach (var mappingItem in schemaMapping)
            {


                Console.WriteLine($"Started validation {pathToFile} against {mappingItem.Key}");

                try
                {
                    using (var reader = XmlReader.Create(pathToFile))
                    {
                        XmlSchemaSet xsdSchema = new XmlSchemaSet();
                        xsdSchema.Add(mappingItem.Value, mappingItem.Key);
                        XDocument doc = XDocument.Load(reader);

                        doc.Validate(xsdSchema, ValidationCompleted);



                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }

                Console.WriteLine($"Finished validation {pathToFile} against {mappingItem.Key}");

            }


        }

        private static void ValidationCompleted(object sender, ValidationEventArgs e)
        {

            if (e.Exception != null)
                Console.WriteLine($"Validation status {e.Severity}, exception - { e.Exception}");
            else
                Console.WriteLine($"Validation status {e.Severity}, info message - {e.Message}");
        }


    }
}