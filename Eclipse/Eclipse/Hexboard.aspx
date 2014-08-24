<%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hexboard.aspx.cs" Inherits="Eclipse.Hexboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <script type="text/javascript">
          var ctx = null;
          var _componentSize = -1;
          var canvas = null;
          var _isMilitaryView = false;
          var _hexBoard;
          var _tempScrollTop;
          function DrawPopSquare(point, color, isAdvanced) {
              //var ctx = canvas.getContext('2d');
              ctx.beginPath();
              sides = 4;
              var size = _componentSize;
              for (i = 0; i <= sides; i++) {
                  var angle = 2 * Math.PI / sides * (i) + Math.PI / 4;
                  x_i = point.X + size * Math.cos(angle);
                  y_i = point.Y + size * Math.sin(angle);
                  if (i == 0)
                      ctx.moveTo(x_i, y_i)
                  else
                      ctx.lineTo(x_i, y_i)
              }
              ctx.strokeStyle = color;
              ctx.lineWidth = 2;
              ctx.stroke();
              ctx.lineWidth = 1;
              ctx.font = '10pt';
              ctx.fillStyle = 'black';
              if (isAdvanced) {
                  ctx.textAlign = 'center';
                  ctx.textBaseline = 'middle';
                  ctx.fillText('A', point.X, point.Y);
              }
          }
          function DrawCircle(point, color) {
             // ctx.beginPath();
             // ctx.fillStyle = color;
             // ctx.arc(point.X, point.Y,_componentSize/2, 2 * Math.PI, 0, 2 * Math.PI);
              // ctx.fill();
              var size = 25;
              x = point.X - size / 2;
              y = point.Y - size / 2;
              var obj = document.getElementById('ancientImg');
              ctx.drawImage(obj, x, y, size, size);
                           
          }
          function DrawHex(hex, color) {
              if (!hex.IsVisible)
                  return;
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
                DrawController(hex);
              
          }

          function DrawController(hex)
          {
              if (hex.Controller) {
                  DrawCircle(hex.CanvasLocation, hex.Controller.Color);
              }
          }

          function DrawComponents(hex) {
              $(hex.PopulationSquares).each(function (index, o) {
                  if (!_isMilitaryView)
                    DrawPopSquare(o.CanvasLocation, o.Color, o.IsAdvanced);
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

          function GetHexboard() {
            
              $.ajax({
                  url: "EclipseService.asmx/GetHexBoard",
                  data: '{}',
                  dataType: "json",
                  type: "POST",
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var hexboard = data["d"];
                      _hexBoard = hexboard;
                      DrawHexboard();
                  },
                  error: function (xmlHttpRequest, textStatus, errorThrown) {
                      alert(errorThrown);
                  }
              });
          }

          function DrawHexboard()
          {
              ctx.clearRect(0, 0, canvas.width, canvas.height);
              $(_hexBoard.Hexes).each(function (index, item) {
                  DrawHex(item);
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
                  async: false,
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var constants = data["d"];
                      _componentSize = constants.ComponentSize;
                      canvas.setAttribute('width', constants.CanvasWidth);
                      canvas.setAttribute('height', constants.CanvasHeight);
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
              canvas = document.getElementById('myCanvas');
              ctx = canvas.getContext('2d');
           
              InitCanvasConstants();
              canvas.addEventListener('dblclick', function (evt) {
                  var mousePos = getMousePos(canvas, evt);
                  var message = alert('Mouse position: ' + mousePos.x + ',' + mousePos.y);
                  GetNearestHex(mousePos.x, mousePos.y);
              }, false);

              GetHexboard();
              
              var windowHeight = $(window).height();
              $(window).scrollTop(canvas.height/2 - windowHeight / 2 + canvas.offsetTop);
           
              $('#militaryViewTab').click(function (event) {
                  _tempScrollTop = $(window).scrollTop();
                  _isMilitaryView = true;
                  DrawHexboard();//   window.location.href = '/Hexboard.aspx';
                  $(window).scrollTop(_tempScrollTop);
              });


              $('#resourceViewTab').click(function (event) {
                  _tempScrollTop = $(window).scrollTop();
                  _isMilitaryView = false;
                  DrawHexboard();//   window.location.href = '/Hexboard.aspx';
                  $(window).scrollTop(_tempScrollTop);
              });
          });

              




          </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
 <img style="display:none"  id="ancientImg" src="Images/ancient.png"/>

    <div>
    <canvas id="myCanvas" width="900" height="900" style="border:1px solid #000000;">
Your browser does not support the HTML5 canvas tag.
</canvas>
    </div>
   
</asp:Content>
