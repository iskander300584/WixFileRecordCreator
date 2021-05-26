using Microsoft.VisualStudio.TestTools.UnitTesting;
using WixFileRecordCreator.DataModels;


namespace UnitTests
{
    /// <summary>
    /// Тесты для класса FileRecord
    /// </summary>
    [TestClass]
    public class FileRecordTests
    {
        /// <summary>
        /// Тестирование метода GetResultString
        /// </summary>
        [TestMethod]
        public void TestGetResultString()
        {
            FileRecord fileRecord1 = new FileRecord()
            {
                IdPrefix = "Patcher",
                KeyPath = false,
                Checksum = true,
                Vital = true,
                FileName = "Ascon.Pilot.DataClasses.dll",
                Path = @"$(var.PatcherExe)\"
            };
            string expected1 = "<File Id=\"PatcherAsconPilotDataClassesdll\" KeyPath=\"no\" Name=\"Ascon.Pilot.DataClasses.dll\" Checksum=\"yes\" Vital=\"yes\" Source=\"$(var.PatcherExe)\\Ascon.Pilot.DataClasses.dll\" />";

            Assert.AreEqual(expected1, fileRecord1.ResultString);


            FileRecord fileRecord2 = new FileRecord()
            {
                KeyPath = false,
                Checksum = true,
                Vital = true,
                FileName = "Ascon.Pilot.DataClasses.dll",
                Path = @"C:\Program Files\ASCON\Plugin"
            };
            string expected2 = "<File Id=\"AsconPilotDataClassesdll\" KeyPath=\"no\" Name=\"Ascon.Pilot.DataClasses.dll\" Checksum=\"yes\" Vital=\"yes\" Source=\"C:\\Program Files\\ASCON\\Plugin\\Ascon.Pilot.DataClasses.dll\" />";

            Assert.AreEqual(expected2, fileRecord2.ResultString);


            FileRecord fileRecord3 = new FileRecord()
            {
                IdPrefix = "Patcher",
                KeyPath = false,
                Checksum = true,
                Vital = true,
                Path = @"C:\Program Files\ASCON\Plugin"
            };
            string expected3 = "";

            Assert.AreEqual(expected3, fileRecord3.ResultString);


            FileRecord fileRecord4 = new FileRecord()
            {
                IdPrefix = "Patcher",
                KeyPath = false,
                Checksum = true,
                Vital = true,
                FileName = "Ascon.Pilot.DataClasses.dll"
            };
            string expected4 = "";

            Assert.AreEqual(expected4, fileRecord4.ResultString);


            FileRecord fileRecord5 = new FileRecord()
            {
                IdPrefix = "Patcher",
                KeyPath = true,
                Checksum = true,
                Vital = false,
                FileName = "Ascon.Pilot.DataClasses.dll",
                Path = @"$(var.PatcherExe)"
            };
            string expected5 = "<File Id=\"PatcherAsconPilotDataClassesdll\" KeyPath=\"yes\" Name=\"Ascon.Pilot.DataClasses.dll\" Checksum=\"yes\" Vital=\"no\" Source=\"$(var.PatcherExe)\\Ascon.Pilot.DataClasses.dll\" />";

            Assert.AreEqual(expected5, fileRecord5.ResultString);


            FileRecord fileRecord6 = new FileRecord()
            {
                IdPrefix = "Patcher",
                KeyPath = false,
                Checksum = false,
                Vital = false,
                FileName = "protobuf-net.dll",
                Path = @"$(var.PatcherExe)"
            };
            string expected6 = "<File Id=\"Patcherprotobufnetdll\" KeyPath=\"no\" Name=\"protobuf-net.dll\" Checksum=\"no\" Vital=\"no\" Source=\"$(var.PatcherExe)\\protobuf-net.dll\" />";

            Assert.AreEqual(expected6, fileRecord6.ResultString);
        }
    }
}