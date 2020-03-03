using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace activity_api.Helper
{
    public class PolymorphismSchemaFilter<T> : ISchemaFilter
    {
        private readonly Lazy<HashSet<Type>> derivedTypes = new Lazy<HashSet<Type>>(Init);

        private static HashSet<Type> Init()
        {
            var abstractType = typeof(T);
            var dTypes = abstractType.Assembly
                                     .GetTypes()
                                     .Where(x => abstractType != x && abstractType.IsAssignableFrom(x));

            var result = new HashSet<Type>();

            foreach (var item in dTypes)
                result.Add(item);

            return result;
        }

        public void Apply(Schema schema, SchemaFilterContext context)
        {
            if (!derivedTypes.Value.Contains(context.SystemType)) return;

            var clonedSchema = new Schema
            {
                Properties = schema.Properties,
                Type = schema.Type,
                Required = schema.Required
            };

            //schemaRegistry.Definitions[typeof(T).Name]; does not work correctly in SwashBuckle
            var parentSchema = new Schema { Ref = "#/definitions/" + typeof(T).Name };

            schema.AllOf = new List<Schema> { parentSchema, clonedSchema };

            //reset properties for they are included in allOf, should be null but code does not handle it
            schema.Properties = new Dictionary<string, Schema>();
            
        }
    }


}
