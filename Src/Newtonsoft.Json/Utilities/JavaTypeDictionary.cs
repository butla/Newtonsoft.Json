using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
    public static class JavaTypeDictionary
    {
        private static Dictionary<string, string> toJavaDict = new Dictionary<string,string>();
        private static Dictionary<string, string> fromJavaDict = new Dictionary<string,string>();

        public static void SetTypeDictionary(Dictionary<string, string> dotNetToJavaTypes)
        {
            toJavaDict = dotNetToJavaTypes;
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
