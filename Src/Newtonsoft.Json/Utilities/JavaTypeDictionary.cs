using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
    public static class JavaTypeDictionary
    {
        private static Dictionary<string, string> toJavaDict;
        private static Dictionary<string, string> fromJavaDict;

        static JavaTypeDictionary()
        {
            toJavaDict = new Dictionary<string, string>
            {
                {"Newtonsoft.Json.Tests.Serialization.DaneA, Newtonsoft.Json.Tests", "com.example.jsonrpc4jtest.DaneA"},
                {"Newtonsoft.Json.Tests.Serialization.DaneB, Newtonsoft.Json.Tests", "com.example.jsonrpc4jtest.DaneB"}
            };

            fromJavaDict = toJavaDict.ToDictionary(x => x.Value, x => x.Key);
        }

        public static string ToJava(string typeDescription)
        {
            if(toJavaDict.ContainsKey(typeDescription))
            {
                return toJavaDict[typeDescription];
            }
            else
            {
                return null;
            }            
        }

        public static string FromJava(string typeDescription)
        {
            if (fromJavaDict.ContainsKey(typeDescription))
            {
                return fromJavaDict[typeDescription];
            }
            else
            {
                return null;
            }
        }
    }
}
