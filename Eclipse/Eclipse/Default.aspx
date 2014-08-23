﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Eclipse.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="Scripts/jquery-2.0.3.min.js" type="text/javascript"></script>
      <script type="text/javascript">
          var ctx = null;
          var _componentSize = -1;

          function DrawShape(point, sides) {
              ctx.beginPath();
              var size = _componentSize;
              for (i = 0; i <= sides; i++) {
                  var angle = 2 * Math.PI / sides * (i);
                  x_i = point.X + size * Math.cos(angle);
                  y_i = point.Y + size * Math.sin(angle);
                  if (i == 0)
                      ctx.moveTo(x_i, y_i)
                  else
                      ctx.lineTo(x_i, y_i)
              }
              ctx.fillStyle = '#00FF00';
              ctx.stroke();
          }
          function DrawHex(hex, color) {
              var center_x = hex.CanvasLocation.X;
              var center_y = hex.CanvasLocation.Y;
              var size = hex.Radius;
              ctx.beginPath();
              ctx.strokeStyle = "#000000"

              for(i=0;i<=6;i++)
              {
                  var  angle = 2 * Math.PI / 6 * (i);
                  x_i = center_x + size * Math.cos(angle);
                  y_i = center_y + size * Math.sin(angle);
                  if (i == 0)
                      ctx.moveTo(x_i, y_i)
                  else
                        ctx.lineTo(x_i, y_i)
              }
              if (color)
                  ctx.strokeStyle = color;
              ctx.stroke();

              DrawWormholes(hex);
              DrawComponents(hex);
              
          }

          function DrawComponents(hex) {
              $(hex.ComponentCanvasLocations).each(function (index, point) {
                  DrawShape(point, 3);
              });
          }

          function DrawWormholes(hex)
          {
              $(hex.Sides).each(function (index, side) {
                  if (side.HasWormHole)
                  {
                      ctx.beginPath();
                      ctx.arc(side.WormholeCanvasLocation.X, side.WormholeCanvasLocation.Y, 10, side.WormholeStartAngle, side.WormholeEndAngle, true);
                      ctx.stroke();
                      ctx.stroke();
                 }
              });
          }

          function DrawGreenHex(hex, color)
          {
              DrawHex(hex, '#00FF00')
          }

          function MoveRight(ctx, times) {
              var R= GetHexRadius();
              ctx.moveTo(ctx.X + times * 1.5 * R, ctx.Y);
              ctx.moveTo(ctx.X, ctx.Y + times * R);
          }

          function MoveDown(ctx, times) {
              var R = GetHexRadius();
              ctx.moveTo(ctx.X, ctx.Y + times * R);
          }
          
          function GetHexRadius(){
          }

          function GetCanvasContext() {
          }

          function MoveToCenter(ctx) {
          }

          function TestService() {
            
              $.ajax({
                  url: "EclipseService.asmx/GetHexBoard",
                  data: '{}',
                  dataType: "json",
                  type: "POST",
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var hexboard = data["d"];
                      
                      $(hexboard.Hexes).each(function (index, item) {
                          DrawHex(item);
                      });
                  },
                  error: function (xmlHttpRequest, textStatus, errorThrown) {
                      alert(errorThrown);
                  }
              });
          }

          function GetNearestHex(x,y) {
              var args = { x: x, y: y };

              $.ajax({
                  url: "EclipseService.asmx/GetNearestHex",
                  data: JSON.stringify(args),
                  dataType: "json",
                  type: "POST",
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var hex = data["d"];
                      DrawGreenHex(hex);
                  },
                  error: function (xmlHttpRequest, textStatus, errorThrown) {
                      alert(errorThrown);
                  }
              });
          }

          function InitCanvasConstants() {
              var args = {};

              $.ajax({
                  url: "EclipseService.asmx/GetCanvasConstants",
                  data: JSON.stringify(args),
                  dataType: "json",
                  type: "POST",
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var constants = data["d"];
                      _componentSize = constants.ComponentSize;
                  },
                  error: function (xmlHttpRequest, textStatus, errorThrown) {
                      alert(errorThrown);
                  }
              });
          }

          function getMousePos(canvas, evt) {
              var rect = canvas.getBoundingClientRect();
              return {
                  x: evt.clientX - rect.left,
                  y: evt.clientY - rect.top
              };
          }

          $(document).ready(function () {
              var canvas = document.getElementById('myCanvas');
              ctx = canvas.getContext('2d');
              InitCanvasConstants();
              canvas.addEventListener('mousedown', function (evt) {
                  var mousePos = getMousePos(canvas, evt);
                  var message = alert('Mouse position: ' + mousePos.x + ',' + mousePos.y);
                  GetNearestHex(mousePos.x, mousePos.y);
              }, false);

              TestService();
          });

          </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <canvas id="myCanvas" width="900" height="900" style="border:1px solid #000000;">
Your browser does not support the HTML5 canvas tag.
</canvas>
    </div>
    </form>
</body>
</html>
