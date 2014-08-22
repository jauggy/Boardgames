<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Eclipse.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="Scripts/jquery-2.0.3.min.js" type="text/javascript"></script>
      <script type="text/javascript">
          function DrawHex(hex) {
              var center_x = hex.CanvasLocation.X;
              var center_y = hex.CanvasLocation.Y;
              var size = hex.Radius;
              var c=document.getElementById("myCanvas");
              var ctx=c.getContext("2d");
              ctx.beginPath();
             

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
                          
              ctx.stroke();
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

          $(document).ready(function () {
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
