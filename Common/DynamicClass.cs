using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;
using System.Reflection.Emit;


namespace Common
{
    public class PropertyNameAndType
    {
        public string Name { get; set; }
        public Type Type_ { get; set; }
        public object value { get; set; }
    }
    public class DynamicClass
    {
        public static int counter = 0;
        List<PropertyNameAndType> _properties = new List<PropertyNameAndType>();
        public dynamic resultObject;
        Type classType;
        public List<PropertyNameAndType> properties
        {
            get
            {
                return _properties;
            }
            set
            {
                _properties = value;
            }
        }
        public DynamicClass(List<PropertyNameAndType> prop)
        {
            properties = prop;
            var b = createTheObject();
            populateValues();
        }
        private void populateValues()
        {
            foreach (var aprop in properties)
            {
                var d = aprop.value;
                this.classType.GetProperty(aprop.Name).SetValue(this.resultObject, aprop.value, null);
            }
        }
        public DynamicClass(Dictionary<string, Type> prop)
        {
            foreach (string ky in prop.Keys)
            {
                PropertyNameAndType a = new PropertyNameAndType() { Name = ky, Type_ = prop[ky] };
                properties.Add(a);
            }
            var b = createTheObject();
            populateValues();

        }
        private object createTheObject()
        {
            counter = counter + 1;
            //var yourListOfFields = new PropertyNameAndType[] { new PropertyNameAndType() { Name = "prop1", Type_ = typeof(string) }, new PropertyNameAndType() { Name = "prop2", Type_ = typeof(string) } };
            var z = MyTypeBuilder.CompileResultType(properties, "MyDynamicType" + counter);
            var z2 = Activator.CreateInstance(z);
            resultObject = z2;
            classType = z2.GetType();
            return z2;
            //z2.GetType().GetProperty("prop1").SetValue(z2, "Bob", null);
        }
    }
    public static class MyTypeBuilder
    {
        public static void CreateNewObject(IEnumerable<PropertyNameAndType> yourListOfFields, string nameOfType)
        {
            //var yourListOfFields = new[] { new { FieldName = "prop1", FieldType = typeof(string) }, new { FieldName = "prop2", FieldType = typeof(string) } };
            var myType = CompileResultType(yourListOfFields, nameOfType);
            var myObject = Activator.CreateInstance(myType);
        }
        public static Type CompileResultType(IEnumerable<PropertyNameAndType> yourListOfFields, string nameOfType)
        {
            TypeBuilder tb = GetTypeBuilder(nameOfType);
            ConstructorBuilder constructor = tb.DefineDefaultConstructor(MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
            //List<object> yourListOfFields = new List<object>();
            //yourListOfFields.Add(new { FieldName = "a", FieldType = "b" });
            string z = "5";

            //var yourListOfFields = new { new { FieldName = "a", FieldType = "b" }, new { FieldName = "a", FieldType = "b" } };
            // NOTE: assuming your list contains Field objects with fields FieldName(string) and FieldType(Type)
            //Type gg = typeof(string);
            foreach (var field in yourListOfFields)
                //var gg=typeof(string).GetProperty("FieldName").GetValue(field, null);

                CreateProperty(tb, field.Name, field.Type_);
            //Type mm = field.GetType();
            Type objectType = tb.CreateType();

            return objectType;
        }

        private static TypeBuilder GetTypeBuilder(string nameOfType)
        {
            //var typeSignature = "MyDynamicType";
            var typeSignature = nameOfType;
            var an = new AssemblyName(typeSignature);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(typeSignature
                                , TypeAttributes.Public |
                                TypeAttributes.Class |
                                TypeAttributes.AutoClass |
                                TypeAttributes.AnsiClass |
                                TypeAttributes.BeforeFieldInit |
                                TypeAttributes.AutoLayout
                                , null);
            return tb;
        }

        private static void CreateProperty(TypeBuilder tb, string propertyName, Type propertyType)
        {
            FieldBuilder fieldBuilder = tb.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);

            PropertyBuilder propertyBuilder = tb.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
            MethodBuilder getPropMthdBldr = tb.DefineMethod("get_" + propertyName, MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            ILGenerator getIl = getPropMthdBldr.GetILGenerator();

            getIl.Emit(OpCodes.Ldarg_0);
            getIl.Emit(OpCodes.Ldfld, fieldBuilder);
            getIl.Emit(OpCodes.Ret);

            MethodBuilder setPropMthdBldr =
                tb.DefineMethod("set_" + propertyName,
                  MethodAttributes.Public |
                  MethodAttributes.SpecialName |
                  MethodAttributes.HideBySig,
                  null, new[] { propertyType });

            ILGenerator setIl = setPropMthdBldr.GetILGenerator();
            Label modifyProperty = setIl.DefineLabel();
            Label exitSet = setIl.DefineLabel();

            setIl.MarkLabel(modifyProperty);
            setIl.Emit(OpCodes.Ldarg_0);
            setIl.Emit(OpCodes.Ldarg_1);
            setIl.Emit(OpCodes.Stfld, fieldBuilder);

            setIl.Emit(OpCodes.Nop);
            setIl.MarkLabel(exitSet);
            setIl.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getPropMthdBldr);
            propertyBuilder.SetSetMethod(setPropMthdBldr);
        }
    }
}
