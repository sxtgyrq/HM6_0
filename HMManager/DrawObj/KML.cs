using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
//using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DrawObj
{
    public class KML
    {
        public static void Draw(List<ModelBase.Data.FPPosition> pointData, List<ModelBase.Data.Segment> lineData)
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
            var lineStyle_w5 = new Style
            {
                Id = "lineStyleW5",
                Line = new LineStyle
                {
                    Color = Color32.Parse("ff0000ff"), // 蓝色线段
                    Width = 5
                }
            };
            var lineStyle_w10 = new Style
            {
                Id = "lineStyleW10",
                Line = new LineStyle
                {
                    Color = Color32.Parse("ff0000ff"), // 蓝色线段
                    Width = 10
                }
            };
            document.AddStyle(lineStyle_w5);
            document.AddStyle(lineStyle_w10);
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
            for (int i = 0; i < lineData.Count; i++)
            {
                var startPoint = new Vector(lineData[i].StartLat, lineData[i].StartLon, lineData[i].StartHeight);
                var endPoint = new Vector(lineData[i].EndLat, lineData[i].EndLon, lineData[i].EndHeight);
                var lineCoordinates = new List<Vector>
                {
                   startPoint, endPoint
                };
                var lineString = new LineString
                {
                    Coordinates = new CoordinateCollection(lineCoordinates),
                    AltitudeMode = AltitudeMode.Absolute, // 高度模式，可选
                    Id = lineData[i].SegCode,
                };
                var linePlacemark = new Placemark
                {
                    Name = lineData[i].SegCode,
                    Geometry = lineString,
                    Description = new Description()
                    {
                        Text = $"{lineData[i].FPCodeFrom} {lineData[i].StartBaseHeight} {lineData[i].FPCodeTo} {lineData[i].EndBaseHeight} {"\r\n"}__{lineData[i].Detail}"
                    },

                };
                // 生成箭头的两条斜线段
                double arrowLength = 0.0001; // 箭头长度（可根据需要调整）
                double arrowWidth = 0.00005; // 箭头宽度（可根据需要调整）

                var arrowLeft = CalculateArrowEnd(startPoint, endPoint, arrowLength, arrowWidth, true);
                // var arrowRight = CalculateArrowEnd(startPoint, endPoint, arrowLength, arrowWidth, false);

                // 添加左侧箭头线段
                var leftArrowLine = new LineString
                {
                    Coordinates = new CoordinateCollection(new List<Vector> { arrowLeft[0], arrowLeft[1] }),
                    AltitudeMode = AltitudeMode.Absolute,


                };

                var leftArrowPlacemark = new Placemark
                {
                    Name = $"{lineData[i].SegCode}_Left_Arrow",
                    Geometry = leftArrowLine,
                    StyleUrl = new Uri("#lineStyleW5", UriKind.Relative),

                };

                var rightArrowLine = new LineString
                {
                    Coordinates = new CoordinateCollection(new List<Vector> { arrowLeft[1], arrowLeft[2] }),
                    AltitudeMode = AltitudeMode.Absolute
                };

                var rightArrowPlacemark = new Placemark
                {
                    Name = $"{lineData[i].SegCode}_Right_Arrow",
                    Geometry = rightArrowLine,
                    StyleUrl = new Uri("#lineStyleW10", UriKind.Relative)
                };

                document.AddFeature(linePlacemark);
                document.AddFeature(rightArrowPlacemark);
                document.AddFeature(leftArrowPlacemark);
                //var arrowStyle = new Style
                //{
                //    Id = "arrowStyle",
                //    Icon = new IconStyle
                //    {
                //        Icon = new IconStyle.IconLink(new Uri("http://maps.google.com/mapfiles/kml/shapes/arrow.png")),
                //        Scale = 1.1,
                //        Heading = CalculateHeading(startPoint, endPoint)
                //    }
                //};
            }
            var serializer = new Serializer();
            serializer.Serialize(kml);

            // 保存到文件
            File.WriteAllText("output_with_line.kml", serializer.Xml);

            Console.WriteLine("KML file with points and a line has been created successfully.");
        }

        static Vector[] CalculateArrowEnd(Vector start, Vector end, double length, double width, bool left)
        {

            var v3 = new Vector(
                end.Latitude * 0.618 + start.Latitude * 0.382,
                end.Longitude * 0.618 + start.Longitude * 0.382,
                end.Altitude.Value * 0.618 + start.Altitude.Value * 0.382);

            var v4 = new Vector(
                end.Latitude * 0.668 + start.Latitude * 0.332,
                end.Longitude * 0.668 + start.Longitude * 0.332,
                end.Altitude.Value * 0.668 + start.Altitude.Value * 0.332);
            var v5 = new Vector(
                end.Latitude * 0.718 + start.Latitude * 0.282,
                end.Longitude * 0.718 + start.Longitude * 0.282,
                end.Altitude.Value * 0.718 + start.Altitude.Value * 0.282);
            return new Vector[] { v3, v4, v5 };
        }
        // 计算起点到终点的方位角（heading）

    }
}
