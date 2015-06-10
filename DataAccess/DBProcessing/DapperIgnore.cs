using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DapperIgnore : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class Encrypted : Attribute
    {

    }
    public class EncryptionOfDatabaseStoreValues
    {
        public static void Encrypt(IEnumerable<object> objectInQuestion)
        {
            EncryptOrDecrypt(objectInQuestion, true);



        }
        public static void Decrypt(IEnumerable< object > objectInQuestion)
        {
            EncryptOrDecrypt(objectInQuestion, false);



        }
        public static void Decrypt(object objectInQuestion)
        {
            EncryptOrDecrypt(objectInQuestion, false);



        }
        public static void Encrypt(object objectInQuestion)
        {
            EncryptOrDecrypt(objectInQuestion,true);

            

        }
        public static void EncryptOrDecrypt(object objectInQuestion,bool encryptflag)
        {
            string ecnryptedorDecrypted;
            PropertyContainer PC = ParseProperties(objectInQuestion.GetType());
            foreach (var prop in PC.ValuePairs)
            {
                string propName = prop.Key;
                Type proptype =(Type) prop.Value;
                var currentValue = objectInQuestion.GetType().GetProperty(propName).GetValue(objectInQuestion, null);
                string s = currentValue.ToString();
                if (encryptflag == true)
                {
                     ecnryptedorDecrypted = EncryptThisString(s);
                }
                else
                {
                     ecnryptedorDecrypted = DeccryptThisString(s);
                }
                object valuetoset = Convert.ChangeType(ecnryptedorDecrypted, proptype);

                objectInQuestion.GetType().GetProperty(propName).SetValue(objectInQuestion, valuetoset, null);
            }

        }
        public static string DeccryptThisString(string s)
        {
            
            var val = System.Convert.FromBase64String(s);
            return GetString(val);
        }
        public static string EncryptThisString(string s)
        {
            Byte[] arr = GetBytes(s);
            var val=System.Convert.ToBase64String(arr,
                                0,
                                arr.Length);
            return val;
        }
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        public static PropertyContainer ParseProperties(Type obj)
        {

            var propertyContainer = new PropertyContainer();
            var typeName = obj.Name;
            var validKeyNames = new[] { "Id",
            string.Format("{0}Id", typeName), string.Format("{0}_Id", typeName) };
            var properties = obj.GetProperties();
            foreach (var property in properties)
            {

                // Skip reference types (but still include string!)

                //if (property.PropertyType.IsClass && property.PropertyType != typeof(string))

                //    continue;



                // Skip methods without a public setter

                //if (property.GetSetMethod() == null)

                //    continue;



                // Skip methods specifically ignored

                if (property.IsDefined(typeof(Encrypted), false))

                //continue;
                {

                    var name = property.Name;

                    var value = obj.GetProperty(property.Name).PropertyType;



                    //if (property.IsDefined(typeof(DapperKey), false) || validKeyNames.Contains(name))
                    //{

                    //    propertyContainer.AddId(name, value);

                    //}

                    //else
                    //{

                        propertyContainer.AddValue(name, value);

                    //}
                }

            }



            return propertyContainer;

        }
    }
}
