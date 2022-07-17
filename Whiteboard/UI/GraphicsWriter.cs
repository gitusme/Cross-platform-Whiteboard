using Com.Gitusme.Whiteboard.Platforms;
using Com.Gitusme.Whiteboard.Strokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Com.Gitusme.Whiteboard.UI
{
    public class GraphicsWriter
    {
        private string _dir;
        private string _fileName;

        public GraphicsWriter(string fileName)
        {
            PlatformService platformService = new PlatformService();
            _dir = platformService.GetStoragePath();
            if (!Directory.Exists(_dir))
            {
                Directory.CreateDirectory(_dir);
            }
            _fileName = fileName;
        }

        public async void WriteToPictrue(Task<IScreenshotResult> screenshot)
        {
            await Task.Run(() =>
            {
                IScreenshotResult result = screenshot.GetAwaiter().GetResult();
                using (Stream stream = result.OpenReadAsync().GetAwaiter().GetResult())
                {
                    using (FileStream fileStream = new FileStream(Path.Combine(_dir, $"{_fileName}.png"), FileMode.OpenOrCreate))
                    {
                        stream.CopyTo(fileStream);
                    }
                }
            });
        }

        public async void WriteToXml(Stroke[] strokes)
        {
            await Task.Run(() =>
            {
                using (TextWriter writer = new StreamWriter(Path.Combine(_dir, $"{_fileName}.xml"), false, Encoding.UTF8))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(StrokeXml));
                    serializer.Serialize(writer, new StrokeXml { Strokes = strokes });
                }
            });
        }

        [XmlRoot("Root")]
        public class StrokeXml
        {
            [XmlArray(ElementName = "Strokes")]
            public Stroke[] Strokes { get; set; }
        }

    }
}
