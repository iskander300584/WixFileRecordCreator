using Microsoft.VisualStudio.TestTools.UnitTesting;
using WixFileRecordCreator.Helpers;


namespace UnitTests
{
    /// <summary>
    /// Тесты для класса FileNameWorker
    /// </summary>
    [TestClass]
    public class StringConverterTests
    {
        /// <summary>
        /// Тестирование метода GetStringWithoutSymbols
        /// </summary>
        [TestMethod]
        public void TestGetStringWithoutSymbols()
        {
            string source1 = @"Microsoft.AspNetCore.Hosting.Server.Abstractions.dll";
            string expected1 = @"MicrosoftAspNetCoreHostingServerAbstractionsdll";

            string source2 = @"log4net.dll";
            string expected2 = @"log4netdll";

            string source3 = @"protobuf-net.dll";
            string expected3 = @"protobufnetdll";

            string source4 = @"1protobuf-net.dll";
            string expected4 = @"protobufnetdll";

            string source5 = @"123protobuf-net.dll";
            string expected5 = @"protobufnetdll";

            Assert.AreEqual(expected1, StringConverter.GetStringWithoutSymbols(source1));
            Assert.AreEqual(expected2, StringConverter.GetStringWithoutSymbols(source2));
            Assert.AreEqual(expected3, StringConverter.GetStringWithoutSymbols(source3));
            Assert.AreEqual(expected4, StringConverter.GetStringWithoutSymbols(source4));
            Assert.AreEqual(expected5, StringConverter.GetStringWithoutSymbols(source5));
        }


        /// <summary>
        /// Тестирование метода BoolToString
        /// </summary>
        [TestMethod]
        public void TestBoolToString()
        {
            bool source1 = true;
            string expected1 = "yes";

            bool source2 = false;
            string expected2 = "no";

            Assert.AreEqual(expected1, StringConverter.BoolToString(source1));
            Assert.AreEqual(expected2, StringConverter.BoolToString(source2));
        }


        /// <summary>
        /// Тестирование метода CombinePath
        /// </summary>
        [TestMethod]
        public void TestCombinePath()
        {
            string fileName = @"fileName.txt";

            string path1 = @"C:\ProgramFiles (x86)\ASCON\Plugins\";
            string path2 = @"C:\ProgramFiles (x86)\ASCON\Plugins";
            string expected1 = @"C:\ProgramFiles (x86)\ASCON\Plugins\fileName.txt";

            
            string path3 = @"$(var.PatcherExe)\";
            string path4 = @"$(var.PatcherExe)";
            string expected2 = @"$(var.PatcherExe)\fileName.txt";


            Assert.AreEqual(expected1, StringConverter.CombinePath(path1, fileName));
            Assert.AreEqual(expected1, StringConverter.CombinePath(path2, fileName));
            Assert.AreEqual(expected2, StringConverter.CombinePath(path3, fileName));
            Assert.AreEqual(expected2, StringConverter.CombinePath(path4, fileName));
        }
    }
}