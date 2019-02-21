${
    using Typewriter.Extensions.Types;    
    Template(Settings settings)
    {
       settings.OutputFilenameFactory = (file)=> {
                        var f = file.Name.Replace(".cs", ".ts");
                        f = $"{f[0].ToString().ToLower()}{f.Substring(1)}";
                        return f;
        };
       settings.IncludeCurrentProject().IncludeProject("Angular.Core.DAL");        
    } 

	Type CalculatedType(Type t)
    {
        var type = t;
        while (!type.IsEnumerable && type.IsGeneric) {
            type = type.Unwrap();
        }
        return type;
    }

    Class GetBaseClassIfExists(Class c) {return c.BaseClass ?? null;}  
     
    Type[] CalculatedModelTypes(Class c)
    {
        // get base class name if it exists
        string baseClassName = c.BaseClass != null ? c.BaseClass.Name : "";
        var allTypes = c.Properties
            .Select(m => CalculatedType(m.Type))            
            .Where(t => t != null && t.Name != baseClassName && (t.IsDefined || (t.IsEnumerable && !t.IsPrimitive))) // avoid importing base class (it will be imported with calculated base)
            .ToLookup(t => t.ClassName(), t => t);

        return allTypes                    
                    .ToDictionary(l => l.Key, l => l.First())
                    .Select(kvp => kvp.Value)
                    .Where(kvp => kvp.Name != "T[]" && kvp.Name != c.Name && kvp.Name != c.Name + "[]") // prevention of importing generic
                    .ToArray();
    } 
    string CalculatedTypeName(Type t)
    {
        var type = CalculatedType(t);
        return type != null ? type.Name : "void";
    }

    string TypeMap(Property property)
    {
        var type = property.Type;

        if (type.IsPrimitive)
        {
            return type.IsDate ?
                $"new Date(data.{UpperCasePropertyName(property)})" :
                $"data.{UpperCasePropertyName(property)}";
        }
        else
        {
            return type.IsEnumerable ?
                $"data.{UpperCasePropertyName(property)}.map(i => new {type.Name.TrimEnd('[', ']')}(i))" :
                $"new {type.Name}(data.{UpperCasePropertyName(property)})";
        }
    }

    string UpperCasePropertyName(Property property)
    {
       return property.Name[0].ToString().ToUpperInvariant() + property.Name.Substring(1);
    }
    
    string LowerCasePropertyName(Property property)
    {
       return property.Name[0].ToString().ToLowerInvariant() + property.Name.Substring(1);
    }

    string GetPropertiesAsArguments(Class c)
    {
        var result = "";
        for(int i = 0; i< c.Properties.Count; i++)
        {
            var separator = (i == c.Properties.Count -1) ? "" : ", ";
            var prop = c.Properties[i];
            result += $"{LowerCasePropertyName(prop)} : {prop.Type} = null{separator} ";
        }
        return result;
    }

    string LowerCaseTypeName (Type t){
       var name =  t.Name[0].ToString().ToLower() + t.Name.Substring(1);
       return CalculateName(name);
    }

   string LowerCaseClassName (Class t){
       return  t.Name[0].ToString().ToLower() + t.Name.Substring(1);
    }

    string CalculateName(string name){

        string result = name;
        int index = result.IndexOf('<');
        result = index == -1 ? result : result.Substring(0, index);
        index = result.IndexOf('[');
        result = index == -1 ? result : result.Substring(0, index);
        return result;
    }

}$Classes(f=> (f.Namespace.EndsWith("Models.Dto") || f.Namespace.EndsWith("Models.BindingModel")) && !f.Attributes.Select(a=> a.Name.ToLower()).Contains("typewriterignore"))[$CalculatedModelTypes[import {$ClassName} from './$LowerCaseTypeName';
]$GetBaseClassIfExists[import {I$Name, $Name} from './$LowerCaseClassName';
]
//----------------------------------------------
//  DTO interface and model
//----------------------------------------------

export interface I$Name$TypeParameters $GetBaseClassIfExists[extends I]$GetBaseClassIfExists {$Properties[
    $LowerCasePropertyName: $Type;]
}

export class $Name$TypeParameters $GetBaseClassIfExists[extends ]$GetBaseClassIfExists implements I$Name$TypeParameters{ $Properties[
    public $LowerCasePropertyName: $Type;]

    constructor($GetPropertiesAsArguments) {
        $GetBaseClassIfExists[super();]$Properties[
        this.$LowerCasePropertyName = $LowerCasePropertyName;]
    }
}]

$Enums(Angular.Core.Models.*)[export enum $Name { 
          $Values[
          $Name = $Value][,]
}]

