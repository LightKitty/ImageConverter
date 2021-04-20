using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageConverter.Console
{
    class Program
    {
        static void Main(string[] args)
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
                    System.Console.WriteLine(index);
                    try
                    {
                        Converter.Zoom(imageList[(int)index], $"f:/{index}.png", 0.1);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(i.ToString() + "|" + ex.ToString());
                    }
                }, i);
            }
            Task.WaitAll(tasks);
            System.Console.WriteLine("Thread Finished!");
        }
    }
}
