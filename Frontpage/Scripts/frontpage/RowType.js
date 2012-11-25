
function ImageRow() {
}

ImageRow.prototype.Render = function () {
    var result = $("<li><img></li>");
    result.find("img").attr("src", this.imageUrl);
    return result;
};

ImageRow.prototype.StartEdit = function()
{
    alert("clicked edit");
}

function AllRightAnnouncement() {
    this.imageUrl = "/Content/AllRightGentlemen_dialog.png";
    this.announcement = "We're going to master the gif technology.";
}

$.extend(AllRightAnnouncement.prototype, ImageRow.prototype);

AllRightAnnouncement.prototype.StartEdit = function() {
    var templateSource = "<div>You have text {{announcement}}.</div>";
    var template = Handlebars.compile(templateSource);

    var content = $(template(this));
    content.dialog({modal:true});
    $("body").append();
}


function AllRightMinor() {
    this.imageUrl = "/Content/AllRightGentlemen_unimpressed.png";
}

$.extend(AllRightMinor.prototype, ImageRow.prototype);

function AllRightMajor() {
    this.imageUrl = "/Content/AllRightGentlemen_impressed.png";
}

$.extend(AllRightMajor.prototype, ImageRow.prototype);
