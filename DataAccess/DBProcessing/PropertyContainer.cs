using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PropertyContainer
    {
        private readonly Dictionary<string, object> _ids;
        private readonly Dictionary<string, object> _values;

        #region Properties

        internal IEnumerable<string> IdNames
        {
            get { return _ids.Keys; }
        }

        internal IEnumerable<string> ValueNames
        {
            get { return _values.Keys; }
        }

        internal IEnumerable<string> AllNames
        {
            get { return _ids.Keys.Union(_values.Keys); }
        }

        internal IDictionary<string, object> IdPairs
        {
            get { return _ids; }
        }

        internal IDictionary<string, object> ValuePairs
        {
            get { return _values; }
        }

        internal IEnumerable<KeyValuePair<string, object>> AllPairs
        {
            get { return _ids.Concat(_values); }
        }

        #endregion

        #region Constructor

        public PropertyContainer()
        {
            _ids = new Dictionary<string, object>();
            _values = new Dictionary<string, object>();
        }

        #endregion

        #region Methods

        public void AddId(string name, object value)
        {
            _ids.Add(name, value);
        }

        public void AddValue(string name, object value)
        {
            _values.Add(name, value);
        }
        public string FindPropertyNameOFGivenType(Type requiredType)
        {
            string nametoreturn=null;
            foreach (string a in _values.Keys)
            {
                if ((Type)_values[a] == requiredType)
                {
                    nametoreturn=a;

                    break;
                }
            }
            return nametoreturn;
        }
        #endregion
    }


}
