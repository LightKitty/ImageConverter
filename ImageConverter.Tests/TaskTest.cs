using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageConverter.Tests
{
    [TestClass]
    public class TaskTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int a = Environment.ProcessorCount;
            ThreadPool.SetMaxThreads(a, a);
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                //var t = Task.Run(() =>
                //{
                //    Thread.Sleep(3000);
                //});
                var t = Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(60000);
                });
                tasks.Add(t);
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("Thread Finished!");
        }

        private static object lockObject = new object();

        [TestMethod]
        public void TestMethod2()
        {
            int a = Environment.ProcessorCount;
            ThreadPool.SetMaxThreads(a, a);

            DirectoryInfo dir = new DirectoryInfo(@"E:\地球频道\2.videos\20210419");
            //path为某个目录，如： “D:\Program Files”
            FileInfo[] inf = dir.GetFiles();


            List<string> imageList = new List<string>();

            foreach (FileInfo finf in inf)
            {
                if (finf.Extension.ToLower().Equals(".png"))
                    imageList.Add(finf.FullName);
            }


            Task[] tasks = new Task[imageList.Count];
            for (int i = 0; i < imageList.Count; i++)
            {
                tasks[i] = Task.Factory.StartNew(index =>
               {
                   try
                   {
                       Converter.Zoom(imageList[(int)index], $"f:/{index}.png", 0.1);
                   }
                   catch(Exception ex)
                   {
                       System.Console.WriteLine(i.ToString() + "|" + ex.ToString());
                   }
               }, i);
            }
            Task.WaitAll(tasks);
            Console.WriteLine("Thread Finished!");
        }
    }
}
