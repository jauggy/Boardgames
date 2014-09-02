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
          var _navbarUI;
          var _populatesRemaining;

          function DrawPopSquare(popSquare) {
              var color = popSquare.Color;
              var isAdvanced = popSquare.IsAdvanced;
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
          function DrawDiscovery(point, text) {
              var size = 20;
              x = point.X - size / 2;
              y = point.Y - size / 2;
              var obj = document.getElementById('discoveryImg');
              ctx.drawImage(obj, x, y, size, size);
              ctx.font = '10px sans-serif';
              ctx.textAlign = 'center';
              ctx.textBaseline = 'middle';
              ctx.fillText(text, point.X, point.Y);
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
                          success: ShowPopulatableHexesCallback
                      });
                  
              
          }

          function ShowPopulatableHexesCallback(model) {
              var tempMenus = model["d"];
              $(tempMenus).each(function (index, menu) {
                  ShowPopulateMenu(menu.Hex, menu.MenuItems);
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
                  DrawDiscovery(hex.CanvasLocation, 'a');
              }
              else if (hex.HasDiscoveryToken) {
                  DrawDiscovery(hex.CanvasLocation, '');
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
                      RefreshNavbar();
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
                      var model = data["d"];
                
                      DrawHex(model.Hex);
                      RefreshNavbarCallback({ d: model.MainNavbarUI });
                      ShowPopulatableHexesCallback({ d: model.PopulateMenus });
                      $('#log').html(model.Log);
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
                          var callback = function () { HideMainActions(); GetExploreToHexes(item); };
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
                      var callback = function (event) {
                          RotateHandler(event, hex);
                      };
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

          function RotateHandler(event, hex) {
            
              if ($(event.currentTarget).text() == "Rotate")
                  Rotate(hex);
              else
              {
                  HideTempMenus();
                  _lastHex = hex;
                  if (hex.HasAncient)
                  {

                  } else {
                      $("#modalPlaceholder").load("ChoosePopulationType.html");
                  }
                 
              }
          }




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
              //clone.find('.explorebutton').click(callback);
              clone.css({ 'top': y + canvas.offsetTop + 35, 'left': x + _componentSize, 'position': 'absolute' });

              clone.show();

              $(clone).on('click', '.btn', function (event) {
                  callback(event);
              });
          }

          function HideTempMenus() {
              $('#tempMenus').empty();
          }

          function ShowBuildMenus() {
              $.ajax({
                  url: "EclipseService.asmx/GetBuildMenus",
                  data: '{}', dataType: "json", type: "POST", async: false, contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var menus = data["d"];
                      for (var i = 0; i < menus.length; i++) {
                          var hex = menus[i].Hex;
                          ShowTempMenuBottom(menus[i], function (event) {
                              $('#modalPlaceholder').load('Build.html');
                          });
                      }
                  }
              });
          }

          function ShowPopulateMenu( hex, stringList) {
              var clone= $('#populateDiv').clone();
              clone.attr('id', 'populate' + x + y);
              var x = hex.CanvasLocation.X;
              var y = hex.CanvasLocation.Y;
              
              $('#tempMenus').append(clone);
              $(stringList).each(function (index, o) {
                
                  var button = $(' <li><a href="javascript:void(0)">' + o + '</a></li>');
                  clone.find('.dropdown-menu').append(button);
                  
              });

              clone.on('click', '.btn', function (event) {
                  _lastHex = hex;
                  $('#modalPlaceholder').load('ChoosePopulationType.html');

                  return false;
              });

              clone.css({ 'top': y + canvas.offsetTop + 40, 'left': x - clone.outerWidth() / 2, 'position': 'absolute' });
              clone.show();
          }

          function ShowTempMenuMiddle(tempMenu, callback) {
              var x = tempMenu.Hex.CanvasLocation.X;
              var y = tempMenu.Hex.CanvasLocation.Y;
              var coord = tempMenu.Hex.AxialCoordinates;
              var btnHtml = "";
              var mnu = $('#tempMenu').clone();
              for (var i = 0; i < tempMenu.MenuItems.length; i++) {
                  btnHtml += '<button type="button" data-x="' + coord.X + '" data-y="' + coord.Y + '" class="btn btn-warning">' + tempMenu.MenuItems + '</button>';
              }
              $('#tempMenus').append(mnu);
              $(mnu).find('.btn-group-vertical').append(btnHtml);
              $(mnu).find('.btn').click(function (event) {
                  callback(event);
              });
              mnu.css({ top: y + canvas.offsetTop - $(mnu).outerHeight()/2, left: x - $(mnu).outerWidth() / 2 })

          }

              //Will set the _lastHex variable before callback
          function ShowTempMenuBottom(tempMenu, callback) {
              var x = tempMenu.Hex.CanvasLocation.X;
              var y = tempMenu.Hex.CanvasLocation.Y;
              var coord = tempMenu.Hex.AxialCoordinates;
              var btnHtml = "";
              var mnu = $('#tempMenu').clone();
              for (var i = 0; i < tempMenu.MenuItems.length; i++) {
                  btnHtml += '<button type="button" data-x="'+coord.X+'" data-y="'+coord.Y+'" class="btn btn-primary">' + tempMenu.MenuItems + '</button>';
              }
              $('#tempMenus').append(mnu);
              $(mnu).find('.btn-group-vertical').append(btnHtml);
              $(mnu).find('.btn').click(function (event) {
                  _lastHex;
                  callback(event);
              });
              mnu.css({top:y+canvas.offsetTop+40,left:x-$(mnu).outerWidth()/2})
             
          }

          function GetInfluenceFromMenus(callback) {
              HideTempMenus();
              
              $.ajax({
                  url: "EclipseService.asmx/GetInfluenceFromMenus",
                  data: '{}', dataType: "json", type: "POST", async: false, contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var menus = data["d"];
                      $(menus).each(function (index, menu) {

                          ShowTempMenuBottom(menu, callback);
                      });

                  }
              });
          }

          function InfluenceFromHexToPlayerBoard(x,y) {
              HideTempMenus();
              var args = { x: x, y: y };
              $.ajax({
                  url: "EclipseService.asmx/InfluenceFromHexToPlayerBoard",
                  data: JSON.stringify(args), dataType: "json", type: "POST", async: false, contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var hex = data["d"];
                      DrawHex(hex);

                  }
              });
          }

          function GetInfluenceToMenus(callback) {
              HideTempMenus();
              
              $.ajax({
                  url: "EclipseService.asmx/GetInfluenceToMenus",
                  data: '{}', dataType: "json", type: "POST", async: false, contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var menus = data["d"];
                      $(menus).each(function (index, menu) {
                          ShowTempMenuMiddle(menu, callback);
                      });

                  }
              });
          }

          function NextPlayer() {
              _hideMainActions = false;
              HideTempMenus();
              $('#log').empty();
              $.ajax({
                  url: "EclipseService.asmx/NextPlayer",
                  data: '{}', dataType: "json", type: "POST", async: false, contentType: 'application/json; charset=utf-8',
                  success: RefreshNavbarCallback
              });
          }


          function HideMainActions() {
              //$('#nextPlayerTab').addClass('active');
              $('.mainaction').css('display', 'none');
          }

          function RefreshNavbar() {
              
              $.ajax({
                  url: "EclipseService.asmx/GetMainNavbarUI",
                  data: '{}', dataType: "json", type: "POST", async: false, contentType: 'application/json; charset=utf-8',
                  success: RefreshNavbarCallback
              });
          }

          function RefreshNavbarCallback(data) {
              var model = data["d"];
              _navbarUI = model;
              $('.navbar-brand').html(model.CurrentPlayerName);
              _populatesRemaining = model.PopulatesRemaining;
              if (model.PopulatesRemaining > 0) {
                  $('#populateTab').text('Populate (' + model.PopulatesRemaining + ')');
                  $('#populateTab').show();
              }
              else {
                  $('#populateTab').hide();
              }
              
              if (model.HasDoneMainAction) {
                  $('.mainaction').css('display', 'none');
              }
              else {
                  $('.mainaction').css('display', 'inline');
              }

              $('#log').html(model.Log);

          }


          $(document).ready(function () {
              canvas = document.getElementById('myCanvas');
              ctx = canvas.getContext('2d');
              $('#log-container').css('top', $('.navbar').first().height())
              InitCanvasConstants();
           /*   canvas.addEventListener('click', function (evt) {
                  var mousePos = getMousePos(canvas, evt);
                  //var message = alert('Mouse position: ' + mousePos.x + ',' + mousePos.y);
                  GetNearestHex(mousePos.x, mousePos.y);
              }, false);*/

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

              $('#influenceHexToBoard').click(function (event) {
                  
                  GetInfluenceFromMenus(function (event) {
                      var x = $(event.currentTarget).attr('data-x');
                      var y = $(event.currentTarget).attr('data-y');
                      InfluenceFromHexToPlayerBoard(x, y);
                  });
              });

              $('#influenceBoardToHex').click(function (event) {
                  GetInfluenceToMenus(function (event) {

                  });
              });

              $('#nextPlayerTab').click(function (event) {
                  NextPlayer();
                  $('#nextPlayerTab').removeClass('active');
                  event.stopImmediatePropagation();
              });

              $('#upgradeTab').click(function (event) {
                  $('#modalPlaceholder').load('UpgradeView.html');
              });

              $('#combatSimTab').click(function (event) {
                  $('#modalPlaceholder').load('CombatSim.html');
              });

              $('#buildTab').click(function (event) {
                  ShowBuildMenus();
              });
          });
           
          </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
 <img style="display:none"  id="ancientImg" src="Images/ancient.png"/>
   <img style="display:none"  id="discoveryImg" src="Images/discoveryshield.png"/> 

    <!--Begin temp menus-->
    <div style="display:none">
          <div id="dropMenu" style="position:absolute">
             <!-- Single button -->
                <div class="btn-group">
                  <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
                    Action <span class="caret"></span>
                  </button>
                  <ul class="dropdown-menu" role="menu">
                    <li><a href="#">Action</a></li>
                    <li><a href="#">Another action</a></li>
                    <li><a href="#">Something else here</a></li>
                    <li class="divider"></li>
                    <li><a href="#">Separated link</a></li>
                  </ul>
                </div>
        </div>

        <div id="tempMenu" style="position:absolute">
            <div class="btn-group-vertical">
                         
            </div>
        </div>

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
             <button type="button" data-toggle="popover" class="btn btn-default">Accept</button>
            </div>
        </div>


         <div id="populateDiv" style="display:none; background-color:white" >
                <button type="button" class="btn btn-primary">Populate</button>
            
        </div>
     <!--End 
         /*   <div id="populateDiv" style="display:none; background-color:white" >
                <button type="button" class="btn btn-primary dropdown-toggle" data-toggle="dropdown">
                    Populate <span class="caret"></span>
                  </button>
                  <ul class="dropdown-menu" role="menu">
                    
                  </ul>
        </div>*/temp menus-->
    </div>
  
    <!--End temp menus-->
     <div id="tempMenus">
    </div>
    <div>
    <canvas id="myCanvas" width="900" height="900" style="border:1px solid #000000;">
Your browser does not support the HTML5 canvas tag.
</canvas>
    </div>
   
    <div style="position:fixed; top:100px; left:0px; padding:10px"  id="log-container">

                    <div  style="font-size:small;">
                      <p id="log" class="text-warning">...</p>
                    </div>
          
    </div>

<div id="modalPlaceholder"></div>

           


</asp:Content>
