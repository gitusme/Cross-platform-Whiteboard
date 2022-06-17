using Com.Gitusme.Whiteboard.Platforms;
using Com.Gitusme.Whiteboard.Strokes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Com.Gitusme.Whiteboard
{
    public class GraphicsWriter
    {
        private string _dir;

        public GraphicsWriter()
        {
            PlatformService platformService = new PlatformService();
            _dir = platformService.GetStoragePath();
            if (!Directory.Exists(_dir))
            {
                Directory.CreateDirectory(_dir);
            }
        }

        public async void WriteToPictrue(Task<IScreenshotResult> screenshot)
        {
            IScreenshotResult result = await screenshot;
            using (Stream stream = await result.OpenReadAsync())
            {
                string datetime = DateTime.Now.ToString("yyyyMMddhhmmss");
                using (FileStream fileStream = new FileStream(Path.Combine(_dir, $"Whiteboard-{datetime}.png"), FileMode.OpenOrCreate))
                {
                    stream.CopyTo(fileStream);
                }
            }
        }

        public void WriteToXml(Stroke[] strokes)
        {
            string datetime = DateTime.Now.ToString("yyyyMMddhhmmss");
            using (TextWriter writer = new StreamWriter(Path.Combine(_dir, $"Whiteboard-{datetime}.xml"), false, Encoding.UTF8))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(StrokeXml));
                serializer.Serialize(writer, new StrokeXml { Strokes = strokes });
            }
        }

        [XmlRoot("Root")]
        public class StrokeXml
        {
            [XmlArray(ElementName = "Strokes")]
            public Stroke[] Strokes { get; set; }
        }

    }
}
