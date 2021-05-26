using Microsoft.VisualStudio.TestTools.UnitTesting;
using WixFileRecordCreator.DataContexts;


namespace UnitTests
{
    /// <summary>
    /// Тесты для класса MainDataContext
    /// </summary>
    [TestClass]
    public class MainDataContextTests
    {
        /// <summary>
        /// Тестирование метода GetResultPath
        /// </summary>
        [TestMethod]
        public void TestGetResultPath()
        {
            MainDataContext mainDataContext1 = new MainDataContext()
            {
                FolderName = @"C:\Program Files\ASCON\Plugin",
                FolderIsVariable = false,
                VariableName = ""
            };
            string expected1 = @"C:\Program Files\ASCON\Plugin";

            Assert.AreEqual(expected1, mainDataContext1.ResultPath);


            MainDataContext mainDataContext2 = new MainDataContext()
            {
                FolderName = @"C:\Program Files\ASCON\Plugin",
                FolderIsVariable = true,
                VariableName = ""
            };
            string expected2 = "";

            Assert.AreEqual(expected2, mainDataContext2.ResultPath);


            MainDataContext mainDataContext3 = new MainDataContext()
            {
                FolderIsVariable = true,
                VariableName = "PatcherExe"
            };
            string expected3 = @"$(var.PatcherExe)\";

            Assert.AreEqual(expected3, mainDataContext3.ResultPath);
        }
    }
}