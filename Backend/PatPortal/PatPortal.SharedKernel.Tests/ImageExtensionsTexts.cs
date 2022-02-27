using NUnit.Framework;
using PatPortal.SharedKernel.Enums;
using PatPortal.SharedKernel.Extensions;
using System.IO;

namespace PatPortal.Unit.Tests
{

    public class ImageExtensionsTexts
    {
        [Test]
        [TestCase(@"..\..\..\Images\Photo.jpg", ImageFormat.Jpeg)]
        [TestCase(@"..\..\..\Images\Photo.png", ImageFormat.Png)]
        [TestCase(@"..\..\..\Images\Photo.gif", ImageFormat.Gif)]
        [TestCase(@"..\..\..\Images\Photo.bmp", ImageFormat.Bmp)]
        [TestCase(@"..\..\..\Images\Photo.tif", ImageFormat.Tiff)]
        [TestCase(@"..\..\..\Images\Photo.txt", ImageFormat.Unknown)]
        public void GetImageFormatTests(string path, ImageFormat resultFormat)
        {
            //Arrange
            var fileBytes = File.ReadAllBytes(path);

            //Act
            var fileFormat = fileBytes.GetImageFormat();

            //Assert
            Assert.AreEqual(resultFormat, fileFormat);
        }
    }
}
