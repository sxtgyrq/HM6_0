using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawObj
{
    public class KML
    {
        public static void Draw(List<ModelBase.Data.FPPosition> pointData)
        {
            var kml = new Kml();
            var document = new Document();
            kml.Feature = document;

            //var points = new List<Vector>
            //{
            //    new Vector(37.422, -122.084,0), // 第一个点的坐标 (纬度, 经度)
            //    new Vector(37.428, -122.085, ), // 第二个点的坐标
            //    new Vector(37.429, -122.082)  // 第三个点的坐标
            //};
            for (int i = 0; i < pointData.Count; i++)
            {
                var placemark = new Placemark
                {
                    Name = $"{pointData[i].fPName}",
                    Geometry = new SharpKml.Dom.Point
                    {
                        Coordinate = new Vector(pointData[i].lat, pointData[i].lon, pointData[i].Height + pointData[i].baseHeight),
                        AltitudeMode = AltitudeMode.Absolute,
                        Id = $"{pointData[i].fPCode}_{pointData[i].Height}"
                    },
                    Description = new Description()
                    {
                        Text = pointData[i].lineStr
                    }
                };
                document.AddFeature(placemark);
            }
            if (false)
            {

                var lineCoordinates = new List<Vector>
            {
                new Vector(37.422, -122.084), // 起点坐标 (纬度, 经度)
                new Vector(37.428, -122.085), // 中间点坐标
                new Vector(37.429, -122.082)  // 终点坐标
            };
                var lineString = new LineString
                {
                    Coordinates = new CoordinateCollection(lineCoordinates),
                    AltitudeMode = AltitudeMode.Absolute // 高度模式，可选
                };
                var linePlacemark = new Placemark
                {
                    Name = "Line Example",
                    Geometry = lineString
                };
                document.AddFeature(linePlacemark);
            }
            //var lineString = new LineString
            //{
            //    Coordinates = new CoordinateCollection(lineCoordinates),
            //    AltitudeMode = AltitudeMode.ClampToGround // 高度模式，可选
            //};
            // throw new NotImplementedException();
            var serializer = new Serializer();
            serializer.Serialize(kml);

            // 保存到文件
            File.WriteAllText("output_with_line.kml", serializer.Xml);

            Console.WriteLine("KML file with points and a line has been created successfully.");
        }
    }
}
