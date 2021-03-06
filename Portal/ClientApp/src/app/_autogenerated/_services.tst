${
    using Typewriter.Extensions.WebApi;
    using Typewriter.Extensions.Types;
 

    Template(Settings settings)
    {
        //settings.IncludeCurrentProject().IncludeProject("WebAPI");        
        //  settings.OutputExtension = ".tsx";
        settings.OutputFilenameFactory = (file)=> {
                        var f = file.Name.Replace("Controller", ".service");
                        f =  f.Replace(".cs", ".ts");
                        f= $"{f[0].ToString().ToLower()}{f.Substring(1)}";
                        return f;
        };
    }
     static string debugInfo = "";
     string PrintDebugInfo(File f){
          return debugInfo;        
     }
    string ReturnType(Method m) => m.Type.Name == "IHttpActionResult" ? "any" : m.Type.Name;
    string ServiceName(Class c){
         return c.Name.Replace("Controller", "Service").Replace("Controller", "Service");
    }
    string ParentServiceName(Method m) => ServiceName((Class)m.Parent);
    Type CalculatedType(Type t)
    {       
        var type = t;
        while (!type.IsEnumerable && type.IsGeneric) {
            type = type.Unwrap();
        }
        return type.Name == "IHttpActionResult" ? null : type;
    }
    Type[] CalculatedModelTypes(Class c)
    {
        var methodResults = c.Methods.Select(m=> m.Type);
        var allTypes = c.Methods
            .Where(m=> !m.Attributes.Select(a=> a.Name.ToLower()).Contains("typewriterignore"))
            .SelectMany(m => m.Parameters.Select(p => p.Type).Concat(new Type[]{m.Type}))            
            .Select(t => CalculatedType(t))
            .Concat(methodResults)
            .Where(t => t != null && (t.IsDefined || (t.IsEnumerable && !t.IsPrimitive)) && !t.Name.Contains("DataSourceLoadOptions"))   // ignore  DataSourceLoadOptions types        

            //.ToLookup(t => t.ClassName(), t => t);
            .ToLookup(t => CalculatedResultTypeName(t), t => t);

        return allTypes                    
                    .ToDictionary(l => l.Key, l => l.First())
                    .Select(kvp => kvp.Value)                    
                    .ToArray();      
    } 
    string CalculatedResultTypeName(Type t)
    {        
        string result = t.Name;
        return CalculateName(result);
    }

    string CalculatedResultTypeNameForImport(Type t)
    {        
        string result = t.Name[0].ToString().ToLower() + t.Name.Substring(1);
        return CalculateName(result);
    }

    string CalculateName(string name){

        string result = name;
        int index = result.IndexOf('<');
        result = index == -1 ? result : result.Substring(0, index);
        index = result.IndexOf('[');
        result = index == -1 ? result : result.Substring(0, index);

        return result;
    }


    string GetMethodDefinitionPrefix (Method method)
    { 
      var definition = $"fetchHelper.{method.HttpMethod().First().ToString().ToUpper()}{method.HttpMethod().Substring(1)}<T>(";
      return definition;
    }    

   string GetMethodDefinitionSuffix(Method method)
    {        
        return $", showSpinner, requestInit";
    }

    string EmitCallbacks(Method method)
    {
        var declaration = " showSpinner = true, requestInit?: RequestInit) : Promise<T>";
        if (method.Parameters.Count > 0) declaration = "," + declaration;
        return declaration;
    }
    
    //  helper to return only non URI parameters
    Parameter[] GetNonUriParameters(Method method)
    {
        var parameters = method.Parameters.Where( x=> ( !x.Attributes.Any() 
                                                        ||  x.Attributes.Any(y=>y.Name!="FromUri"))
                                                ).ToArray();
        return parameters;
    }

    string CommaGenerator(Method method)
    {
        var paremeters = GetNonUriParameters(method).Select(p => p.Name);

        string comma = "";
        if (method.HttpMethod() == "post" || method.HttpMethod() == "put") {
            comma = paremeters.Any() ? ", " : "";
        }
        return $"{comma}";
    }

    string GetFromBodyParameter(Method method)
    {
      //  var parameters = GetNonUriParameters(method).Select(p=> p.Name);
        var parameter = GetNonUriParameters(method).FirstOrDefault(x=>x.Attributes.Any(attr=>attr.Name == "FromBody"))?.Name;
        if(parameter == null){parameter = GetNonUriParameters(method).FirstOrDefault(x=>!x.Type.IsPrimitive)?.Name;}

       // return String.Join(",",parameter);  
       return parameter;
    }

    //----------------------------------------------------------------------------------
    //  builds a typescript method argument list e.g. "name1 : Type, ..., nameN : Type"
    //  - Complex types are emitted as interfaces         -> 'complexName   : IType'
    //  - Primitive types are emitted with type name only -> 'primitiveName : Type'
    //----------------------------------------------------------------------------------
    string GetAllParameters(Method method)
    {
        var parameters = method.Parameters.Select(p=> $"{p.Name} : {(!p.Type.IsPrimitive ? "I" : "")}{p.Type.Name}");
        return String.Join(", ", parameters);  
    }


}$Classes(:BaseApiController)[$CalculatedModelTypes[import { I$CalculatedResultTypeName } from './$CalculatedResultTypeNameForImport';
]import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class $ServiceName {

  constructor(@Inject('BASE_URL') public baseUrl: string) {}

  $Methods[public Url_$Name = ($Parameters(p => p.Type.IsPrimitive)[$Name: $Type][, ]) => this.baseUrl + `$Url`;
  ]
}]
