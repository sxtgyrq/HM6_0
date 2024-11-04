using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WsOfWebClient
{
    internal partial class Startup
    {
        internal static void ObjData(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    //string pattern = @"/(?<amid>[a-zA-Z0-9]{32})$";

                    if (pathValue == "/Drone.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\W202410\\lowpolydrone\\simple4.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);

                    }
                    else if (pathValue == "/Drone.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\W202410\\lowpolydrone\\simple4.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }

                    //Match match = Regex.Match(pathValue, pattern);
                    //if (match.Success)
                    //{
                    //    string amid = match.Groups["amid"].Value;
                    //    //int roomindex = int.Parse(match.Groups["roomindex"].Value);
                    //    //int password = int.Parse(match.Groups["password"].Value);
                    //    var jsonStr = Room.GetObjFileJson(amid);

                    //    if (string.IsNullOrEmpty(jsonStr))
                    //    {

                    //    }
                    //    else
                    //    {

                    //        {
                    //            var bytes = Encoding.UTF8.GetBytes(jsonStr);
                    //            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    //        }
                    //    }
                    //}
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }

        internal static void PicData(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    string pattern = @"/(?<fpCode>[A-Z]{10})/(?<height>\d+)/(?<pic>[np]{1}[xyz]{1}).jpg$";

                    var regex = new Regex(pattern);
                    Match match = regex.Match(pathValue);

                    if (match.Success)
                    {
                        string fpCode = match.Groups["fpCode"].Value;
                        string height = match.Groups["height"].Value;
                        string pic = match.Groups["pic"].Value;
                        //regex.Matches(pathValue).
                        var filePath = $"E:\\DB\\bgImg\\{fpCode}\\h{height}\\{pic}.jpg";
                        if (File.Exists(filePath))
                        {
                            context.Response.ContentType = "image/jpeg";
                            var bytes = File.ReadAllBytes(filePath);
                            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                        }
                        else
                        {
                            filePath = $"E:\\DB\\bgImg\\{pic}.jpg";
                            context.Response.ContentType = "image/jpeg";
                            var bytes = File.ReadAllBytes(filePath);
                            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                        }
                    }
                    else
                    {

                    }

                    //Match match = Regex.Match(pathValue, pattern);
                    //if (match.Success)
                    //{
                    //    string amid = match.Groups["amid"].Value;
                    //    //int roomindex = int.Parse(match.Groups["roomindex"].Value);
                    //    //int password = int.Parse(match.Groups["password"].Value);
                    //    var jsonStr = Room.GetObjFileJson(amid);

                    //    if (string.IsNullOrEmpty(jsonStr))
                    //    {

                    //    }
                    //    else
                    //    {

                    //        {
                    //            var bytes = Encoding.UTF8.GetBytes(jsonStr);
                    //            await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    //        }
                    //    }
                    //}
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }

        internal static void ObjCore(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    if (pathValue == "/core.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\W202410\\core\\core003.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/core.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\W202410\\core\\core003.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/px.jpg")
                    {
                        context.Response.ContentType = "image/jpeg";
                        var bytes = File.ReadAllBytes("E:\\DB\\bgImg\\RUDCSSPVVX\\h0\\px.jpg");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }

        internal static void goldIcon(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    if (pathValue == "/gold.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\goldModel\\gold.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/gold.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\goldModel\\gold.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/material_baseColor.png")
                    {
                        context.Response.ContentType = "image/jpeg";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\goldModel\\material_baseColor.png");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }


        //goldCompass
        internal static void goldCompass(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    if (pathValue == "/Compass.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\compass\\Compass.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/Compass.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\compass\\Compass.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/compassBG.png")
                    {
                        context.Response.ContentType = "image/jpeg";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\compass\\compassBG.png");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }

        //turbine
        internal static void turbine(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    if (pathValue == "/turbine.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\turbine\\turbine.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/turbine.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\turbine\\turbine.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    //else if (pathValue == "/compassBG.png")
                    //{
                    //    context.Response.ContentType = "image/jpeg";
                    //    var bytes = File.ReadAllBytes("E:\\DB\\model\\compass\\compassBG.png");
                    //    await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    //}
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }

        //satelite
        internal static void satelite(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    if (pathValue == "/satelite.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\satelite\\satelite.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/satelite.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\satelite\\satelite.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/satelite.png")
                    {
                        context.Response.ContentType = "image/png";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\satelite\\satelite.png");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }


        internal static void battery(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    if (pathValue == "/battery.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\battery\\battery.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/battery.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\battery\\battery.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/battery.png")
                    {
                        context.Response.ContentType = "image/png";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\battery\\battery.png");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }

        internal static void doubleRewardIcon(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    if (pathValue == "/doubleReward.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\doubleReward\\doubleReward.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/doubleReward.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\doubleReward\\doubleReward.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/doubleReward.png")
                    {
                        context.Response.ContentType = "image/png";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\doubleReward\\doubleReward.png");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }

        //speedicon
        internal static void speedicon(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    if (pathValue == "/speedicon.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\speedicon\\speedicon.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/speedicon.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\speedicon\\speedicon.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/speedicon.png")
                    {
                        context.Response.ContentType = "image/png";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\speedicon\\speedicon.png");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }

        internal static void gearIcon(IApplicationBuilder app)
        {
            app.UseCors("AllowAny");
            app.Run(async context =>
            {
                try
                {
                    //$.get("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/d/d")
                    //$.getJSON("http://127.0.0.1:11001/objdata/04FF6C83E093F15D5E844ED94838D761/3/2")
                    // throw new NotImplementedException();

                    var pathValue = context.Request.Path.Value;

                    if (pathValue == "/gearicon.obj")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\gearicon\\gearicon.obj");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/gearicon.mtl")
                    {
                        context.Response.ContentType = "text/plain";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\gearicon\\gearicon.mtl");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                    else if (pathValue == "/gearicon.jpg")
                    {
                        context.Response.ContentType = "image/jpeg";
                        var bytes = File.ReadAllBytes("E:\\DB\\model\\gearicon\\gearicon.jpg");
                        await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
                    }
                }
                catch (Exception e)
                {
                    //throw e;
                }
            });
        }
    }
}
