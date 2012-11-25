
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
}

$.extend(AllRightAnnouncement.prototype, ImageRow.prototype);

function AllRightMinor() {
    this.imageUrl = "/Content/AllRightGentlemen_unimpressed.png";
}

$.extend(AllRightMinor.prototype, ImageRow.prototype);

function AllRightMajor() {
    this.imageUrl = "/Content/AllRightGentlemen_impressed.png";
}

$.extend(AllRightMajor.prototype, ImageRow.prototype);
