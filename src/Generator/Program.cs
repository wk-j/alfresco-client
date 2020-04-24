using System;
using System.IO;
using System.Threading.Tasks;
using NSwag;
using NSwag.CodeGeneration.CSharp;

namespace Generator {
    class Program {
        static async Task Generate(string source, string target, string name, bool repeat = false) {
            var document = await OpenApiYamlDocument.FromUrlAsync(source);

            var settings = new CSharpClientGeneratorSettings {
                ClassName = "{controller}Client",
                ExposeJsonSerializerSettings = true,
                CSharpGeneratorSettings = {
                    Namespace = name
                }
            };

            var generator = new CSharpClientGenerator(document, settings);
            var code = generator.GenerateFile();
            File.WriteAllText(target, code);
        }

        static async Task Main(string[] args) {
            await Generate(
                "https://raw.githubusercontent.com/Alfresco/rest-api-explorer/master/src/main/webapp/definitions/alfresco-core.yaml",
                "src/AlfrescoClient/AlfrescoCore.cs",
                "AlfrescoClient.AlfrescoCore");

            await Generate(
                "https://raw.githubusercontent.com/Alfresco/rest-api-explorer/master/src/main/webapp/definitions/alfresco-auth.yaml",
                "src/AlfrescoClient/AlfrescoAuth.cs",
                "AlfrescoClient.AlfrescoAuth",
                true);

            await Generate(
                "https://raw.githubusercontent.com/Alfresco/rest-api-explorer/master/src/main/webapp/definitions/alfresco-search.yaml",
                "src/AlfrescoClient/AlfrescoSearch.cs",
                "AlfrescoClient.AlfrscoSearch",
                true);
        }
    }

}
