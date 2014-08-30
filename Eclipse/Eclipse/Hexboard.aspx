<%@ Page MaintainScrollPositionOnPostback="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Hexboard.aspx.cs" Inherits="Eclipse.Hexboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <script type="text/javascript">
          var ctx = null;
          var _componentSize = -1;
          var canvas = null;
          var _isMilitaryView = false;
          var _hexBoard;
          var _tempScrollTop;
          var _tempScrollLeft;
          var _lastHex;
          function DrawPopSquare(popSquare) {
              var color = popSquare.Color;
              var isAdvanced = popSquare.isAdvanced;
              var point = popSquare.CanvasLocation;
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
              ctx.closePath();
              if (popSquare.IsOccupied) {
                  var playerColor = popSquare.Owner.Color;
                  ctx.fillStyle = playerColor;
                  ctx.fill();
              }
              ctx.strokeStyle = 'white';
              ctx.lineWidth = 7;
              ctx.stroke();

              ctx.strokeStyle = color;
              ctx.lineWidth = 2;
              ctx.stroke();
              ctx.lineWidth = 1;
              ctx.font = '10px sans-serif';
              ctx.fillStyle = 'black';
              if (isAdvanced) {
                  ctx.textAlign = 'center';
                  ctx.textBaseline = 'middle';
                  ctx.fillText('A', point.X, point.Y);
              }
          }
          function DrawCircle(point, color) {
              ctx.beginPath();
              ctx.fillStyle = color;
              ctx.arc(point.X, point.Y,_componentSize/2, 2 * Math.PI, 0, 2 * Math.PI);
              ctx.fill();   
          }
          function DrawAncient(point) {
              var size = 25;
              x = point.X - size / 2;
              y = point.Y - size / 2;
              var obj = document.getElementById('ancientImg');
              ctx.drawImage(obj, x, y, size, size);
          }
          function DrawDiscovery(point) {
              var size = 20;
              x = point.X - size / 2;
              y = point.Y - size / 2;
              var obj = document.getElementById('discoveryImg');
              ctx.drawImage(obj, x, y, size, size);
          }

          function ShowPopulatableHexes()
          {

              HideTempMenus();
                      $.ajax({
                          url: "EclipseService.asmx/GetPopulateMenus",
                          data: '{}',
                          dataType: "json",
                          type: "POST",
                          contentType: 'application/json; charset=utf-8',
                          success: function (data) {
                              var tempMenus = data["d"];
                              $(tempMenus).each(function(index,menu){
                                  ShowPopulateMenu(menu.Hex, menu.MenuItems);
                              });
                              
                          }
                      });
                  
              
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
              ctx.fillStyle = 'white';
              ctx.fill();
              DrawWormholes(hex);
              
                DrawComponents(hex);
                DrawController(hex);
              
          }

          function DrawController(hex)
          {
              if (hex.Controller) {
                  DrawCircle(hex.CanvasLocation, hex.Controller.Color);
              }
              else if (hex.HasAncient) {
                  DrawDiscovery(hex.CanvasLocation);
              }
          }

          function DrawComponents(hex) {
              if (!_isMilitaryView) {

                  $(hex.PopulationSquares).each(function (index, o) {
                      DrawPopSquare(o);
                     // DrawPopSquare(o.CanvasLocation, o.Color, o.IsAdvanced);
                  });
              }
              else {
                  $(hex.Ships).each(function (index, ship) {

                      DrawShip(ship);
                  });
              }
          }

          function DrawShip(ship) {
              if (ship.IsAncient) {
                  DrawAncient(ship.CanvasLocation);
              }
              else {
                  ctx.textAlign = 'center';
                  ctx.textBaseline = 'middle';
                  ctx.fillStyle = ship.Owner.Color;
                  ctx.font = '15px sans-serif';
                  ctx.fillText(ship.Code, ship.CanvasLocation.X, ship.CanvasLocation.Y);
              }
          }

          function DrawWormholes(hex)
          {
              debugger;
              $(hex.Sides).each(function (index, side) {
                  if (side.HasWormHole)
                  {
                      ctx.beginPath();
                     // ctx.strokeStyle = 'Gray';
                      ctx.arc(side.WormholeCanvasLocation.X, side.WormholeCanvasLocation.Y, 10, side.WormholeStartAngle, side.WormholeEndAngle, true);
                      ctx.stroke();
                
                 }
              });
          }

          function DrawGreenHex(hex, color)
          {
              if (_lastHex)
                  DrawHexboard();
              DrawHex(hex, '#00FF00');
              _lastHex = hex;
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
                  }
              });
          }



          function ajaxAddPopulation(hex, popTypeString) {
              var args = { x: hex.AxialCoordinates.X, y: hex.AxialCoordinates.Y, popType: popTypeString };
              $.ajax({
                  url: "EclipseService.asmx/AddPopulationToHex",
                  data: JSON.stringify(args),
                  dataType: "json",
                  type: "POST",
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var hex = data["d"];
                      DrawHex(hex);
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
                      if (hex && hex.IsVisible)
                      {
                        DrawGreenHex(hex);
                        ShowExploreFromMenu(x, y);
                        }

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
                  }
              });
          }


          function GetExploreFromHexes() {
              var args = {};
              $.ajax({
                  url: "EclipseService.asmx/GetExploreFromHexes",
                  data: JSON.stringify(args),dataType: "json",type: "POST",async: false,contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var hexes = data["d"];
                      HideTempMenus();
                      $(hexes).each(function (index, item) {
                          var callback = function () { GetExploreToHexes(item);};
                          ShowExploreFromMenu(item.CanvasLocation.X, item.CanvasLocation.Y,  callback);

                      });
                  }
              });
          }

          function GetExploreToHexes(hex) {
              var args = {x:hex.AxialCoordinates.X, y:hex.AxialCoordinates.Y};
              $.ajax({
                  url: "EclipseService.asmx/GetExploreToHexes",
                  data: JSON.stringify(args), dataType: "json", type: "POST", async: false, contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var hexes = data["d"];
                      HideTempMenus();
                      $(hexes).each(function (index, item) {
                          var callback = function () { ExploreTo(item);};
                          ShowExploreToMenu(item.CanvasLocation.X, item.CanvasLocation.Y, callback);
                      });
                  }
              });
          }

          function ExploreTo(hex) {
              var args = { x: hex.AxialCoordinates.X, y: hex.AxialCoordinates.Y };
              $.ajax({
                  url: "EclipseService.asmx/ExploreTo",
                  data: JSON.stringify(args), dataType: "json", type: "POST", async: false, contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var hex = data["d"];
                      HideTempMenus();
                      DrawHex(hex);
                      var callback = function () { Rotate(hex);};
                      ShowRotateMenu(hex.CanvasLocation.X, hex.CanvasLocation.Y, callback);
                  }
              });
          }

          function Rotate(hex) {
              var args = { x: hex.AxialCoordinates.X, y: hex.AxialCoordinates.Y };
              $.ajax({
                  url: "EclipseService.asmx/Rotate",
                  data: JSON.stringify(args), dataType: "json", type: "POST", async: false, contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var hex = data["d"];
                      DrawHex(hex);
                      var callback = function () { Rotate(hex); };
                      ShowRotateMenu(hex.CanvasLocation.X, hex.CanvasLocation.Y, callback);
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

          $.ajaxSetup({
              // Disable caching of AJAX responses  
              cache: false
          });




          function ShowExploreFromMenu(x, y,  callback) {
       
                var klon = $('#exploreFromDiv');
                var newId = x + y + '';
                var clone = klon.clone().attr('id', newId);
                $('#tempMenus').append(clone);
                clone.find('.explorebutton').click(callback);
                clone.css({ 'top': y + canvas.offsetTop + 40, 'left': x - clone.outerWidth()/2, 'position': 'absolute' });
               
                clone.show();
          }


          function ShowExploreToMenu(x, y, callback) {

              var klon = $('#exploreToDiv');
              var newId = x + y + '';
              var clone = klon.clone().attr('id', newId);
              $('#tempMenus').append(clone);
              clone.find('.explorebutton').click(callback);
              clone.css({ 'top': y + canvas.offsetTop - clone.outerHeight()/2, 'left': x - clone.outerWidth() / 2, 'position': 'absolute' });

              clone.show();
          }

          function ShowRotateMenu(x, y, callback) {

              var klon = $('#rotateDiv');
              var newId = x + y + '';
              var clone = klon.clone().attr('id', newId);
              $('#tempMenus').append(clone);
              clone.find('.explorebutton').click(callback);
              clone.css({ 'top': y + canvas.offsetTop + 35, 'left': x + _componentSize, 'position': 'absolute' });

              clone.show();
          }

          function HideTempMenus() {
              $('#tempMenus').empty();
          }

          function ShowPopulateMenu( hex, stringList) {
              var clone= $('#populateDiv').clone();
              clone.attr('id', 'populate' + x + y);
              var x = hex.CanvasLocation.X;
              var y = hex.CanvasLocation.Y;
              
              $('#tempMenus').append(clone);
              $(stringList).each(function (index, o) {
                  var callback = function () { ajaxAddPopulation(hex, o); HideTempMenus(); };
                  var button = $(' <li><a href="javascript:void(0)">' + o + '</a></li>');
                  clone.find('.dropdown-menu').append(button);
                  button.click(callback);
              });
              clone.css({ 'top': y + canvas.offsetTop + 40, 'left': x - clone.outerWidth() / 2, 'position': 'absolute' });
              clone.show();
          }




          $(document).ready(function () {
              canvas = document.getElementById('myCanvas');
              ctx = canvas.getContext('2d');

              InitCanvasConstants();
              canvas.addEventListener('click', function (evt) {
                  var mousePos = getMousePos(canvas, evt);
                  //var message = alert('Mouse position: ' + mousePos.x + ',' + mousePos.y);
                  GetNearestHex(mousePos.x, mousePos.y);
              }, false);

              GetHexboard();

              var windowHeight = $(window).height();
              $(window).scrollTop(canvas.height / 2 - windowHeight / 2 + canvas.offsetTop);
              $(window).scrollLeft(canvas.width / 2 - $(window).width() / 2 + canvas.offsetLeft);

              $('#militaryViewTab').click(function (event) {
                  _isMilitaryView = true;
                  GetHexboard();//   window.location.href = '/Hexboard.aspx';
                  //  LoadScroll();
              });


              $('#resourceViewTab').click(function (event) {
                  _isMilitaryView = false;
                  GetHexboard();//   window.location.href = '/Hexboard.aspx';

              });

              $('#exploreTab').click(function (event) {
                  GetExploreFromHexes();
              });

              $('#populateTab').click(function (event) {
                  ShowPopulatableHexes();
              })

              $('#playerboardTab').click(function (event) {
                  $("#modalPlaceholder").load("Playerboard.html");

              });

              $('#supplyboardTab').click(function (event) {
                  $('#modalPlaceholder').load("SupplyBoard.html");
              });

              $('#clearTempMenusTab').click(function (event) {
                  HideTempMenus();
              });


          });
           
          </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
 <img style="display:none"  id="ancientImg" src="Images/ancient.png"/>
   <img style="display:none"  id="discoveryImg" src="Images/discoveryshield.png"/> 
    <div id="exploreFromDiv" style="display:none; background-color:white" class="explore">
            <div class="btn-group-vertical">
               <button type="button" class="btn btn-primary explorebutton">Explore from</button>
            
            </div>
        </div>

        <div id="exploreToDiv" style="display:none; background-color:white" class="explore">
            <div class="btn-group-vertical">
               <button type="button" class="btn btn-warning explorebutton">Explore to</button>
            
            </div>
        </div>

            <div id="rotateDiv" style="display:none; background-color:white" class="explore">
            <div class="btn-group-vertical">
               <button type="button" class="btn btn-success explorebutton">Rotate</button>
             <button type="button" onclick="HideTempMenus(); return false;" class="btn btn-default">Accept</button>
            </div>
        </div>

    
            <div id="populateDiv" style="display:none; background-color:white" >
                <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                    Populate <span class="caret"></span>
                  </button>
                  <ul class="dropdown-menu" role="menu">
                    
                  </ul>
        </div>

     <div id="tempMenus">
    </div>
    <div>
    <canvas id="myCanvas" width="900" height="900" style="border:1px solid #000000;">
Your browser does not support the HTML5 canvas tag.
</canvas>
    </div>
   

<div id="modalPlaceholder"></div>

           


</asp:Content>
