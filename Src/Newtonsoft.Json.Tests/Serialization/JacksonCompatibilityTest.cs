using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Tests.Serialization
{
    [TestFixture]
    class JacksonCompatibilityTest
    {
        private readonly JsonSerializerSettings CommonSettings;
        private const string SerializedDaneA = @"{""@class"":""com.example.jsonrpc4jtest.DaneA"",""liczbaA"":38,""tekstA"":""costam AAA""}";
        private readonly DaneA TestDaneA;

        public JacksonCompatibilityTest()
        {
            CommonSettings = new JsonSerializerSettings();
            CommonSettings.TypeNameHandling = TypeNameHandling.All;

            TestDaneA = new DaneA() { liczbaA = 38, tekstA = "costam AAA" };
        }

        [Test]
        public void AAADeserialize()
        {
            object deserialized = JsonConvert.DeserializeObject(SerializedDaneA, this.CommonSettings);
            Assert.IsInstanceOf(typeof(DaneA), deserialized);
            DaneA deserializedTyped = deserialized as DaneA;
            Assert.AreEqual(TestDaneA.liczbaA, deserializedTyped.liczbaA);
            Assert.AreEqual(TestDaneA.tekstA, deserializedTyped.tekstA);
        }


        [Test]
        public void AaaSerialize()
        {
            string serialized = JsonConvert.SerializeObject(TestDaneA, Formatting.None, this.CommonSettings);
            Assert.AreEqual(SerializedDaneA, serialized);
        }
    }

    class DaneA
    {
        public int liczbaA = 13;
        public string tekstA = "domyslny";
    }

    class DaneB : DaneA
    {
        public double liczbaB = 5.25;
    }

    public class Call
    {
        public List<string> someStrings;
        public int[] someInts;
        public Object[] parameters;

        public Call()
        {
            someInts = new int[] { 1, 2, 3 };
            someStrings = new List<string> { "a", "b", "c" };
        }
    }
}
