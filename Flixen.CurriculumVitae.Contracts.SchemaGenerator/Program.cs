// See https://aka.ms/new-console-template for more information

using Flixen.CurriculumVitae.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
using NJsonSchema.Generation;

if (args.Length < 1)
{
    Console.WriteLine("please specify path");
    return;
}

var settings = new JsonSchemaGeneratorSettings
{
    SerializerSettings = new JsonSerializerSettings
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        }
    }
};
var schema = JsonSchema.FromType<ResumeOptions>(settings);
var schemaData = schema.ToJson();
File.WriteAllTextAsync(args[0],schemaData);
