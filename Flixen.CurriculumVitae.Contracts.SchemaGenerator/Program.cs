// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Flixen.CurriculumVitae.Contracts;
using NJsonSchema;
using NJsonSchema.Generation;

if (args.Length < 1)
{
    Console.WriteLine("please specify path");
    return;
}

var settings = new SystemTextJsonSchemaGeneratorSettings()
{
    SerializerOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    }

};
// var settings = new JsonSchemaGeneratorSettings
// {
//     SerializerSettings = new JsonSerializerSettings
//     {
//         ContractResolver = new DefaultContractResolver
//         {
//             NamingStrategy = new CamelCaseNamingStrategy()
//         }
//     }
// };
var schema = JsonSchema.FromType<ResumeModel>(settings);
var schemaData = schema.ToJson();
File.WriteAllTextAsync(args[0],schemaData);
