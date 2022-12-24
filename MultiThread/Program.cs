using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace MultiThread
{
    public class Program
    {
        public static List<object[]> objects;
        public static string pdfPath = @"C:\Users\ali\source\repos\MultiThread\MultiThread\PdfPath";

        public static void Main(string[] args)
        {
            LoadList();
            CreatePdf();
            Console.ReadLine();
        }
        public static void LoadList()
        {
            // Fake objeler listesi ve her bir objenin detayları
            objects = new List<object[]>();
            for (int i = 0; i < 100; i++)
            {
                objects.Add(new object[] { i, $"Name{i}", $"Surname{i}", $"01/01/19{i % 100}", "Status" });
            }
        }
        public static void CreatePdf()
        {
            CheckPath();
            // Threads listesi
            List<Thread> threads = new List<Thread>();

            foreach (object[] obj in objects)
            {
                // Objenin detaylarını al
                int id = (int)obj[0];
                string name = (string)obj[1];
                string surname = (string)obj[2];
                string bdate = (string)obj[3];
                string status = (string)obj[4];

                Thread thread = new Thread(() =>
                {
                    // PDF dosyasını oluştur ve objenin detaylarını içine yaz
                    string fileName = $"object_{id}.pdf";
                    string filePath = Path.Combine(pdfPath, fileName);

                    using (FileStream stream = new FileStream(filePath, FileMode.Create))

                    {
                        Document document = new Document();
                        PdfWriter.GetInstance(document, stream);
                        document.Open();

                        document.Add(new Paragraph($"ID: {id}"));
                        document.Add(new Paragraph($"Name: {name}"));
                        document.Add(new Paragraph($"Surname: {surname}"));
                        document.Add(new Paragraph($"Birth date: {bdate}"));
                        document.Add(new Paragraph($"Status: {status}"));

                        document.Close();
                    }
                    Console.WriteLine(filePath);
                });

                thread.Priority = ThreadPriority.Lowest;
                thread.Start();
                threads.Add(thread);

                if (threads.Count == 5)
                {
                    threads[0].Join();
                    threads.RemoveAt(0);
                }
                Console.WriteLine(threads.Count.ToString());
            }
        }
        public static void CheckPath()
        {
            // Dizin yoksa oluştur
            if (!Directory.Exists(pdfPath))
            {
                Directory.CreateDirectory(pdfPath);
            }
        }
    }
}

