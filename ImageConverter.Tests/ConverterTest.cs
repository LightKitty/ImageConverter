using System;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImageConverter.Tests
{
    [TestClass]
    public class ConverterTest
    {
        [TestMethod]
        public void ZoomTest()
        {
            string path = @"E:\地球频道\2.videos\20210419\hima820210418112000fd.png";
            //string path = @"E:\地球频道\2.videos\20210419\hima820210418014000fd.png";
            //string path = @"F:\Pictures\Sci Fi_Space_319803.jpg";
            string newPath = @"E:\222.jpg";
            Converter.Zoom(path, newPath, 0.1);
        }
    }
}
