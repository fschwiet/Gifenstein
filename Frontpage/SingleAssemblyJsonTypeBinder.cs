using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Frontpage
{
    public class SingleAssemblyJsonTypeBinder : SerializationBinder
    {
        private readonly Assembly _assembly;
        private Dictionary<string, Type> _typesBySimpleName = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase); 
        private Dictionary<Type,string> _simpleNameByType = new Dictionary<Type, string>(); 

        public SingleAssemblyJsonTypeBinder(Assembly assembly)
        {
            _assembly = assembly;
            _typesBySimpleName = new Dictionary<string, Type>();

            foreach (var type in _assembly.GetTypes().Where(t => t.IsPublic))
            {
                if (_typesBySimpleName.ContainsKey(type.Name))
                    throw new InvalidOperationException("Cannot user PolymorphicBinder on a namespace where multiple public types have same name.");

                _typesBySimpleName[type.Name] = type;
                _simpleNameByType[type] = type.Name;
            }
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            Type result;
            if (_typesBySimpleName.TryGetValue(typeName.Trim(), out result))
                return result;

            return null;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            string name;

            if (_simpleNameByType.TryGetValue(serializedType, out name))
            {
                typeName = name;
                assemblyName = null;// _assembly.FullName;
            }
            else
            {
                typeName = null;
                assemblyName = null;
            }
        }
    }
}