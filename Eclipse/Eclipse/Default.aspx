<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Eclipse.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script src="../Scripts/jquery-1.10.2.js" type="text/javascript"></script>
      <script type="text/javascript">
          function DrawHex(hex) {
              var ctx = GetCanvasContext();
              var x = hex.X;
              MoveToCenter(ctx);
              MoveRight(ctx, x);
              MoveDown(ctx, y);

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

          function GetThresholdSettings(graphSegmentId) {
              var dataObject = { 'graphSegmentId': graphSegmentId }
              $.ajax({
                  url: "UserProfileService.asmx/GetThresholdPopupModel",
                  data: JSON.stringify(dataObject),
                  dataType: "json",
                  type: "POST",
                  contentType: 'application/json; charset=utf-8',
                  success: function (data) {
                      var model = data["d"];
                      PopulateThresholdSettings(model.Thresholds, graphSegmentId);
                      PopulateThresholdHistory(model.ThresholdHistory, graphSegmentId);
                      ShowModal('currentTabView', graphSegmentId);
                  },
                  error: function (xmlHttpRequest, textStatus, errorThrown) {
                      alert(errorThrown);
                  }
              });
          }

          </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
