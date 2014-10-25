using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Tests.Serialization
{
    [TestFixture]
    class JacksonCompatibilityTest
    {
        private readonly JsonSerializerSettings CommonSettings;

        public JacksonCompatibilityTest()
        {
            CommonSettings = new JsonSerializerSettings();
            CommonSettings.TypeNameHandling = TypeNameHandling.All;
        }

        [Test]
        public void AAADeserialize()
        {
            string serializedCall = @"{
  ""@class"":""uniserialjava.Call"",
  ""someStrings"": [ ""java.util.ArrayList"", [""a"", ""b"", ""c""] ],
  ""someInts"": [1,2,3],
  ""parameters"": [
    ""[Ljava.lang.Object;"",
    [
      28,
      ""jakis tekst"",
      {
        ""@class"":""uniserialjava.DaneB"",
        ""numberA"":13,
        ""stringA"":""domyslny"",
        ""numberB"":5.25
      },
      [""[I"",[1,2,3] ]]
    ]
  ]
}";

            object deserialized = JsonConvert.DeserializeObject(serializedCall, this.CommonSettings);
            Assert.IsInstanceOf(typeof(Call), deserialized);
        }


        [Test]
        public void AaaSerialize()
        {
            int[] tablicaIntow = new int[] { 1, 2, 3 };
            object[] tablica = new object[] { 28, "jakis tekst", new DaneB(), tablicaIntow };

            Call call = new Call() { parameters = tablica };

            string serialized = JsonConvert.SerializeObject(call, Formatting.Indented, this.CommonSettings);
            Console.WriteLine(serialized);
        }

        [Test]
        public void AaaOriginalDeserialization()
        {
            string serializedOriginal = @"{
  ""$type"": ""Newtonsoft.Json.Tests.Serialization.Call, Newtonsoft.Json.Tests"",
  ""someStrings"": {
    ""$type"": ""System.Collections.Generic.List`1[[System.String, mscorlib]], mscorlib"",
    ""$values"": [""a"", ""b"", ""c""]
  },
  ""someInts"": {
    ""$type"": ""System.Int32[], mscorlib"",
    ""$values"": [1, 2, 3]
  },
  ""parameters"": {
    ""$type"": ""System.Object[], mscorlib"",
    ""$values"": [
      28,
      ""jakis tekst"",
      {
        ""$type"": ""Newtonsoft.Json.Tests.Serialization.DaneB, Newtonsoft.Json.Tests"",
        ""liczbaB"": 5.25,
        ""liczbaA"": 13,
        ""tekstA"": ""domyslny""
      },
      {
        ""$type"": ""System.Int32[], mscorlib"",
        ""$values"": [1, 2, 3]
      }
    ]
  }
}";

            object deserialized = JsonConvert.DeserializeObject(serializedOriginal, this.CommonSettings);
            Assert.IsInstanceOf(typeof(Call), deserialized);
        }


        [Test]
        public void AaaOriginalDeserialSimpler()
        {
            string serializedOriginal = @"{
  ""$type"": ""System.Object[], mscorlib"",
  ""$values"": [
    28,
    ""jakis tekst"",
    {
      ""$type"": ""Newtonsoft.Json.Tests.Serialization.DaneA, Newtonsoft.Json.Tests"",
      ""liczbaA"": 13,
      ""tekstA"": ""domyslny""
    },
    {
      ""$type"": ""Newtonsoft.Json.Tests.Serialization.DaneB, Newtonsoft.Json.Tests"",
      ""liczbaB"": 5.25,
      ""liczbaA"": 13,
      ""tekstA"": ""domyslny""
    }
  ]
}";

            object deserialized = JsonConvert.DeserializeObject(serializedOriginal, this.CommonSettings);
            Assert.IsInstanceOf(typeof(object[]), deserialized);
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
