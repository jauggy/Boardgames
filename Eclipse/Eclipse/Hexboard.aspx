<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hexboard.aspx.cs" Inherits="Eclipse.Hexboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <script type="text/javascript">
          var ctx = null;
          var _componentSize = -1;
          var canvas = null;

          function DrawPopSquare(point, color) {
              var ctx = canvas.getContext('2d');
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
             // ctx.fillStyle = '#00FF00';
            //  ctx.fill();
              ctx.strokeStyle = color;
              ctx.lineWidth = 2;
              ctx.stroke();
              ctx.lineWidth = 1;
              ctx.font = '10pt';
              ctx.fillStyle = 'black';
              ctx.textAlign = 'center';
              ctx.textBaseline = 'middle';
              ctx.fillText('6', point.X, point.Y);
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
              
          }

          function DrawComponents(hex) {
              $(hex.PopulationSquares).each(function (index, o) {
                  DrawPopSquare(o.CanvasLocation, o.Color);
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

              TestService();
          });

          </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
 <div class="navbar navbar-default navbar-fixed-top" role="navigation">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <a class="navbar-brand" href="#">Project name</a>
        </div>
        <div class="navbar-collapse collapse">
          <ul class="nav navbar-nav">
            <li class="active"><a href="#">Home</a></li>
            <li><a href="#about">About</a></li>
            <li><a href="#contact">Contact</a></li>
            <li class="dropdown">
              <a href="#" class="dropdown-toggle" data-toggle="dropdown">Dropdown <span class="caret"></span></a>
              <ul class="dropdown-menu" role="menu">
                <li><a href="#">Action</a></li>
                <li><a href="#">Another action</a></li>
                <li><a href="#">Something else here</a></li>
                <li class="divider"></li>
                <li class="dropdown-header">Nav header</li>
                <li><a href="#">Separated link</a></li>
                <li><a href="#">One more separated link</a></li>
              </ul>
            </li>
          </ul>
          <ul class="nav navbar-nav navbar-right">
            <li><a href="../navbar/">Default</a></li>
            <li><a href="../navbar-static-top/">Static top</a></li>
            <li class="active"><a href="./">Fixed top</a></li>
          </ul>
        </div><!--/.nav-collapse -->
      </div>
    </div>

    <div>
    <canvas id="myCanvas" width="900" height="900" style="border:1px solid #000000;">
Your browser does not support the HTML5 canvas tag.
</canvas>
    </div>
   
</asp:Content>
