using System;
using System.Collections.Generic;
using System.Reflection;

public static class PredefinedAssemblyUtil
{
    enum AssemlyType
    {
        AssemblyCSharp,
        AssemblyCSharpEditor,
        AssemblyCSharpFirstPass,
        AssemblyCSharpEditorFirstPass,
    }

    static AssemlyType? GetAssemblyType(string assemblyname)
    {
        return assemblyname switch
        {
            "Assembly-CSharp" => AssemlyType.AssemblyCSharp,
            "Assembly-CSharp-Editor" => AssemlyType.AssemblyCSharpEditor,
            "Assembly-CSharp-firstpass" => AssemlyType.AssemblyCSharpFirstPass,
            "Assembly-CSharp-Editor-firstpass" => AssemlyType.AssemblyCSharpEditorFirstPass,
            _ => null,
        };
    }

    static void AddTypesFromAssembly(Type[] assembly, ICollection<Type> types, Type interfaceType)
    {
        if(assembly == null) return;
        for (int i = 0; i < assembly.Length; i++)
        {
            Type type = assembly[i];
            if(type != interfaceType && interfaceType.IsAssignableFrom(type))
            {
                types.Add(type);
            }
        }
    }

    public static List<Type> GetTypes(Type interfaceType)
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        
        Dictionary<AssemlyType,Type[]> assemblyTypes = new Dictionary<AssemlyType, Type[]>();
        List<Type> types = new List<Type>();
        for (int i = 0; i < assemblies.Length; i++)
        {
            AssemlyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
            if(assemblyType != null)
            {
                assemblyTypes.Add((AssemlyType)assemblyType, assemblies[i].GetTypes());
            }
        }
        
        AddTypesFromAssembly(assemblyTypes[AssemlyType.AssemblyCSharp], types,interfaceType);
        AddTypesFromAssembly(assemblyTypes[AssemlyType.AssemblyCSharpFirstPass], types,interfaceType);
        return types;
    } 
}