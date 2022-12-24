using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace MultiThread.Test
{
    //[TestClass]
    //public class UnitTest1
    //{
    //    [TestMethod]
    //    public void TestLoadList()
    //    {
    //        // LoadList metodunu �al��t�r ve objeler listesini al
    //        Program.LoadList();
    //        List<object[]> objects = Program.objects;

    //        // Test: objeler listesi do�ru bir �ekilde y�klenmi� mi?
    //        Assert.AreEqual(100, objects.Count);
    //        for (int i = 0; i < 100; i++)
    //        {
    //            Assert.AreEqual(i, (int)objects[i][0]);
    //            Assert.AreEqual($"Name{i}", (string)objects[i][1]);
    //            Assert.AreEqual($"Surname{i}", (string)objects[i][2]);
    //            Assert.AreEqual($"01/01/19{i % 100}", (string)objects[i][3]);
    //            Assert.AreEqual("Status", (string)objects[i][4]);
    //        }
    //    }

    //    [TestMethod]
    //    public void TestCreatePdf()
    //    {
    //        // LoadList metodunu �al��t�r ve objeler listesini al
    //        Program.LoadList();
    //        List<object[]> objects = Program.objects;

    //        // PDF dosyalar�n� olu�tur
    //        Program.CreatePdf();

    //        // Test: PDF dosyalar� do�ru bir �ekilde olu�turulmu� mu?
    //        foreach (object[] obj in objects)
    //        {
    //            int id = (int)obj[0];
    //            string fileName = $"object_{id}.pdf";
    //            string filePath = Path.Combine(Program.pdfPath, fileName);
    //            Assert.IsTrue(File.Exists(filePath));
    //        }
    //    }
    //}


    [TestClass]
    public class UnitTest1
    {
        private ProgramFixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new ProgramFixture();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _fixture.Dispose();
        }

        [TestMethod]
        public void TestLoadList()
        {
            // objeler listesini al
            List<object[]> objects = _fixture.objects;

            // Test: objeler listesi do�ru bir �ekilde y�klenmi� mi?
            Assert.AreEqual(100, objects.Count);
            for (int i = 0; i < 100; i++)
            {
                Assert.AreEqual(i, (int)objects[i][0]);
                Assert.AreEqual($"Name{i}", (string)objects[i][1]);
                Assert.AreEqual($"Surname{i}", (string)objects[i][2]);
                Assert.AreEqual($"01/01/19{i % 100}", (string)objects[i][3]);
                Assert.AreEqual("Status", (string)objects[i][4]);
            }
        }

        [TestMethod]
        public void TestCreatePdf()
        {
            // objeler listesini al
            List<object[]> objects = _fixture.objects;

            // PDF dosyalar�n� olu�tur
            _fixture.CreatePdf();

            // Test: PDF dosyalar� do�ru bir �ekilde olu�turulmu� mu?
            foreach (object[] obj in objects)
            {
                int id = (int)obj[0];
                string fileName = $"object_{id}.pdf";
                string filePath = Path.Combine(_fixture.pdfPath, fileName);
                Assert.IsTrue(File.Exists(filePath));
            }
        }
    }

    public class ProgramFixture : IDisposable
    {
        public List<object[]> objects { get; private set; }
        public string pdfPath { get; private set; }

        public ProgramFixture()
        {
            // LoadList metodunu �al��t�r ve objeler listesini al
            Program.LoadList();
            objects = Program.objects;
            pdfPath = Program.pdfPath;
        }

        public void CreatePdf()
        {
            // PDF dosyalar�n� olu�tur
            Program.CreatePdf();
        }

        public void Dispose()
        {
            // PDF dosyalar�n� sil
            foreach (object[] obj in objects)
            {
                int id = (int)obj[0];
                string fileName = $"object_{id}.pdf";
                string filePath = Path.Combine(pdfPath, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }

    }
}

